using System.Collections.Generic;

using Guu.Utils;

using SRML;
using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace Guu.API.Identifiables
{
	/// <summary>
	/// This is the base class to make special foods like tofu
	/// </summary>
	public abstract class SpecialFood : IdentifiableItem
	{
		// Base item to create this one
		private static GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.SPICY_TOFU) ?? 
		                                      SRObjects.Get<GameObject>("tofuSpicy");
		
		// Material for the Model
		private Material ModelMat { get; set; }
		
		// Overrides
		protected override string NamePrefix => IsTofu ? "tofu" : "special";

		// Abstracts
		protected abstract bool IsTofu { get; }
		protected abstract Mesh Mesh { get; }
		
		// Virtual
		protected virtual bool IsPlantable { get; } = true;
		protected virtual bool VacuumableWhenRipe { get; } = true;
		
		protected virtual float UnripeGameHours { get; } = 6f;
		protected virtual float RipeGameHours { get; } = 6f;
		protected virtual float EdibleGameHours { get; } = 36f;
		protected virtual float RottenGameHours { get; } = 6f;
		
		protected virtual Vector3 ModelScale { get; } = Vector3.one * 0.2f;
		
		protected virtual Material RottenMat { get; } = null;
		
		protected virtual List<Identifiable.Id> IsFavoritedBy { get; } = null;

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

			GameObject child = Prefab.FindChild("model_pogofruit");
			child.transform.localScale = ModelScale;

			Object.Destroy(Prefab.GetComponent<BoxCollider>());

			// Load Components
			SphereCollider col = Prefab.AddComponent<SphereCollider>();
			MeshFilter filter = Prefab.GetComponent<MeshFilter>();
			Rigidbody body = Prefab.GetComponent<Rigidbody>();
			Vacuumable vac = Prefab.GetComponent<Vacuumable>();
			Identifiable iden = Prefab.GetComponent<Identifiable>();

			ResourceCycle cycle = IsPlantable ? Prefab.GetComponent<ResourceCycle>() : null;

			MeshFilter model = child.GetComponent<MeshFilter>();
			MeshRenderer render = child.GetComponent<MeshRenderer>();

			// Setup Components
			col.radius = ModelScale.x;
			filter.sharedMesh = Mesh;
			body.mass = Mass;
			vac.size = Size;
			iden.id = ID;

			if (IsPlantable)
			{
				if (cycle != null)
				{
					cycle.unripeGameHours    = UnripeGameHours;
					cycle.ripeGameHours      = RipeGameHours;
					cycle.edibleGameHours    = EdibleGameHours;
					cycle.rottenGameHours    = RottenGameHours;
					cycle.rottenMat          = RottenMat ? RottenMat : cycle.rottenMat;
					cycle.vacuumableWhenRipe = VacuumableWhenRipe;
				}
			}
			else
				Object.Destroy(Prefab.GetComponent<ResourceCycle>());

			model.sharedMesh = Mesh;
			render.sharedMaterial = ModelMat;
		}

		/// <summary>Registers the item into it's registry</summary>
		public override IdentifiableItem Register()
		{
			base.Register();

			SlimeUtils.AddFoodToGroup(ID, SlimeEat.FoodGroup.NONTARRGOLD_SLIMES, IsFavoritedBy);

			return this;
		}
	}
}
