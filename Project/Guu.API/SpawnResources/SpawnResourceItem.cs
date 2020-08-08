using System.Collections.Generic;
using SRML.SR;
using UnityEngine;

namespace Guu.API.SpawnResources
{
	/// <summary>
	/// This is the base class for all spawn resource items
	/// </summary>
	public abstract class SpawnResourceItem : RegistryItem<SpawnResourceItem>
	{
		// A buffer for the registration of plant slots
		private static readonly Dictionary<Identifiable.Id, GardenCatcher.PlantSlot> PLANT_SLOT_BUFFER = new Dictionary<Identifiable.Id, GardenCatcher.PlantSlot>();

		/// <summary>The prefab for this item</summary>
		protected GameObject Prefab { get; set; }

		/// <summary>The name prefix for this object</summary>
		protected abstract string NamePrefix { get; }

		/// <summary>Is this a garden resource?</summary>
		protected abstract bool IsGarden { get; }

		/// <summary>Is this a garden resource for the deluxe upgrade?</summary>
		protected abstract bool IsDeluxe { get; }

		/// <summary>The ID of the plant object of this item</summary>
		protected abstract Identifiable.Id PlantID { get; }

		/// <summary>The Spawn Resource ID of this item</summary>
		public abstract SpawnResource.Id ID { get; }

		/// <summary>The objects to spawn by this item</summary>
		protected abstract Identifiable.Id[] ToSpawn { get; }

		/// <summary>The bonus objects to spawn by this item</summary>
		protected virtual Identifiable.Id[] BonusToSpawn { get; } = null;

		/// <summary>The max number of objects to spawn</summary>
		protected abstract int MaxObjectsSpawned { get; }

		/// <summary>The min number of objects to spawn</summary>
		protected abstract int MinObjectsSpawned { get; }

		/// <summary>The min number of objects to spawn with the nutrient upgrade</summary>
		protected virtual int MinNutrientObjectsSpawned { get; } = -1;

		/// <summary>The min spawn interval in game hours</summary>
		protected virtual int MinSpawnIntervalGameHours { get; } = 18;

		/// <summary>The max spawn interval in game hours</summary>
		protected virtual int MaxSpawnIntervalGameHours { get; } = 24;

		/// <summary>The chance to spawn a bonus spawn</summary>
		protected virtual float BonusChance { get; } = 0.01f;

		/// <summary>Min about of bonus selected</summary>
		protected virtual int MinBonusSelections { get; } = 0;

		/// <summary>The mesh to display when growing</summary>
		protected abstract Mesh PreviewMesh { get; }

		/// <summary>The material for the mesh being displayed</summary>
		protected abstract Material PreviewMat { get; }

		/// <summary>Builds the spawn points</summary>
		protected void BuildSpawnPoints(SpawnResource spawn)
		{
			foreach (Joint joint in spawn.SpawnJoints)
			{
				joint.gameObject.GetComponent<MeshFilter>().sharedMesh = PreviewMesh;
				joint.gameObject.GetComponent<MeshRenderer>().sharedMaterial = PreviewMat;
			}
		}

		/// <summary>Registers the item into it's registry</summary>
		public override SpawnResourceItem Register()
		{
			Build();

			// Collect Joints
			SpawnResource spawn = Prefab.GetComponent<SpawnResource>();
			spawn.SpawnJoints = Prefab.GetComponentsInChildren<Joint>();

			LookupRegistry.RegisterSpawnResource(Prefab);

			if (IsGarden)
				RegisterSlot();

			return this;
		}

		// Registers the plant slot for this item based on the slot buffer
		private void RegisterSlot()
		{
			if (!PLANT_SLOT_BUFFER.ContainsKey(PlantID))
			{
				PLANT_SLOT_BUFFER.Add(PlantID, new GardenCatcher.PlantSlot()
				{
					id = PlantID,
					deluxePlantedPrefab = null,
					plantedPrefab = null
				});
			}

			if (PLANT_SLOT_BUFFER[PlantID].plantedPrefab == null && !IsDeluxe)
				PLANT_SLOT_BUFFER[PlantID].plantedPrefab = Prefab;

			if (PLANT_SLOT_BUFFER[PlantID].deluxePlantedPrefab == null && IsDeluxe)
				PLANT_SLOT_BUFFER[PlantID].deluxePlantedPrefab = Prefab;

			if (PLANT_SLOT_BUFFER[PlantID].plantedPrefab != null && PLANT_SLOT_BUFFER[PlantID].deluxePlantedPrefab != null)
			{
				PlantSlotRegistry.RegisterPlantSlot(PLANT_SLOT_BUFFER[PlantID]);
				PLANT_SLOT_BUFFER.Remove(PlantID);
			}
		}

		/// <summary>
		/// Adds a new spawn point
		/// </summary>
		/// <param name="pos">Position to add at</param>
		/// <param name="rot">Rotation to add with</param>
		protected void AddSpawnpoint(Vector3 pos, Vector3 rot)
		{
			SpawnResource spawn = Prefab.GetComponent<SpawnResource>();

			GameObject joint = Object.Instantiate(spawn.SpawnJoints[0].gameObject, pos, Quaternion.Euler(rot), spawn.transform);
			joint.name = "SpawnJoint" + spawn.SpawnJoints.Length.ToString("D2");
		}
	}
}
