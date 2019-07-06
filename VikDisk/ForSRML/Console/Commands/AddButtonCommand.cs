using System;
using System.Collections.Generic;
using SRML.ConsoleSystem;

namespace SRML.Commands
{
	/// <summary>
	/// A command to add a user defined button to the command menu
	/// </summary>
	public class AddButtonCommand : ConsoleCommand
	{
		public override string ID { get; } = "addbutton";
		public override string Usage { get; } = "addbutton <text> <command>";
		public override string Description { get; } = "Adds a user defined button to the command menu";

		public override string ExtendedDescription => 
			"<color=#8ab7ff><text></color> - The text to display on the button (it also serves as the id of the button, for removal or editing)\n" +
			"<color=#8ab7ff><command></color> - The command the button will execute";

		public override bool Execute(string[] args)
		{
			if (args == null)
			{
				Console.LogError($"The 'addbutton' command takes 2 arguments");
				return false;
			}

			if (ArgsOutOfBounds(args.Length, 2, 2))
				return false;

			ConsoleBinder.RegisterBind(args[0], args[1]);
			Console.Log($"Added new user defined button '{args[0]}' with command '{args[1]}'");

			return true;
		}

		public override List<string> GetAutoComplete(int argIndex, string argText)
		{
			if (argIndex == 1)
				return new List<string>(Console.commands.Keys);
			else
				return base.GetAutoComplete(argIndex, argText);
		}
	}
}
