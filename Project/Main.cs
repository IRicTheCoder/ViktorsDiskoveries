using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using SRML;

namespace VikDisk
{
	/// <summary>
	/// The main class and entry point for the mod
	/// </summary>
	// ReSharper disable once ClassNeverInstantiated.Global
	public class Main : ModEntryPoint
	{
		// THE EXECUTING ASSEMBLY
		public static Assembly execAssembly;

		// PRE LOAD MOD
		public override void PreLoad()
		{
			// Gets the Assembly being executed
			execAssembly = Assembly.GetExecutingAssembly();
			HarmonyInstance.PatchAll(execAssembly);

			// Sets up the mod
			VikDisk.SetupMod();
		}

		// LOAD MOD
		public override void Load()
		{
			// Populates the mod
			VikDisk.PopulateMod();
		}

		// POST LOAD MOD
		public override void PostLoad()
		{
			// Sets up the intermod communications
			VikDisk.IntermodComms();
		}
	}
}
