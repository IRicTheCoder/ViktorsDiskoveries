using System;
using SRML.ConsoleSystem;

namespace SRML.Commands
{
	/// <summary>
	/// A command to reload the mods
	/// </summary>
	public class ReloadCommand : ConsoleCommand
	{
		public override string ID { get; } = "reload";
		public override string Usage { get; } = "reload";
		public override string Description { get; } = "Reloads the mods";

		public override bool Execute(string[] args)
		{
			if (args != null)
			{
				Console.LogError($"The 'reload' command takes no arguments");
				return false;
			}

			DateTime now = DateTime.Now;

			try
			{
				Console.ReloadMods();
				Console.Log($"Reloaded Successfully! (Took {(DateTime.Now - now).TotalMilliseconds} ms)");

				return true;
			}
			catch (Exception e)
			{
				Console.LogError("Reload Failed! Reason displayed below:");
				Console.LogError(e.Message + "\n" + e.StackTrace);
				return false;
			}
		}
	}
}
