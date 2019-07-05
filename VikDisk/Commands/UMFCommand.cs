using SRML.ConsoleSystem;

namespace VikDisk.Commands
{
	/// <summary>
	/// A command that taps into UMF's command system
	/// <para>Main usage would be the ability to run commands from </para>
	/// </summary>
	public class UMFCommand : ConsoleCommand
	{
		public override string ID { get; } = "umf";
		public override string Usage { get; } = "umf <command> [args..]";
		public override string Description { get; } = "Runs a command from UMF Console";

		public override bool Execute(string[] args)
		{
			if (args == null)
			{
				//Console.LogError($"The 'umf' command requires at least 1 argument");
				return false;
			}

			if (ArgsOutOfBounds(args.Length, 1))
				return false;

			/*Console.Log("<color=lightblue>List of Commands Available:</color>");

			foreach (string line in ConsoleWindow.cmdsText.Split('\n'))
				Console.Log(line);*/

			return true;
		}
	}
}
