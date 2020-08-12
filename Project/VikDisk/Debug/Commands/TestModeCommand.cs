using Guu.Utils;

using MonomiPark.SlimeRancher.DataModel;
using SRML.Console;
using SRML;
using SRML.Debug;

namespace VikDisk.Debug
{
	/// <summary>
	/// A command to trigger test mod
	/// </summary>
	public class TestModeCommand : ConsoleCommand
	{
		private PlayerState Player => SceneContext.Instance.PlayerState;

		public override bool Execute(string[] args)
		{
			new UnlockAllCommand().Execute(args);

			GadgetsModel gadgets = SceneContext.Instance.GameModel.GetGadgetsModel();

			foreach (Gadget.Id value in EnumUtils.GetAll<Gadget.Id>())
			{
				try
				{
					if (gadgets.gadgets.ContainsKey(value))
						gadgets.gadgets[value] = GameContext.Instance.LookupDirector.GetGadgetDefinition(value).buyCountLimit == 0 ? 99 : GameContext.Instance.LookupDirector.GetGadgetDefinition(value).buyCountLimit;
					else
						gadgets.gadgets.Add(value, GameContext.Instance.LookupDirector.GetGadgetDefinition(value).buyCountLimit == 0 ? 99 : GameContext.Instance.LookupDirector.GetGadgetDefinition(value).buyCountLimit);
				}
				catch { continue; }
			}

			PlayerModel model = SceneContext.Instance.GameModel.GetPlayerModel();
			model.currency = 1000000;
			model.keys = 99;
			model.maxEnergy = 99999;
			model.currEnergy = 99999;
			model.maxHealth = 99999;
			model.currHealth = 99999;

			foreach (Identifiable.Id value in EnumUtils.GetAll<Identifiable.Id>())
			{
				if (GadgetDirector.IsRefineryResource(value))
					SceneContext.Instance.GadgetDirector.AddToRefinery(value, 999, true);
			}

			return true;
		}

		public override string ID { get; } = "testMode";

		public override string Usage { get; } = "testMode";

		public override string Description { get; } = "Activates the test mode for VikDisk";
	}
}
