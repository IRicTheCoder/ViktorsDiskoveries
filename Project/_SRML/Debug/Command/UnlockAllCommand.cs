using MonomiPark.SlimeRancher.DataModel;
using SRML.Console;

namespace VikDisk.SRML.Debug
{
	/// <summary>
	/// A command to unlock all progress
	/// </summary>
	public class UnlockAllCommand : ConsoleCommand
	{
		public override bool Execute(string[] args)
		{
			SceneContext.Instance.PlayerState.UnlockAllMaps();

			foreach (PlayerState.Upgrade value in EnumUtils.GetAll<PlayerState.Upgrade>())
				SceneContext.Instance.PlayerState.AddUpgrade(value);

			foreach (ProgressDirector.ProgressType value in EnumUtils.GetAll<ProgressDirector.ProgressType>())
			{
				SceneContext.Instance.ProgressDirector.SetProgress(value, 1);
				SceneContext.Instance.ProgressDirector.SetUniqueProgress(value);
			}

			SceneContext.Instance.ProgressDirector.SetProgress(ProgressDirector.ProgressType.CORPORATE_PARTNER, 28);
			SceneContext.Instance.ProgressDirector.SetProgress(ProgressDirector.ProgressType.MOCHI_REWARDS, 3);
			SceneContext.Instance.ProgressDirector.SetProgress(ProgressDirector.ProgressType.OGDEN_REWARDS, 3);
			SceneContext.Instance.ProgressDirector.SetProgress(ProgressDirector.ProgressType.VIKTOR_REWARDS, 3);

			SceneContext.Instance.ProgressDirector.MaybeUnlockMochiMissions();
			SceneContext.Instance.ProgressDirector.MaybeUnlockOgdenMissions();
			SceneContext.Instance.ProgressDirector.MaybeUnlockViktorMissions();
			SceneContext.Instance.ProgressDirector.Update();

			SceneContext.Instance.PediaDirector.UnlockScience();

			foreach (PediaDirector.Id value in EnumUtils.GetAll<PediaDirector.Id>())
				SceneContext.Instance.PediaDirector.UnlockWithoutPopup(value);

			foreach (Gadget.Id value in EnumUtils.GetAll<Gadget.Id>())
			{
				if (SceneContext.Instance.GadgetDirector.HasBlueprint(value))
					SceneContext.Instance.GadgetDirector.AddBlueprint(value);
			}

			foreach (AccessDoorModel door in SceneContext.Instance.GameModel.AllDoors().Values)
			{
				door.state = AccessDoor.State.OPEN;
			}

			return true;
		}

		public override string ID { get; } = "unlockAll";
		public override string Usage { get; } = "unlockAll";
		public override string Description { get; } = "Unlocks all progress";
	}
}
