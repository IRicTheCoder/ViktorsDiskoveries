using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class to make slime resources
	/// </summary>
	public abstract class SlimeResource : IdentifiableItem
	{
		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "craft";

		/// <summary>The base item to use when creating the one</summary>
		public GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.BUZZ_WAX_CRAFT) ?? SRObjects.Get<GameObject>("craftBuzzWax");

		/// <summary>The mesh of this resource</summary>
		public virtual Mesh Mesh { get; } = SRObjects.Get<Mesh>("waxLump_ld");

		/// <summary>Scale of this resource</summary>
		public override Vector3 Scale => Vector3.one;

		/// <summary>The material of the model</summary>
		public Material ModelMat { get; private set; } = null;

		/// <summary>The scale of the Model</summary>
		public virtual Vector3 ModelScale { get; } = Vector3.one * 0.7f;

		/// <summary>Is this item a resource for the refinery?</summary>
		public override bool IsRefineryResource => true;

		/// <summary>Creates the material for the model</summary>
		public abstract Material CreateModelMat();

		/// <summary>Checks if it is a valid ammo for any storage type</summary>
		public override bool ValidSiloAmmo(SiloStorage.StorageType type)
		{
			return type == SiloStorage.StorageType.NON_SLIMES || type == SiloStorage.StorageType.CRAFTING;
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

			GameObject child = Prefab.FindChild("waxLump_ld");
			child.transform.localScale = ModelScale;

			// Load Components
			Rigidbody body = Prefab.GetComponent<Rigidbody>();
			Vacuumable vac = Prefab.GetComponent<Vacuumable>();
			Identifiable iden = Prefab.GetComponent<Identifiable>();

			MeshFilter filter = child.GetComponent<MeshFilter>();
			MeshRenderer render = child.GetComponent<MeshRenderer>();

			// Setup Components
			body.mass = Mass;
			vac.size = Size;
			iden.id = ID;

			filter.sharedMesh = Mesh;
			render.sharedMaterial = ModelMat;
		}
	}
}
