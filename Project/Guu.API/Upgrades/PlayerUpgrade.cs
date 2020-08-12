using Guu.Utils;

using MonomiPark.SlimeRancher.DataModel;

using SRML;
using SRML.SR;
using UnityEngine;

namespace Guu.API.Upgrades
{
	/// <summary>
	/// This is the base class to make player upgrades
	/// </summary>
	public abstract class PlayerUpgrade : UpgradeItem
	{
		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "pUpgrade";

		/// <summary>The upgrade to register</summary>
		protected abstract PlayerState.Upgrade Upgrade { get; }

		/// <summary>The type of upgrade</summary>
		protected override UpgradeType Type => UpgradeType.PLAYER_UPGRADE;

		/// <summary>The icon of this upgrade</summary>
		protected virtual Sprite Icon { get; } = null;

		/// <summary>The cost of this upgrade</summary>
		protected virtual int Cost { get; } = 0;

		/// <summary>Should the upgrade start unlocked?</summary>
		protected virtual bool StartUnlocked { get; } = false;

		/// <summary>The method called when the upgrade is applied</summary>
		protected virtual void ApplyUpgrade(PlayerModel player, bool isFirstTime) { }

		/// <summary>The method called to create the upgrade locker</summary>
		protected virtual PlayerState.UpgradeLocker CreateUpgradeLocker(PlayerState state) { return null; }

		/// <summary>Builds this Item</summary>
		protected override void Build() { }

		/// <summary>Registers the item into it's registry</summary>
		public override UpgradeItem Register()
		{
			base.Register();

			LookupRegistry.RegisterUpgradeEntry(ScriptableObjectsUtils.CreateUpgradeDefinition(Upgrade, Icon ?? SRObjects.MissingIcon, Cost));
			PersonalUpgradeRegistry.RegisterUpgradeCallback(Upgrade, ApplyUpgrade);

			if (StartUnlocked)
				PersonalUpgradeRegistry.RegisterDefaultUpgrade(Upgrade);
			else
				PersonalUpgradeRegistry.RegisterUpgradeLock(Upgrade, CreateUpgradeLocker);

			return this;
		}
	}
}
