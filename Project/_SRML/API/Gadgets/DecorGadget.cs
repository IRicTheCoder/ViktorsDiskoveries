using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class to make decoration gadgets
	/// </summary>
	public abstract class DecorGadget : GadgetItem
	{
		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "decor";

		/// <summary>The base prefab to turn into a decoration gadget</summary>
		public abstract GameObject BasePrefab { get; }

		/// <summary>The classification of this gadget</summary>
		public override GadgetRegistry.GadgetClassification Classification => GadgetRegistry.GadgetClassification.DECO;

		/// <summary>Builds this Item</summary>
		public override void Build()
		{
			// Get GameObjects
			Prefab = PrefabUtils.CopyPrefab(BasePrefab);
			Prefab.name = NamePrefix + Name;

			// Load Components
			Gadget gadget = Prefab.AddComponent<Gadget>();

			// Setup Components
			gadget.id = ID;
		}
	}
}
