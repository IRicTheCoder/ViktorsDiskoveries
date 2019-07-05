using System;

namespace VikDisk.Console.Commands
{
	/// <summary>
	/// A command to display all mods
	/// </summary>
	public class ModsCommand : ConsoleCommand
	{
		public override string ID { get; } = "mods";
		public override string Usage { get; } = "mods";
		public override string Description { get; } = "Displays all mods loaded";

		public override bool Execute(string[] args)
		{
			if (args != null)
			{
				Console.LogError($"The 'mods' command takes no arguments");
				return false;
			}

			Console.Log("List of Mods Loaded:");

			foreach (string line in ConsoleWindow.modsText.Split('\n'))
				Console.Log(line);

			return true;
		}
	}
}
