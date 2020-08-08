using SRML;
using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace Guu.API.Identifiables
{
	/// <summary>
	/// This is the base class to make plorts
	/// </summary>
	public abstract class Plort : IdentifiableItem
	{
		// Base item to create this one
		private static GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.PINK_PLORT) ?? 
		                                      SRObjects.Get<GameObject>("plortPink");
		
		// Material for the Model
		private Material ModelMat { get; set; }
		
		// Overrides
		protected override string NamePrefix => "plort";
		protected override Vector3 Scale => Vector3.one * 0.3f;

		// Virtual
		protected virtual bool CanBeSold { get; } = false;
		
		protected virtual float LifetimeHours { get; } = 0;
		protected virtual float MarketValue { get; } = 0f;
		protected virtual float MarketSaturationValue { get; } = 0f;
		
		protected virtual Mesh Mesh { get; } = SRObjects.Get<Mesh>("plort");

		// Methods
		protected abstract Material CreateModelMat();

		protected override bool ValidSiloAmmo(SiloStorage.StorageType type) =>
			type == SiloStorage.StorageType.NON_SLIMES || 
			type == SiloStorage.StorageType.PLORT;

		protected override void Build()
		{
			// Load Material
			ModelMat = CreateModelMat();

			// Get GameObjects
			Prefab = PrefabUtils.CopyPrefab(BaseItem);
			Prefab.name = NamePrefix + Name;
			Prefab.transform.localScale = Scale;

			// Load Components
			MeshFilter filter = Prefab.GetComponent<MeshFilter>();
			MeshRenderer render = Prefab.GetComponent<MeshRenderer>();
			Rigidbody body = Prefab.GetComponent<Rigidbody>();
			Vacuumable vac = Prefab.GetComponent<Vacuumable>();
			Identifiable iden = Prefab.GetComponent<Identifiable>();
			PlortInstability inst = Prefab.GetComponent<PlortInstability>();

			// Setup Components
			filter.sharedMesh = Mesh;
			render.sharedMaterial = ModelMat;
			body.mass = Mass;
			vac.size = Size;
			iden.id = ID;
			inst.lifetimeHours = LifetimeHours;
		}

		/// <summary>Registers the item into it's registry</summary>
		public override IdentifiableItem Register()
		{
			base.Register();

			if (CanBeSold)
				PlortRegistry.RegisterPlort(ID, MarketValue, MarketSaturationValue);

			return this;
		}
	}
}
