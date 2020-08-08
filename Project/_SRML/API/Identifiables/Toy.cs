using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class to make toys
	/// </summary>
	public abstract class Toy : IdentifiableItem
	{
		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "toy";

		/// <summary>The base item to use when creating the one</summary>
		public GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.BEACH_BALL_TOY) ?? SRObjects.Get<GameObject>("toyBeachBall");

		/// <summary>The size of the vacuumable</summary>
		public override Vacuumable.Size Size { get; } = Vacuumable.Size.LARGE;

		/// <summary>The mesh of this resource</summary>
		public virtual Mesh Mesh { get; } = SRObjects.Get<Mesh>("beachBall_ld");

		/// <summary>Scale of this resource</summary>
		public override Vector3 Scale => Vector3.one * 1.3f;

		/// <summary>The material of the model</summary>
		public Material ModelMat { get; private set; } = null;

		/// <summary>The scale of the Model</summary>
		public virtual Vector3 ModelScale { get; } = Vector3.one * 0.5f;

		/// <summary>The cost of this toy in the market</summary>
		public virtual int Cost { get; } = 200;

		/// <summary>Creates the material for the model</summary>
		public abstract Material CreateModelMat();

		/// <summary>Builds this Item</summary>
		public override void Build()
		{
			// Load Material
			ModelMat = CreateModelMat();

			// Get GameObjects
			Prefab = PrefabUtils.CopyPrefab(BaseItem);
			Prefab.name = NamePrefix + Name;
			Prefab.transform.localScale = Scale;

			GameObject child = Prefab.FindChild("beachBall_ld");
			child.transform.localScale = ModelScale;

			// Load Components
			SphereCollider col = Prefab.GetComponent<SphereCollider>();
			Rigidbody body = Prefab.GetComponent<Rigidbody>();
			Vacuumable vac = Prefab.GetComponent<Vacuumable>();
			Identifiable iden = Prefab.GetComponent<Identifiable>();

			MeshFilter filter = child.GetComponent<MeshFilter>();
			MeshRenderer render = child.GetComponent<MeshRenderer>();

			// Setup Components
			col.radius = ModelScale.x;
			body.mass = Mass;
			vac.size = Size;
			iden.id = ID;

			filter.sharedMesh = Mesh;
			render.sharedMaterial = ModelMat;
		}

		/// <summary>Registers the item into it's registry</summary>
		public override IdentifiableItem Register()
		{
			base.Register();

			LookupRegistry.RegisterToy(ScriptableObjectsUtils.CreateToyDefinition(ID, Icon, Cost));

			return this;
		}
	}
}
