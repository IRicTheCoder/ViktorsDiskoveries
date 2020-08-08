using SRML;
using SRML.Utils;
using UnityEngine;

namespace Guu.API.Identifiables
{
	/// <summary>
	/// This is the base class to make other items
	/// </summary>
	public abstract class Other : IdentifiableItem
	{
		// Base item to create this one
		private static GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.BUZZ_WAX_CRAFT) ?? 
		                                      SRObjects.Get<GameObject>("craftBuzzWax");
		
		// Material for the Model
		private Material ModelMat { get; set; }
		
		// Overrides
		protected override string NamePrefix => "other";
		protected override Vector3 Scale => Vector3.one;

		// Virtual 
		protected virtual Mesh Mesh { get; } = SRObjects.Get<Mesh>("waxLump_ld");
		protected virtual Vector3 ModelScale { get; } = Vector3.one * 0.7f;

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
