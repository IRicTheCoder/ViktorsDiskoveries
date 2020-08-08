using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class to make liquids
	/// </summary>
	public abstract class Liquid : IdentifiableItem
	{
		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "liquid";

		/// <summary>The base item to use when creating the one</summary>
		public GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.WATER_LIQUID) ?? SRObjects.Get<GameObject>("WaterIdentifiable");

		/// <summary>The mesh of this resource</summary>
		public virtual Mesh Mesh { get; } = SRObjects.Get<Mesh>("slimeglop");

		/// <summary>Scale of this resource</summary>
		public override Vector3 Scale => Vector3.one;

		/// <summary>The material of the model</summary>
		public Material ModelMat { get; private set; } = null;

		/// <summary>The scale of the Model</summary>
		public virtual Vector3 ModelScale { get; } = Vector3.one * 1.5f;

		/// <summary>The effect when vacced in</summary>
		public virtual GameObject InFX { get; } = null;

		/// <summary>The effect when vaccing fails</summary>
		public virtual GameObject VacFailFX { get; } = null;

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

			GameObject child = Prefab.FindChild("Sphere");
			child.transform.localScale = ModelScale;

			GameObject cPart = child.FindChild("FX Water Glops");

			// Load Components
			Rigidbody body = Prefab.GetComponent<Rigidbody>();
			Vacuumable vac = Prefab.GetComponent<Vacuumable>();
			Identifiable iden = Prefab.GetComponent<Identifiable>();
			DestroyOnTouching dest = Prefab.GetComponent<DestroyOnTouching>();

			MeshFilter filter = child.GetComponent<MeshFilter>();
			MeshRenderer render = child.GetComponent<MeshRenderer>();

			ParticleSystemRenderer part = cPart.GetComponent<ParticleSystemRenderer>();

			// Setup Components
			body.mass = Mass;
			vac.size = Size;
			iden.id = ID;

			dest.liquidType = ID;
			dest.destroyFX = PrefabUtils.CopyPrefab(dest.destroyFX);

			filter.sharedMesh = Mesh;
			render.sharedMaterial = ModelMat;

			part.mesh = Mesh;
			part.sharedMaterial = ModelMat;

			// Setup After effect
			GameObject cPart2 = dest.destroyFX.FindChild("Water Glops");

			ParticleSystemRenderer part2 = cPart2.GetComponent<ParticleSystemRenderer>();

			part2.mesh = Mesh;
			part2.sharedMaterial = ModelMat;
		}

		/// <summary>Registers the item into it's registry</summary>
		public override IdentifiableItem Register()
		{
			base.Register();

			LookupRegistry.RegisterLiquid(ScriptableObjectsUtils.CreateLiquidDefinition(ID, InFX, VacFailFX));

			return this;
		}
	}
}
