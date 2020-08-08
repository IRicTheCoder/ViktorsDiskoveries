using System.Collections.Generic;
using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class for all purchasable UIs
	/// </summary>
	public abstract class PurchasableUIItem : UIItem
	{
		/// <summary>The prefab for this item</summary>
		public GameObject Prefab { get; protected set; }

		/// <summary>The name prefix for this object</summary>
		protected virtual string NamePrefix { get; } = "ui";

		/// <summary>The UI Component type to add to the object</summary>
		public abstract System.Type UIType { get; }

		/// <summary>The base item to use when creating the one</summary>
		public GameObject BaseItem => SRObjects.Get<GameObject>("EmptyPlotUI");

		/// <summary>Builds this Item</summary>
		public override void Build()
		{
			// Create Prefab
			Prefab = PrefabUtils.CopyPrefab(BaseItem);

			// Fix Components
			Object.Destroy(Prefab.GetComponent<EmptyPlotUI>());
			Prefab.AddComponent(UIType);
		}

		/// <summary>Registers the item into it's registry</summary>
		public override UIItem Register()
		{
			Build();

			return this;
		}

		/// <summary>
		/// A custom plot ui, that allows for full control
		/// </summary>
		public class CustomPlotUI : LandPlotUI
		{
			/// <summary>The ID of this plot</summary>
			public virtual LandPlot.Id ID { get; } = LandPlot.Id.NONE;

			/// <summary>The Icon for this plot</summary>
			public virtual Sprite Icon { get; } = null;

			/// <summary>The List of Purchasables</summary>
			public List<PurchaseUI.Purchasable> Purchasables { get; } = new List<PurchaseUI.Purchasable>();

			/// <summary>Should the cost of the purchasables be hidden?</summary>
			public virtual bool HideNewBucksCost { get; } = false;

			/// <summary>A method to populate the UI</summary>
			public virtual void PopulateUI() { }

			/// <summary>Creates the Purchase UI</summary>
			protected override GameObject CreatePurchaseUI()
			{
				PopulateUI();
				//return SRSingleton<GameContext>.Instance.UITemplates.CreatePurchaseUI(Icon ?? SRObjects.MissingIcon, MessageUtil.Qualify("ui", $"t.{ID.ToString().ToLower()}"), Purchasables.ToArray(), HideNewBucksCost, new PurchaseUI.OnClose(Close), false);
				return CreatePurchaseUI(Icon ?? SRObjects.MissingIcon, MessageUtil.Qualify("ui", $"t.{ID.ToString().ToLower()}"), Purchasables.ToArray(), HideNewBucksCost, new PurchaseUI.OnClose(Close), false);
			}

			// TODO: Remove this, fix UITemplatesPatch
			public GameObject CreatePurchaseUI(Sprite titleIcon, string titleKey, PurchaseUI.Purchasable[] purchasables, bool hideNubuckCost, PurchaseUI.OnClose onClose, bool unavailInMainList = false)
			{
				GameObject gameObject = Instantiate(GameContext.Instance.UITemplates.purchaseUIPrefab);
				PurchaseUI component = gameObject.GetComponent<PurchaseUI>();
				component.Init(titleIcon, titleKey, onClose);
				foreach (PurchaseUI.Purchasable purchasable in purchasables)
				{
					component.AddButton(purchasable, unavailInMainList);
				}
				if (hideNubuckCost)
				{
					component.HideNubuckCost();
				}
				component.SelectFirst();
				return gameObject;
			}
		}
	}
}
