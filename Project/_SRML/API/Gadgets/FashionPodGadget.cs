using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class to make decoration gadgets
	/// </summary>
	public abstract class FashionPodGadget : GadgetItem
	{
		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "gadgetFashionPod";

		/// <summary>The base item to use when creating the one</summary>
		public GameObject BaseItem => GameContext.Instance.LookupDirector?.GetGadgetDefinition(Gadget.Id.FASHION_POD_CLIP_ON).prefab ?? SRObjects.Get<GameObject>("gadgetFashionPodClipOn");

		/// <summary>The Fashion ID for the fashion used by this pod</summary>
		public abstract Identifiable.Id FashionID { get; }

		/// <summary>The classification of this gadget</summary>
		public override GadgetRegistry.GadgetClassification Classification => GadgetRegistry.GadgetClassification.FASHION_POD;

		/// <summary>Builds this Item</summary>
		public override void Build()
		{
			// Get GameObjects
			Prefab = PrefabUtils.CopyPrefab(BaseItem);
			Prefab.name = NamePrefix + Name;

			// Load Components
			Gadget gadget = Prefab.GetComponent<Gadget>();
			FashionPod pod = Prefab.GetComponent<FashionPod>();

			// Setup Components
			gadget.id = ID;
			pod.fashionId = FashionID;
		}

		/// <summary>Registers the item into it's registry</summary>
		public override GadgetItem Register()
		{
			base.Register();

			return this;
		}
	}
}
