using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class to make world spawn resources (patches)
	/// </summary>
	public abstract class PatchWorldResource : SpawnResourceItem
	{
		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "patch";

		/// <summary>Is this a garden resource?</summary>
		sealed protected override bool IsGarden => false;

		/// <summary>The base item to use when creating the one</summary>
		protected virtual GameObject BaseItem => SRObjects.Get<GameObject>("patchCarrot02");

		/// <summary>Builds this Item</summary>
		public override void Build()
		{
			// Get GameObjects
			Prefab = PrefabUtils.CopyPrefab(BaseItem);
			Prefab.name = NamePrefix + Name;

			// Load Components
			SpawnResource spawn = Prefab.GetComponent<SpawnResource>();

			// Setup Components
			spawn.id = ID;
			spawn.ObjectsToSpawn = GameContext.Instance.LookupDirector.GetPrefabs(ToSpawn);
			spawn.BonusObjectsToSpawn = BonusToSpawn != null ? GameContext.Instance.LookupDirector.GetPrefabs(BonusToSpawn) : new GameObject[0];
			spawn.MaxObjectsSpawned = MaxObjectsSpawned;
			spawn.MinObjectsSpawned = MinObjectsSpawned;
			spawn.MinNutrientObjectsSpawned = MinNutrientObjectsSpawned == -1 ? MaxObjectsSpawned : MinNutrientObjectsSpawned;
			spawn.MinSpawnIntervalGameHours = MinSpawnIntervalGameHours;
			spawn.MaxSpawnIntervalGameHours = MaxSpawnIntervalGameHours;
			spawn.BonusChance = BonusChance;
			spawn.minBonusSelections = MinBonusSelections;

			// Builds the Spawn Points
			BuildSpawnPoints(spawn);
		}
	}
}
