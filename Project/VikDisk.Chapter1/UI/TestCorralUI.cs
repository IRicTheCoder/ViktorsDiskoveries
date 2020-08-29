using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SRML;
using Guu.API;
using Guu.API.UI;

namespace VikDisk.Game
{
	[NoRegister]
	public class TestCorralUI : PurchasableUIItem
	{
		public override string Name => "TestCorral";

		protected override Type UIType => typeof(TestCorralPlotUI);

		public class TestCorralPlotUI : CustomPlotUI
		{
			protected override LandPlot.Id ID { get; } = LandPlot.Id.NONE;

			protected override Sprite Icon { get; } = null;

			protected override void PopulateUI()
			{
				Purchasables.Add(new PurchaseUI.Purchasable("t.garden", SRObjects.MissingIcon, SRObjects.MissingImg, "m.intro.garden", 100, new PediaDirector.Id?(PediaDirector.Id.GARDEN), BuyGarden, () => true, () => true));
				Purchasables.Add(new PurchaseUI.Purchasable(MessageUtil.Qualify("ui", "l.demolish_plot"), SRObjects.MissingIcon, SRObjects.MissingImg, MessageUtil.Qualify("ui", "m.desc.demolish_plot"), -125, null, Demolish, () => true, () => true, "b.demolish"));
			}

			public void BuyGarden()
			{
				BuyPlot(new PlotPurchaseItem()
				{
					cost = 100,
					icon = SRObjects.MissingIcon,
					img = SRObjects.MissingImg,
					plotPrefab = GameContext.Instance.LookupDirector.GetPlotPrefab(LandPlot.Id.GARDEN)
				});
			}

			public void Demolish()
			{
				if (playerState.GetCurrency() >= 0)
				{
					playerState.SpendCurrency(-125, false);
					Replace(GameContext.Instance.LookupDirector.GetPlotPrefab(LandPlot.Id.EMPTY));
					PlayPurchaseCue();
					return;
				}
				Error("e.insuf_coins", false);
			}
		}
	}
}
