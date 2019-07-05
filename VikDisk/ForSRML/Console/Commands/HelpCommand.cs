using System;
using SRML.ConsoleSystem;

namespace SRML.Commands
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

			Console.Log("List of Commands Available:");

			foreach (string line in ConsoleWindow.cmdsText.Split('\n'))
				Console.Log(line);

			return true;
		}
	}
}
