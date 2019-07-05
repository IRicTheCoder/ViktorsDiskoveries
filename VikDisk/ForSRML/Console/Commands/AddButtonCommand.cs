using System;
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
			Console.Log($"Added new user defined command button '{args[0]}' with command '{args[1]}'");

			return true;
		}
	}
}
