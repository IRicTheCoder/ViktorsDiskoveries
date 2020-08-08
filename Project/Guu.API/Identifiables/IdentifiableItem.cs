using SRML;
using SRML.SR;
using UnityEngine;

namespace Guu.API.Identifiables
{
	/// <summary>
	/// This is the base class for all Identifiable Items
	/// </summary>
	public abstract class IdentifiableItem : RegistryItem<IdentifiableItem>
	{
		// Instance Bound
		protected GameObject Prefab { get; set; }
		protected VacItemDefinition VacEntry { get; set; }

		// Abstracts
		protected abstract string NamePrefix { get; }
		public abstract Identifiable.Id ID { get; }

		// Virtual
		protected virtual Sprite Icon { get; } = null;
		protected virtual Color Color { get; } = Color.clear;
		protected virtual Vacuumable.Size Size { get; } = Vacuumable.Size.NORMAL;
		protected virtual Vector3 Scale { get; } = Vector3.one;
		protected virtual float Mass { get; } = 0.3f;
		protected virtual bool IsVacuumableOverride { get; } = false;
		protected virtual bool IsRefineryResource { get; } = false;
		
		// Methods
		protected virtual bool ValidSiloAmmo(SiloStorage.StorageType type) => type == SiloStorage.StorageType.NON_SLIMES;

		/// <summary>Registers the item into it's registry</summary>
		public override IdentifiableItem Register()
		{
			Build();

			LookupRegistry.RegisterIdentifiablePrefab(Prefab);

			VacEntry = VacItemDefinition.CreateVacItemDefinition(ID, Color, Icon ? Icon : SRObjects.MissingIcon);
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
		/// <param name="id">The pedia ID to register</param>
		/// <param name="cat">The pedia category to add this in</param>
		public IdentifiableItem AddPediaMapping(PediaDirector.Id id, PediaRegistry.PediaCategory cat = PediaRegistry.PediaCategory.RESOURCES)
		{
			PediaRegistry.RegisterIdentifiableMapping(id, ID);
			PediaRegistry.RegisterIdEntry(id, Icon);
			PediaRegistry.SetPediaCategory(id, cat);

			return this;
		}
	}
}
