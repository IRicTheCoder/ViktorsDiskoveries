using System.Collections.Generic;

using SRML;
using SRML.Utils;
using UnityEngine;

namespace Guu.API.Identifiables
{
	/// <summary>
	/// This is the base class to make Veggies or Fruits
	/// </summary>
	public abstract class PlantFood : IdentifiableItem
	{
		// Base item to create this one
		private static GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.POGO_FRUIT) ?? 
		                                      SRObjects.Get<GameObject>("fruitPogo");
		
		// Material for the Model
		public Material ModelMat { get; set; }
		
		// Override
		protected override string NamePrefix => IsFruit ? "fruit" : "veggie";

		// Abstracts
		protected abstract bool IsFruit { get; }
		protected abstract Mesh Mesh { get; }

		// Virtual
		protected virtual bool IsPlantable { get; } = true;
		protected virtual bool VacuumableWhenRipe { get; } = true;

		protected virtual float UnripeGameHours { get; } = 6f;
		protected virtual float RipeGameHours { get; } = 6f;
		protected virtual float EdibleGameHours { get; } = 36f;
		protected virtual float RottenGameHours { get; } = 6f;

		protected virtual Vector3 ModelScale { get; } = Vector3.one * 0.26f;
		
		protected virtual Material RottenMat { get; } = null;

		protected virtual List<Identifiable.Id> IsFavoritedBy { get; } = null;

		// Methods
		protected override bool ValidSiloAmmo(SiloStorage.StorageType type) =>
			type == SiloStorage.StorageType.NON_SLIMES || 
			type == SiloStorage.StorageType.FOOD;

		protected abstract Material CreateModelMat();

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

			// Load Components
			SphereCollider sCol = Prefab.GetComponent<SphereCollider>();
			MeshFilter filter = Prefab.GetComponent<MeshFilter>();
			Rigidbody body = Prefab.GetComponent<Rigidbody>();
			Vacuumable vac = Prefab.GetComponent<Vacuumable>();
			Identifiable iden = Prefab.GetComponent<Identifiable>();

			ResourceCycle cycle = IsPlantable ? Prefab.GetComponent<ResourceCycle>() : null;

			MeshFilter model = child.GetComponent<MeshFilter>();
			MeshRenderer render = child.GetComponent<MeshRenderer>();

			// Setup Components
			sCol.radius = ModelScale.x;
			filter.sharedMesh = Mesh;
			body.mass = Mass;
			vac.size = Size;
			iden.id = ID;

			if (IsPlantable)
			{
				cycle.unripeGameHours = UnripeGameHours;
				cycle.ripeGameHours = RipeGameHours;
				cycle.edibleGameHours = EdibleGameHours;
				cycle.rottenGameHours = RottenGameHours;
				cycle.rottenMat = RottenMat ?? cycle.rottenMat;
				cycle.vacuumableWhenRipe = VacuumableWhenRipe;
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

			SlimeUtils.AddFoodToGroup(ID, IsFruit ? SlimeEat.FoodGroup.FRUIT : SlimeEat.FoodGroup.VEGGIES, IsFavoritedBy);
			SlimeUtils.AddFoodToGroup(ID, SlimeEat.FoodGroup.NONTARRGOLD_SLIMES, IsFavoritedBy);

			return this;
		}
	}
}
