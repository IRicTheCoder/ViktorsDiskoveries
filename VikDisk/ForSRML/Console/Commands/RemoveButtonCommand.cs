using System;
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

		public override bool Execute(string[] args)
		{
			if (args == null)
			{
				Console.LogError($"The 'rmvbutton' command takes 1 arguments");
				return false;
			}

			if (ArgsOutOfBounds(args.Length, 1, 1))
				return false;

			if (ConsoleBinder.RemoveBind(args[0]))
			{
				Console.Log($"Removed user defined command button '{args[0]}'");
				return true;
			}

			// TODO: Remove All

			Console.LogError($"The user defined button '{args[0]}' was not found");
			return false;
		}
	}
}
