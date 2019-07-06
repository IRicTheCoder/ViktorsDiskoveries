using System;
using System.Collections.Generic;
using SRML.ConsoleSystem;

namespace SRML.Commands
{
	/// <summary>
	/// A command to remove a user defined button from the command menu
	/// </summary>
	public class RemoveButtonCommand : ConsoleCommand
	{
		public override string ID { get; } = "rmvbutton";
		public override string Usage { get; } = "rmvbutton <text>";
		public override string Description { get; } = "Removes a user defined button from the command menu";

		public override string ExtendedDescription => "<color=#8ab7ff><text></color> - The text of the button to remove. 'all' will remove all buttons";

		public override bool Execute(string[] args)
		{
			if (args == null)
			{
				Console.LogError($"The 'rmvbutton' command takes 1 arguments");
				return false;
			}

			if (ArgsOutOfBounds(args.Length, 1, 1))
				return false;

			if (!args[0].Equals("all"))
			{
				if (ConsoleBinder.RemoveBind(args[0]))
				{
					Console.Log($"Removed user defined button '{args[0]}'");
					return true;
				}
			}
			else
			{
				ConsoleBinder.RemoveAll();
				Console.Log($"Removed all user defined buttons");
				return true;
			}

			Console.LogError($"The user defined button '{args[0]}' was not found");
			return false;
		}

		public override List<string> GetAutoComplete(int argIndex, string argText)
		{
			if (argIndex == 0)
				return ConsoleBinder.GetAllBinds();
			else
				return base.GetAutoComplete(argIndex, argText);
		}
	}
}
