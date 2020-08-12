using Guu.Utils;

using SRML;
using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace Guu.API.Identifiables
{
	/// <summary>
	/// This is the base class to make liquids
	/// </summary>
	public abstract class Liquid : IdentifiableItem
	{
		// Base item to create this one
		private static GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.WATER_LIQUID) ?? 
		                                      SRObjects.Get<GameObject>("WaterIdentifiable");
		
		// Material for the Model
		private Material ModelMat { get; set; }
		
		// Overrides
		protected override string NamePrefix => "liquid";
		protected override Vector3 Scale => Vector3.one;

		// Virtual
		protected virtual Mesh Mesh { get; } = SRObjects.Get<Mesh>("slimeglop");

		protected virtual Vector3 ModelScale { get; } = Vector3.one * 1.5f;

		protected virtual GameObject InFx { get; } = null;
		protected virtual GameObject VacFailFx { get; } = null;

		// Methods
		protected abstract Material CreateModelMat();

		protected override void Build()
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

			LookupRegistry.RegisterLiquid(ScriptableObjectsUtils.CreateLiquidDefinition(ID, InFx, VacFailFx));

			return this;
		}
	}
}
