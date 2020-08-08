using System.Collections.Generic;

using SRML;
using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace Guu.API.UI
{
	/// <summary>
	/// This is the base class for all purchasable UIs
	/// </summary>
	public abstract class PurchasableUIItem : UIItem
	{
		// Base item to create this one
		private static GameObject BaseItem => SRObjects.Get<GameObject>("EmptyPlotUI");
		
		// Instance Bound
		public GameObject Prefab { get; private set; }

		// Abstracts
		protected abstract System.Type UIType { get; }
		
		// Virtual
		protected virtual string NamePrefix { get; } = "ui";

		// Methods
		protected override void Build()
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
			// Instance Bound
			protected List<PurchaseUI.Purchasable> Purchasables { get; } = new List<PurchaseUI.Purchasable>();
			
			// Virtual
			protected virtual bool HideNewBucksCost { get; } = false;
			protected virtual Sprite Icon { get; } = null;
			protected virtual LandPlot.Id ID { get; } = LandPlot.Id.NONE;

			// Methods
			protected virtual void PopulateUI() { }

			/// <summary>Creates the Purchase UI</summary>
			protected override GameObject CreatePurchaseUI()
			{
				PopulateUI();
				//return SRSingleton<GameContext>.Instance.UITemplates.CreatePurchaseUI(Icon ?? SRObjects.MissingIcon, MessageUtil.Qualify("ui", $"t.{ID.ToString().ToLower()}"), Purchasables.ToArray(), HideNewBucksCost, new PurchaseUI.OnClose(Close), false);
				return CreatePurchaseUI(Icon ?? SRObjects.MissingIcon, MessageUtil.Qualify("ui", $"t.{ID.ToString().ToLower()}"), Purchasables.ToArray(), HideNewBucksCost, Close);
			}

			// TODO: Remove this, fix UITemplatesPatch
			private static GameObject CreatePurchaseUI(Sprite titleIcon, string titleKey, PurchaseUI.Purchasable[] purchasables, bool hideNubuckCost, PurchaseUI.OnClose onClose, bool unavailInMainList = false)
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
