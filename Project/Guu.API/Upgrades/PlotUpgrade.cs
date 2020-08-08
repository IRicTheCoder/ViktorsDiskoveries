using SRML;
using SRML.SR;
using UnityEngine;

namespace Guu.API.Upgrades
{
	/// <summary>
	/// This is the base class to make plot upgrades
	/// </summary>
	public abstract class PlotUpgrade : UpgradeItem
	{
		/// <summary>The shop entry for this upgrade</summary>
		private LandPlotUpgradeRegistry.UpgradeShopEntry ShopEntry { get; set; }

		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "lUpgrade";

		/// <summary>The upgrade to register</summary>
		protected abstract LandPlot.Upgrade Upgrade { get; }

		/// <summary>The Pedia ID of the plot this belongs to</summary>
		protected abstract PediaDirector.Id PlotPediaID { get; }

		/// <summary>The ID of the plot this belongs to</summary>
		protected abstract LandPlot.Id PlotID { get; }

		/// <summary>The type of upgrade</summary>
		protected override UpgradeType Type => UpgradeType.PLOT_UPGRADE;

		/// <summary>The icon of this upgrade</summary>
		protected virtual Sprite Icon { get; } = null;

		/// <summary>The image of this upgrade</summary>
		protected virtual Sprite Image { get; } = null;

		/// <summary>The cost of this upgrade</summary>
		protected virtual int Cost { get; } = 0;

		/// <summary>Should the upgrade start unlocked?</summary>
		protected virtual bool StartUnlocked { get; } = false;

		/// <summary>Is this upgrade purchasable by default?</summary>
		protected virtual bool IsPurchasable { get; } = false;

		/// <summary>The type for the plot upgrader</summary>
		protected virtual System.Type PlotUpgrader { get; } = null;

		/// <summary>The method called when the upgrade is applied</summary>
		protected virtual bool IsUnlocked() { return StartUnlocked; }

		/// <summary>The method called to configure the Upgrader</summary>
		protected virtual void ConfigUpgrader(PlotUpgrader upgrader) { }

		/// <summary>Builds this Item</summary>
		protected override void Build()
		{
			ShopEntry = new LandPlotUpgradeRegistry.UpgradeShopEntry()
			{
				upgrade = Upgrade,
				icon = Icon ?? SRObjects.MissingIcon,
				mainImg = Image ?? Icon ?? SRObjects.MissingImg,
				cost = Cost,
				landplotPediaId = PlotPediaID,
				isUnlocked = IsUnlocked,
				LandPlotName = PlotID.ToString().ToLower()
			};
		}

		/// <summary>Registers the item into it's registry</summary>
		public override UpgradeItem Register()
		{
			base.Register();

			if (IsPurchasable)
			{
				GameObject landPlot = GameContext.Instance.LookupDirector.GetPlotPrefab(PlotID);

				// TODO: Fix this when the LandPlotUpgradeRegistry gets fixed
				landPlot.GetComponentInChildren<UIActivator>().uiPrefab.GetComponent<LandPlotUI>().RegisterUpgrade(ShopEntry);
				ConfigUpgrader(landPlot.AddComponent(PlotUpgrader) as PlotUpgrader);
			}

			return this;
		}
	}

	// TODO: Remove when the LandPlotUpgradeRegistry gets fixed
	public static class FixExtensions
	{
		public static void RegisterUpgrade<T>(this T obj, LandPlotUpgradeRegistry.UpgradeShopEntry shopEntry) where T : LandPlotUI
		{
			LandPlotUpgradeRegistry.RegisterPurchasableUpgrade<T>(shopEntry);
		}
	}
}
