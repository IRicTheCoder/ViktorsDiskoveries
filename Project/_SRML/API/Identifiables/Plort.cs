using System.Collections.Generic;
using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class to make plorts
	/// </summary>
	public abstract class Plort : IdentifiableItem
	{
		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "plort";

		/// <summary>The base item to use when creating the one</summary>
		public GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.PINK_PLORT) ?? SRObjects.Get<GameObject>("plortPink");

		/// <summary>The mesh of this plort</summary>
		public virtual Mesh Mesh { get; } = SRObjects.Get<Mesh>("plort");

		/// <summary>Scale of this plort</summary>
		public override Vector3 Scale => Vector3.one * 0.3f;

		/// <summary>The material of the model</summary>
		public Material ModelMat { get; private set; } = null;

		/// <summary>The lifetime of the plort in Game Hours</summary>
		public virtual float LifetimeHours { get; }

		/// <summary>Can this plort be sold?</summary>
		public virtual bool CanBeSold { get; } = false;

		/// <summary>The value in the market</summary>
		public virtual float MarketValue { get; } = 0f;

		/// <summary>The full saturation value in the market</summary>
		public virtual float MarketSaturationValue { get; } = 0f;

		/// <summary>Creates the material for the model</summary>
		public abstract Material CreateModelMat();

		/// <summary>Checks if it is a valid ammo for any storage type</summary>
		public override bool ValidSiloAmmo(SiloStorage.StorageType type)
		{
			return type == SiloStorage.StorageType.NON_SLIMES || type == SiloStorage.StorageType.PLORT;
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
