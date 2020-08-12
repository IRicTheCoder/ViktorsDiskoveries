using Guu.Utils;

using SRML;
using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace Guu.API.Identifiables
{
	/// <summary>
	/// This is the base class to make toys
	/// </summary>
	public abstract class Toy : IdentifiableItem
	{
		// Base item to create this one
		private static GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.BEACH_BALL_TOY) ?? 
		                                      SRObjects.Get<GameObject>("toyBeachBall");
		
		// Material for the Model
		private Material ModelMat { get; set; }
		
		// Overrides
		protected override string NamePrefix => "toy";
		protected override Vector3 Scale => Vector3.one * 1.3f;
		protected override Vacuumable.Size Size { get; } = Vacuumable.Size.LARGE;
		
		// Virtual
		protected virtual int Cost { get; } = 200;
		protected virtual Vector3 ModelScale { get; } = Vector3.one * 0.5f;
		protected virtual Mesh Mesh { get; } = SRObjects.Get<Mesh>("beachBall_ld");

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
