using System.Collections.Generic;

using SRML;
using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace Guu.API.Identifiables
{
	/// <summary>
	/// This is the base class to make gordos
	/// </summary>
	public abstract class Gordo : IdentifiableItem
	{
		// Base item to create this one
		private static GameObject BaseItem => GameContext.Instance.LookupDirector?.GetGordo(Identifiable.Id.PINK_GORDO) ??
		                                      SRObjects.Get<GameObject>("gordoPink");
		
		// Material for the Model
		private Material ModelMat { get; set; }
		
		// Overrides
		protected override string NamePrefix => "gordo";
		protected override Vector3 Scale => Vector3.one * 4;

		// Abstracts
		protected abstract Mesh Mesh { get; }
		protected abstract SlimeDefinition Definition { get; }

		// Virtual
		protected virtual int TargetCount { get; } = 30;
		
		protected virtual MapMarker MarkerPrefab { get; } = null;
		
		protected virtual ZoneDirector.Zone[] NativeZones { get; } = null;
		
		public virtual List<Identifiable.Id> FavoriteFoods { get; } = null;
		protected virtual List<Identifiable.Id> Rewards { get; } = null;

		// Methods
		protected abstract Material CreateModelMat();

		protected override bool ValidSiloAmmo(SiloStorage.StorageType type) { return false; }

		protected override void Build()
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

			disp.markerPrefab = MarkerPrefab ? MarkerPrefab : disp.markerPrefab;

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
