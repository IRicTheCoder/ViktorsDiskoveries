using System.Collections.Generic;

using SRML;
using SRML.Utils;
using UnityEngine;

namespace Guu.API.Identifiables
{
	/// <summary>
	/// This is the base class to make chicks
	/// </summary>
	public abstract class BirdBaby : IdentifiableItem
	{
		// Base item to create this one
		private static GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.CHICK) ?? 
		                                      SRObjects.Get<GameObject>("birdChick");
		
		// Material for the Model
		private Material ModelMat { get; set; }
		
		// Overrides
		protected override string NamePrefix => "bird";
		protected override Vector3 Scale => Vector3.one * 0.5f;

		// Abstracts
		protected abstract Mesh Mesh { get; }
		
		// Virtual
		protected virtual float DelayGameHours { get; } = 6f;
		protected virtual float EggPeriod { get; } = 3;
		
		protected virtual Vector3 ModelScale { get; } = Vector3.one * 0.3f;
		
		protected virtual List<Identifiable.Id> IsFavoritedBy { get; } = null;
		protected virtual List<TransformAfterTime.TransformOpt> Options { get; } = new List<TransformAfterTime.TransformOpt>();
		
		// Methods
		protected abstract Material CreateModelMat();
		
		protected override bool ValidSiloAmmo(SiloStorage.StorageType type) =>
			type == SiloStorage.StorageType.NON_SLIMES || 
			type == SiloStorage.StorageType.FOOD;

		protected override void Build()
		{
			// Load Material
			ModelMat = CreateModelMat();

			// Get GameObjects
			Prefab = PrefabUtils.CopyPrefab(BaseItem);
			Prefab.name = NamePrefix + Name;
			Prefab.transform.localScale = Scale;

			GameObject child = Prefab.FindChild("Body");
			child.transform.localScale = ModelScale;

			GameObject child2 = Prefab.transform.Find("Chickadoo/mesh_body1").gameObject;

			// Load Components
			Rigidbody body = Prefab.GetComponent<Rigidbody>();
			Vacuumable vac = Prefab.GetComponent<Vacuumable>();
			Identifiable iden = Prefab.GetComponent<Identifiable>();

			EggActivator egg = Prefab.GetComponent<EggActivator>();
			TransformAfterTime trans = Prefab.GetComponent<TransformAfterTime>();

			MeshFilter model = child.GetComponent<MeshFilter>();
			SkinnedMeshRenderer mRender = child2.GetComponent<SkinnedMeshRenderer>();

			// Setup Components
			body.mass = Mass;
			vac.size = Size;
			iden.id = ID;

			trans.delayGameHours = DelayGameHours;
			trans.options = Options;

			egg.eggPeriod = EggPeriod;

			model.sharedMesh = Mesh;
			mRender.sharedMaterial = ModelMat;

			foreach (MeshRenderer render in Prefab.transform.Find("Chickadoo/root").GetComponentsInChildren<MeshRenderer>())
			{
				if (render.sharedMaterial.name.Equals("Chickadoo"))
				{
					render.sharedMaterial = ModelMat;
				}
			}
		}

		/// <summary>Registers the item into it's registry</summary>
		public override IdentifiableItem Register()
		{
			base.Register();

			SlimeUtils.AddFoodToGroup(ID, SlimeEat.FoodGroup.MEAT, IsFavoritedBy);
			SlimeUtils.AddFoodToGroup(ID, SlimeEat.FoodGroup.NONTARRGOLD_SLIMES, IsFavoritedBy);

			return this;
		}
	}
}
