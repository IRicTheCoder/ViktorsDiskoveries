using System.Collections.Generic;
using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class to make chicks
	/// </summary>
	public abstract class BirdBaby : IdentifiableItem
	{
		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "bird";

		/// <summary>The base item to use when creating the one</summary>
		public GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.CHICK) ?? SRObjects.Get<GameObject>("birdChick");

		/// <summary>Which slimes have this food as it's favorite</summary>
		public virtual List<Identifiable.Id> IsFavoritedBy { get; } = null;

		/// <summary>The mesh of this meat food</summary>
		public abstract Mesh Mesh { get; }

		/// <summary>The material of the model</summary>
		public Material ModelMat { get; private set; } = null;

		/// <summary>The scale of the object</summary>
		public override Vector3 Scale => Vector3.one * 0.5f;

		/// <summary>The game hours to turn into adult</summary>
		public virtual float DelayGameHours { get; } = 6f;

		/// <summary>The list of transform options when becoming an adult</summary>
		public virtual List<TransformAfterTime.TransformOpt> Options { get; } = new List<TransformAfterTime.TransformOpt>();

		/// <summary>Time to hatch the egg</summary>
		public virtual float EggPeriod { get; } = 3;

		/// <summary>The scale of the Model</summary>
		public virtual Vector3 ModelScale { get; } = Vector3.one * 0.3f;

		/// <summary>Creates the material for the model</summary>
		public abstract Material CreateModelMat();

		/// <summary>Checks if it is a valid ammo for any storage type</summary>
		public override bool ValidSiloAmmo(SiloStorage.StorageType type)
		{
			return type == SiloStorage.StorageType.NON_SLIMES || type == SiloStorage.StorageType.FOOD;
		}

		/// <summary>Builds this Item</summary>
		public override void Build()
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
