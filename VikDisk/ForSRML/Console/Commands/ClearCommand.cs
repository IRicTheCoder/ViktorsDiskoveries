﻿using System;
using SRML.ConsoleSystem;

namespace SRML.Commands
{
	/// <summary>
	/// A command to clear the console
	/// </summary>
	public class ClearCommand : ConsoleCommand
	{
		public override string ID { get; } = "clear";
		public override string Usage { get; } = "clear";
		public override string Description { get; } = "Clears the console";

		public override bool Execute(string[] args)
		{
			if (args != null)
			{
				Console.LogError($"The 'clear' command takes no arguments");
				return false;
			}

			ConsoleWindow.fullText = string.Empty;
			return true;
		}
	}
}
