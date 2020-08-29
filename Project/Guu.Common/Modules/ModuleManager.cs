using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Guu.Modules
{
	/// <summary>
	/// The manager for Mod Modules
	/// </summary>
	internal static class ModuleManager
	{
		/// <summary>The list of loaded entries</summary>
		private static readonly Dictionary<string, ModuleEntry> LOADED_ENTRIES = new Dictionary<string, ModuleEntry>();

		/// <summary>
		/// Loads all modules from a mod
		/// </summary>
		/// <param name="modEntry">The entry type for the mod</param>
		/// <param name="modAssembly">The assembly of the mod loading the modules</param>
		public static void LoadModules(Type modEntry, Assembly modAssembly)
		{
			if (!(modEntry.GetCustomAttributes(typeof(ModuleEntry), false) is ModuleEntry[] entries) || entries.Length == 0)
				return;

			foreach (ModuleEntry entry in entries)
			{
				string entryID = $"{entry.modID}.{entry.moduleName}";

				if (LOADED_ENTRIES.ContainsKey(entryID))
					continue;

				int dependencies = entry.dependencies.Count;
				bool canLoad = true;
				
				foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
				{
					if (ass == modAssembly || !entry.dependencies.ContainsKey(ass.GetName().Name))
						continue;

					dependencies--;
					
					if (!ass.GetName().Version.ToString().Equals(entry.dependencies[ass.GetName().Name]))
						canLoad = false;
				}

				if (dependencies > 0)
					canLoad = false;

				if (canLoad)
				{
					LOADED_ENTRIES.Add(entryID, entry);

					string codeBase = modAssembly.CodeBase;
					UriBuilder uri = new UriBuilder(codeBase);
					string path = Uri.UnescapeDataString(uri.Path);

					// ReSharper disable once AssignNullToNotNullAttribute
					Assembly.LoadFile(Path.Combine(Path.Combine(Path.GetDirectoryName(path), $"{entryID}.dll")));
				}
			}
		}
	}
}
