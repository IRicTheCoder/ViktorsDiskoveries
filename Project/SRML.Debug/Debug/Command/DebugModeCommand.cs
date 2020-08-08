using SRML.Console;
using static SRML.Console.Console;

namespace SRML.Debug
{
	public class DebugModeCommand : ConsoleCommand
	{
		public override bool Execute(string[] args)
		{
			if (ArgsOutOfBounds(args.Length, 1))
				return false;

			switch (args[0])
			{
				case "help":
					RunHelp();
					break;
				case "state":
					if (ArgsOutOfBounds(args.Length, 2, 2))
						return false;

					RunState(bool.Parse(args[1]));

					break;
				case "markers":
					if (ArgsOutOfBounds(args.Length, 2, 2))
						return false;

					RunMarkers(bool.Parse(args[1]));

					break;
			}

			return true;
		}

		private void RunHelp()
		{
			Log("<color=cyan>List of all commands:</color>", false);
			LogSuccess("<color=white>debugmode state <value></color> - <value> can be true or false to activate or deactivate the debug mode", false);
			LogSuccess("<color=white>debugmode markers <value> - <value></color> can be true or false to activate or deactivate the markers", false);
		}

		private void RunState(bool state)
		{
			DebugHandler.IsDebugging = state;
			LogSuccess($"Turned the debug mode {(state ? "on" : "off")}");
		}

		private void RunMarkers(bool state)
		{
			MarkerController.ShowMarkers = state;
			LogSuccess($"Turned the markers visibility {(state ? "on" : "off")}");
		}

		public override string ID { get; } = "debugmode";
		public override string Usage { get; } = "debugmode <action>";
		public override string Description { get; } = "Controls SRML's debug mode. Use 'help' as the action to get more info";
		public override string ExtendedDescription => 
			"This command is used to control the debug mode for SRML.\nYou can use the 'help' action to learn all the actions and arguments";
	}
}
