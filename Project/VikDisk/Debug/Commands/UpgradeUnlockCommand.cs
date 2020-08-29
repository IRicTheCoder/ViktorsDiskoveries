using Guu.Utils;

using MonomiPark.SlimeRancher.DataModel;
using SRML.Console;

namespace VikDisk.Debug
{
	/// <summary>
	/// A command to unlock upgrades
	/// </summary>
	public class UpgradeUnlockCommand : ConsoleCommand
	{
		private static PlayerModel Player => SceneContext.Instance.GameModel.GetPlayerModel();

		public override bool Execute(string[] args)
		{
			if (ArgsOutOfBounds(args.Length, 1, 1)) return false;

			PlayerState.Upgrade upg = EnumUtils.Parse<PlayerState.Upgrade>(args[0]);
			if (Player.upgradeLocks.ContainsKey(upg))
			{
				Player.upgradeLocks[upg].Unlock();
			}

			return true;
		}

		public override string ID { get; } = "unlockUpgrade";

		public override string Usage { get; } = "unlockUpgrade <id>";

		public override string Description { get; } = "Unlocks an upgrade and adds it to the shop";
	}
}
