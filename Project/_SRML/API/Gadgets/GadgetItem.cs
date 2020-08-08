using System.Collections.Generic;
using SRML.SR;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class for all gadget items
	/// </summary>
	public abstract class GadgetItem : RegistryItem<GadgetItem>
	{
		/// <summary>The name prefix for this object</summary>
		protected abstract string NamePrefix { get; }

		/// <summary>The prefab from this item</summary>
		public GameObject Prefab { get; protected set; }

		/// <summary>The vac entry from this item</summary>
		public GadgetDefinition Definition { get; protected set; }

		/// <summary>The ID for this object</summary>
		public abstract Gadget.Id ID { get; }

		/// <summary>The Pedia ID for this object</summary>
		public abstract PediaDirector.Id PediaID { get; }

		/// <summary>The icon for this object</summary>
		public virtual Sprite Icon { get; } = null;

		/// <summary>The cost of the blueprint</summary>
		public virtual int BlueprintCost { get; } = 0;

		/// <summary>The costs of crafting this gadget</summary>
		public virtual GadgetDefinition.CraftCost[] CraftCosts { get; } = null;

		/// <summary>The limit of gadgets one can buy</summary>
		public virtual int BuyLimit { get; } = 0;

		/// <summary>The limit of gadgets one can place</summary>
		public virtual int CountLimit { get; } = 0;

		/// <summary>The IDs of gadgets that count for the limits of this one</summary>
		public virtual Gadget.Id[] CountIDs { get; } = null;

		/// <summary>Should this gadget get destroyed when removed?</summary>
		public virtual bool DestroyOnRemoval { get; } = false;

		/// <summary>Should this gadget be built in pairs?</summary>
		public virtual bool BuyInPairs { get; } = false;

		/// <summary>Should the blueprint for this gadget be available from the start?</summary>
		public virtual bool StartAvailable { get; } = false;

		/// <summary>Is the blueprint for this gadget default or is it given later on?</summary>
		public virtual bool IsDefault { get; } = false;

		/// <summary>The classification of this gadget</summary>
		public virtual GadgetRegistry.GadgetClassification Classification { get; } = GadgetRegistry.GadgetClassification.MISC;

		/// <summary>Creates a lock for the gadget</summary>
		public virtual GadgetDirector.BlueprintLocker CreateBlueprintLocker(GadgetDirector dir) { return null; }

		/// <summary>Registers the item into it's registry</summary>
		public override GadgetItem Register()
		{
			Definition = ScriptableObject.CreateInstance<GadgetDefinition>();
			Definition.id = ID;
			Definition.prefab = Prefab;
			Definition.icon = Icon;
			Definition.pediaLink = PediaID;
			Definition.blueprintCost = BlueprintCost;
			Definition.craftCosts = CraftCosts;
			Definition.buyCountLimit = BuyLimit;
			Definition.countLimit = CountLimit;
			Definition.countOtherIds = CountIDs;
			Definition.destroyOnRemoval = DestroyOnRemoval;
			Definition.buyInPairs = BuyInPairs;

			Build();

			if (IsDefault)
				GadgetRegistry.RegisterDefaultBlueprint(ID);

			if (StartAvailable)
				GadgetRegistry.RegisterDefaultAvailableBlueprint(ID);
			else
				GadgetRegistry.RegisterBlueprintLock(ID, CreateBlueprintLocker);

			GadgetRegistry.ClassifyGadget(ID, Classification);
			LookupRegistry.RegisterGadget(Definition);

			return this;
		}

		/// <summary>Creates a Craft Cost Definition</summary>
		protected GadgetDefinition.CraftCost CraftCost(Identifiable.Id ID, int amount)
		{
			return new GadgetDefinition.CraftCost()
			{
				id = ID,
				amount = amount >= 999 ? 999 : amount
			};
		}
	}
}
