using SRML.ConsoleSystem;

namespace VikDisk.Commands
{
	/// <summary>
	/// A command that dumps stuff to dump files
	/// </summary>
	public class DumpCommand : ConsoleCommand
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
