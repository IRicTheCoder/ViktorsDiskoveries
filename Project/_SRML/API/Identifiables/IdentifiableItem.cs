using SRML.SR;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class for all Identifiable Items
	/// </summary>
	public abstract class IdentifiableItem : RegistryItem<IdentifiableItem>
	{
		/// <summary>The prefab from this item</summary>
		public GameObject Prefab { get; protected set; }

		/// <summary>The vac entry from this item</summary>
		public VacItemDefinition VacEntry { get; protected set; }

		/// <summary>The name prefix for this object</summary>
		protected abstract string NamePrefix { get; }

		/// <summary>The Identifiable ID of this plant food</summary>
		public abstract Identifiable.Id ID { get; }

		/// <summary>The icon for the Vac Entry</summary>
		public virtual Sprite Icon { get; } = null;

		/// <summary>The color for the Vac Entry</summary>
		public virtual Color Color { get; } = Color.clear;

		/// <summary>The size of the vacuumable</summary>
		public virtual Vacuumable.Size Size { get; } = Vacuumable.Size.NORMAL;

		/// <summary>The scale of the object</summary>
		public virtual Vector3 Scale { get; } = Vector3.one;

		/// <summary>The mass of this object</summary>
		public virtual float Mass { get; } = 0.3f;

		/// <summary>Makes an item vacuumable independent of their vac size</summary>
		public virtual bool IsVacuumableOverride { get; } = false;

		/// <summary>Is this item a resource for the refinery?</summary>
		public virtual bool IsRefineryResource { get; } = false;

		/// <summary>Checks if it is a valid ammo for any storage type</summary>
		public virtual bool ValidSiloAmmo(SiloStorage.StorageType type) { return type == SiloStorage.StorageType.NON_SLIMES; }

		/// <summary>Registers the item into it's registry</summary>
		public override IdentifiableItem Register()
		{
			Build();

			LookupRegistry.RegisterIdentifiablePrefab(Prefab);

			VacEntry = VacItemDefinition.CreateVacItemDefinition(ID, Color, Icon ?? SRObjects.MissingIcon);
			LookupRegistry.RegisterVacEntry(VacEntry);

			if (IsVacuumableOverride || Size == Vacuumable.Size.NORMAL)
				AmmoRegistry.RegisterPlayerAmmo(PlayerState.AmmoMode.DEFAULT, ID);

			if (IsRefineryResource)
				AmmoRegistry.RegisterRefineryResource(ID);

			AmmoRegistry.RegisterSiloAmmo(ValidSiloAmmo, ID);

			return this;
		}

		/// <summary>
		/// Adds Pedia mappings
		/// </summary>
		/// <param name="ID">The pedia ID to register</param>
		/// <param name="cat">The pedia category to add this in</param>
		public IdentifiableItem AddPediaMapping(PediaDirector.Id ID, PediaRegistry.PediaCategory cat = PediaRegistry.PediaCategory.RESOURCES)
		{
			PediaRegistry.RegisterIdentifiableMapping(ID, this.ID);
			PediaRegistry.RegisterIdEntry(ID, Icon);
			PediaRegistry.SetPediaCategory(ID, cat);

			return this;
		}
	}
}
