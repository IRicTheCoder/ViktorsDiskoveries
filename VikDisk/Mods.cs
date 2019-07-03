using System;
using System.Reflection;
using System.Collections.Generic;

namespace VikDisk
{
	/// <summary>
	/// Used to track mods that are loaded
	/// </summary>
	public static class Mods
	{
		// CACHED VALUES (THE NAME OF EVERY ASSEMBLY DEPENDANT OF SRML OR UMF)
		private static List<string> cachedMods = new List<string>();

		// BOOLEANS THAT STATE IF THEY ARE LOADED OR NOT
		public static bool UMF = false;
		public static bool VacMania = false;

		// CHECKS THE MODS
		public static void CheckMods()
		{
			UMF = IsModLoaded(Configs.Mods.UMF_ITSELF);
			VacMania = IsModLoaded(Configs.Mods.VACUUM_MANIA);
		}

		/// <summary>
		/// Checks if a mod is loaded
		/// </summary>
		/// <param name="name">The name of the mod or mod's dll (Use UMF: prefix to mark as a UMF mod)</param>
		/// <returns>true if loaded, false otherwise</returns>
		public static bool IsModLoaded(string name)
		{
			if (cachedMods.Count <= 0)
			{
				foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					if (assembly.GetName().Name.Contains("SRML") || assembly.GetName().Name.Contains("uModFramework"))
					{
						cachedMods.Add(assembly.GetName().Name);
						continue;
					}

					foreach (AssemblyName assName in assembly.GetReferencedAssemblies())
					{
						if (assName.Name.Contains("SRML") || assName.Name.Contains("uModFramework"))
						{
							cachedMods.Add(assembly.GetName().Name);
							break;
						}
					}
				}
			}

			if (name.StartsWith("UMF:"))
				return cachedMods.Contains(name.Substring(4));

			return cachedMods.Contains(name);
		}
	}
}