using System;
using System.Collections.Generic;
using SRML.ConsoleSystem;

namespace SRML.Commands
{
	/// <summary>
	/// A command to display all commands
	/// </summary>
	public class HelpCommand : ConsoleCommand
	{
		public override string ID { get; } = "help";
		public override string Usage { get; } = "help [cmdName]";
		public override string Description { get; } = "Displays all commands available, or an extended description of a command";

		public override string ExtendedDescription => 
			"<color=#8ab7ff>[cmdName]</color> - The command you want to check the extended description for.\n" +
			"Running without an argument shows all commands";

		public override bool Execute(string[] args)
		{
			if (args != null && ArgsOutOfBounds(args.Length, 1, 1))
				return false;

			if (args == null)
			{
				Console.Log("List of Commands Available:");
				Console.Log("<> is a required argument; [] is an optional argument");

				foreach (string line in ConsoleWindow.cmdsText.Split('\n'))
					Console.Log(line);
			}
			else
			{
				ConsoleCommand cmd = Console.commands[args[0]];

				if (cmd.ExtendedDescription != null)
				{
					Console.Log($"<color=#8ab7ff>{cmd.Usage}</color> - {cmd.Description}");
					foreach (string line in cmd.ExtendedDescription.Split('\n'))
						Console.Log(line);
				}
				else
				{
					Console.Log($"No extended description was found for command '{cmd.ID}'. Showing default description");
					Console.Log($"<color=#8ab7ff>{cmd.Usage}</color> - {cmd.Description}");
				}
			}

			return true;
		}

		public override List<string> GetAutoComplete(int argIndex, string argText)
		{
			if (argIndex == 0)
				return new List<string>(Console.commands.Keys);
			else
				return base.GetAutoComplete(argIndex, argText);
		}
	}
}
