using System.Collections.Generic;
using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class to make gordos
	/// </summary>
	public abstract class Gordo : IdentifiableItem
	{
		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "gordo";

		/// <summary>The base item to use when creating the one</summary>
		public GameObject BaseItem => GameContext.Instance.LookupDirector?.GetGordo(Identifiable.Id.PINK_GORDO) ?? SRObjects.Get<GameObject>("gordoPink");

		/// <summary>Scale of this resource</summary>
		public override Vector3 Scale => Vector3.one * 4;

		/// <summary>The mesh of this meat food</summary>
		public abstract Mesh Mesh { get; }

		/// <summary>List of Favorite Foods</summary>
		public virtual List<Identifiable.Id> FavoriteFoods { get; } = null;

		/// <summary>The definition for this slime (Diets are auto added, it ignores the ones already added)</summary>
		public abstract SlimeDefinition Definition { get; }

		/// <summary>The target count for the gordo to burst</summary>
		public virtual int TargetCount { get; } = 30;

		/// <summary>Rewards to give when bursting</summary>
		public virtual List<Identifiable.Id> Rewards { get; } = null;

		/// <summary>Zones this gordo is native to</summary>
		public virtual ZoneDirector.Zone[] NativeZones { get; } = null;

		/// <summary>Prefab for the map marker for the Rush Mode</summary>
		public virtual MapMarker MarkerPrefab { get; } = null;

		/// <summary>The material of the model</summary>
		private Material ModelMat { get; set; } = null;

		/// <summary>Creates the material for the model</summary>
		public abstract Material CreateModelMat();

		/// <summary>Checks if it is a valid ammo for any storage type</summary>
		public override bool ValidSiloAmmo(SiloStorage.StorageType type) { return false; }

		/// <summary>Builds this Item</summary>
		public override void Build()
		{
			// Load Material
			ModelMat = CreateModelMat();

			// Get GameObjects
			Prefab = PrefabUtils.CopyPrefab(BaseItem);
			Prefab.name = NamePrefix + Name;
			Prefab.transform.localScale = Scale;

			GameObject child = Prefab.transform.Find("Full size/slime_gordo").gameObject;

			// Load Components
			GordoEat eat = Prefab.GetComponent<GordoEat>();
			GordoRewards rew = Prefab.GetComponent<GordoRewards>();

			GordoDisplayOnMap disp = Prefab.GetComponent<GordoDisplayOnMap>();
			GordoIdentifiable iden = Prefab.GetComponent<GordoIdentifiable>();

			MeshFilter filter = Prefab.FindChild("Full size").GetComponent<MeshFilter>();

			SkinnedMeshRenderer render = child.GetComponent<SkinnedMeshRenderer>();

			// Setup Components
			filter.sharedMesh = Mesh;
			eat.slimeDefinition = Definition;
			eat.targetCount = TargetCount;

			List<GameObject> rews = new List<GameObject>();

			if (Rewards != null)
			{
				foreach (Identifiable.Id id in Rewards)
					rews.Add(GameContext.Instance.LookupDirector.GetPrefab(id));
			}

			rew.rewardPrefabs = rews.ToArray();

			iden.id = ID;
			iden.nativeZones = NativeZones;

			disp.markerPrefab = MarkerPrefab ?? disp.markerPrefab;

			render.sharedMaterial = ModelMat;
		}

		/// <summary>Registers the item into it's registry</summary>
		public override IdentifiableItem Register()
		{
			base.Register();

			LookupRegistry.RegisterGordo(Prefab);

			return this;
		}
	}
}
