using System;
using System.Collections.Generic;
using SRML.ConsoleSystem;

namespace SRML.Commands
{
	/// <summary>
	/// A command to edit a user defined button
	/// </summary>
	public class EditButtonCommand : ConsoleCommand
	{
		public override string ID { get; } = "editbutton";
		public override string Usage { get; } = "editbutton <text> <command>";
		public override string Description { get; } = "Edits a user defined button";

		public override string ExtendedDescription => 
			"<color=#8ab7ff><text></color> - The text of the button being edited\n" +
			"<color=#8ab7ff><command></color> - The command to replace the old one";

		public override bool Execute(string[] args)
		{
			if (args == null)
			{
				Console.LogError($"The 'editbutton' command takes 2 arguments");
				return false;
			}

			if (ArgsOutOfBounds(args.Length, 2, 2))
				return false;

			ConsoleBinder.RemoveBind(args[0]);
			ConsoleBinder.RegisterBind(args[0], args[1]);
			Console.Log($"Edited user defined button '{args[0]}' and replaced it with new command '{args[1]}'");

			return true;
		}

		public override List<string> GetAutoComplete(int argIndex, string argText)
		{
			if (argIndex == 0)
				return ConsoleBinder.GetAllBinds();
			else if (argIndex == 1)
				return new List<string>(Console.dumpActions.Keys);
			else
				return base.GetAutoComplete(argIndex, argText);
		}
	}
}
