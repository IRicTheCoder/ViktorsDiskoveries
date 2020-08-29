using System;
using Guu.API.Upgrades;
using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;
using VikDisk.Game;

namespace VikDisk.Chapter1
{
	public class UpgradeSlimeVaccine : PlayerUpgrade
	{
		public override string Name => "SlimeVaccine";

		protected override PlayerState.Upgrade Upgrade => Enums.PlayerUpgrades.SLIME_VACCINE;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconShopSlimeVaccine");

		protected override int Cost => 0;

		protected override bool StartUnlocked => false;

		protected override void ApplyUpgrade(PlayerModel player, bool isFirstTime)
		{
			SlimeDietHandler.FixDiets();
			SlimeSpawnHandler.FixSpawns();

			if (isFirstTime)
			{
				// TODO: Send mail
			}
		}

		protected override PlayerState.UpgradeLocker CreateUpgradeLocker(PlayerState state)
		{
			return new PlayerState.UpgradeLocker(state, CheckProgress, 6);
		}

		private static bool CheckProgress()
		{
			try
			{
				return SceneContext.Instance?.ProgressDirector.HasProgress(ProgressDirector
				                                                           .ProgressType.VIKTOR_SEEN_FINAL_CHAT) ?? false;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
