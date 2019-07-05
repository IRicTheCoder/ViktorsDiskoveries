using System;

namespace VikDisk.Console.Commands
{
	/// <summary>
	/// A command to display all commands
	/// </summary>
	public class HelpCommand : ConsoleCommand
	{
		public override string ID { get; } = "help";
		public override string Usage { get; } = "help";
		public override string Description { get; } = "Displays all commands available";

		public override bool Execute(string[] args)
		{
			if (args != null)
			{
				Console.LogError($"The 'help' command takes no arguments");
				return false;
			}

			Console.Log("<color=lightblue>List of Commands Available:</color>");

			foreach (string line in ConsoleWindow.cmdsText.Split('\n'))
				Console.Log(line);

			return true;
		}
	}
}
