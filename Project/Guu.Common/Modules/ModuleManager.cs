using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SRML.Modules
{
	/// <summary>
	/// The manager for SRML or Mod Modules
	/// </summary>
	internal static class ModuleManager
	{
		/// <summary>The list of loaded entries</summary>
		public static Dictionary<string, ModuleEntry> loadedEntries;

		/// <summary>
		/// Loads all modules from a mod entry point
		/// </summary>
		/// <param name="modEntry">The entry point for the mod from where the modules should be loaded</param>
		/// <param name="modInfo">The info for the mod from where the modules are loaded</param>
		/// <param name="modAssembly">The assembly of the mod loading the modules</param>
		public static void LoadModules(IModEntryPoint modEntry, SRModInfo modInfo, Assembly modAssembly)
		{
			ModuleEntry[] entries = modEntry.GetType().GetCustomAttributes(typeof(ModuleEntry), false) as ModuleEntry[];

			if (entries.Length == 0)
				return;

			foreach (ModuleEntry entry in entries)
			{
				string entryID = $"{modInfo.Id}.{entry.moduleName}";

				if (loadedEntries.ContainsKey(entryID))
					continue;

				int dependencies = entry.dependencies.Count;
				bool canLoad = true;
				foreach (SRModInfo mod in SRModLoader.LoadedMods)
				{
					if (mod == modInfo || !entry.dependencies.ContainsKey(mod.Id))
						continue;

					dependencies--;

					if (!mod.Version.ToString().Equals(entry.dependencies[mod.Id]))
						canLoad = false;
				}

				if (dependencies > 0)
					canLoad = false;

				if (canLoad)
				{
					loadedEntries.Add(entryID, entry);
					loadedEntries[entryID].modID = modInfo.Id;

					string codeBase = modAssembly.CodeBase;
					UriBuilder uri = new UriBuilder(codeBase);
					string path = Uri.UnescapeDataString(uri.Path);

					Assembly.LoadFile(Path.Combine(Path.Combine(Path.GetDirectoryName(path), $"{entry.moduleName}.dll")));
				}
			}
		}
	}
}
