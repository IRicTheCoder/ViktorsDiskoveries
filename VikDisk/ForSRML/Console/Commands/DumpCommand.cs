using System.IO;
using System;
using SRML.ConsoleSystem;

namespace SRML.Commands
{
	/// <summary>
	/// A command that dumps stuff to dump files
	/// </summary>
	public class DumpCommand : ConsoleCommand
	{
		public override string ID { get; } = "dump";
		public override string Usage { get; } = "dump <type>";
		public override string Description { get; } = "Dumps information to a dump file";

		public override bool Execute(string[] args)
		{
			if (args == null)
			{
				Console.LogError($"The '{ID}' command requires at least 1 argument");
				return false;
			}

			if (ArgsOutOfBounds(args.Length, 1, 1))
				return false;

			try
			{
				if (args[0].Equals("all"))
				{
					foreach (string file in Console.dumpActions.Keys)
					{
						DumpFile(file);
					}

					return false;
				}
				else
				{
					return DumpFile(args[0]);
				}
			}
			catch { }

			Console.LogError($"Couldn't find or create file '{args[0]}'");
			return false;
		}

		private bool DumpFile(string name)
		{
			if (!Console.dumpActions.ContainsKey(name))
			{
				Console.LogError($"No dump action found for '{name}'");
				return false;
			}

			string path = Path.Combine(UnityEngine.Application.dataPath, $"../Dumps/{name}.dump");

			if (!Directory.Exists(path.Substring(0, path.LastIndexOf('/'))))
				Directory.CreateDirectory(path.Substring(0, path.LastIndexOf('/')));

			using (StreamWriter writer = File.CreateText(path))
			{
				Console.dumpActions[name].Invoke(writer);
			}

			Console.Log($"File '{name}' dumped succesfully");
			return true;
		}
	}
}
