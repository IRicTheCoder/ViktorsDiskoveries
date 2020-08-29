using Guu.API.Upgrades;
using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;

namespace VikDisk.Chapter1
{
	public class UpgradeARGlasses : PlayerUpgrade
	{
		public override string Name => "ARGlasses";

		protected override PlayerState.Upgrade Upgrade => Enums.PlayerUpgrades.AR_GLASSES;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconShopARGlasses");

		protected override int Cost => 5000;

		protected override bool StartUnlocked => true; // TODO: Change this to false

		protected override void ApplyUpgrade(PlayerModel player, bool isFirstTime)
		{
			ModLogger.Log("Acquired Viktor's AR Glasses");
		}

		protected override void Build()
		{
			// TODO: Add code to create the TargetingUI extra
		}
	}
}
