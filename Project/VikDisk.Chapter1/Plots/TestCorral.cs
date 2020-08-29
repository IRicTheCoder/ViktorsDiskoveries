using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Plots;
using Guu.API.UI;

namespace VikDisk.Game
{
	[NoRegister]
	public class TestCorral : CorralPlot
	{
		public override string Name => "TestCorral";

		protected override LandPlot.Id ID => Enums.LandPlots.TEST_CORRAL;

		protected override GameObject UIPrefab => UIs.Get<PurchasableUIItem>("TestCorral").Prefab;

		protected override PediaDirector.Id PediaID => Enums.Pedia.TEST_CORRAL;
	}
}
