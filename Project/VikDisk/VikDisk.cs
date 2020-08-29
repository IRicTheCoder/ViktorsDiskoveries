using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Guu.Debug;
using VikDisk.Core;
using VikDisk.Debug;
using VikDisk.Game;
using SRML.Debug;
using VikDisk.Utils;
using Console = SRML.Console.Console;

namespace VikDisk
{
	// TODO: Move the TODO list to ClickUp
	/// <summary>
	/// The controller for the mod, deals with registries,
	/// loadings, etc.
	/// </summary>
	public static class VikDisk
	{
		//+ CONSTANTS
		// Is the game in testing mode?
		private static readonly bool TESTING = true;
		
		//+ VARIABLES & EVENTS
		// A list of all injectors from different chapters
		private static List<Injector> injectors = new List<Injector>();

		//+ MOD LOADING METHODS
		
		// Sets up the mod with all the main stuff
		internal static void SetupMod()
		{
			Packs.Setup();
			LanguageHandler.Setup();
			CallbackHandler.Setup();

			// Gets all injectors and runs them
			Type inject = Main.execAssembly.GetTypes().FirstOrDefault(type => type.IsSubclassOf(typeof(Injector)));

			if (inject == null) return;
			injectors.Add(Activator.CreateInstance(inject) as Injector);
			injectors[injectors.Count - 1].SetupMod();
		}

		// Runs the population code for the mod,
		// registering all objects and populating
		// them
		internal static void PopulateMod()
		{
			// Register extra modules for registration
			foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (ass.GetName().Name.StartsWith("VikDisk."))
					RegistryUtils.extraModules.Add(ass);
			}

			
			// Registers the all game content
			Others.RegisterAll();
			Identifiables.RegisterAll();
			SpawnResources.RegisterAll();
			Gadgets.RegisterAll();
			Upgrades.RegisterAll();
			UIs.RegisterAll();
			Plots.RegisterAll();
			
			//GordoRegistry.Setup();
			
			// Adds new commands to the game
			Console.RegisterCommand(new DumperCommand());
			Console.RegisterCommand(new UnlockAllCommand());
			Console.RegisterCommand(new TPCommand());
			Console.RegisterCommand(new DebugModeCommand());

			if (TESTING)
				Console.RegisterCommand(new TestModeCommand());
			
			// Registers the remaining callback
			CallbackHandler.LateSetup();
			
			// Runs fixing methods
			GameFixer.FixAtGameLoad();
			
			// Run Injectors
			foreach (Injector inject in injectors)
				inject.PopulateMod();
		}

		// TODO: Add Guu UMF Support
		// Takes care of all intermod communication,
		// this will also work with UMF mods as
		// Guu has a way to connect to those mods
		internal static void IntermodComms()
		{
			// TODO: Remove this code (Glitch Slime version)
			#region Glitch Slime Workaround
			
			SlimeDiet diet = GameContext.Instance?.SlimeDefinitions
			                            .GetSlimeByIdentifiableId(Identifiable.Id.PINK_SLIME).Diet;
			
			diet?.EatMap.Add(new SlimeDiet.EatMapEntry()
			{
				becomesId = Enums.Identifiables.REAL_GLITCH_SLIME,
				driver = SlimeEmotions.Emotion.NONE,
				eats = Identifiable.Id.MANIFOLD_CUBE_CRAFT
			});
			
			diet?.EatMap.Add(new SlimeDiet.EatMapEntry()
			{
				becomesId = Enums.Identifiables.REAL_DIGITARR_SLIME,
				driver = SlimeEmotions.Emotion.NONE,
				eats = Enums.Identifiables.REAL_GLITCH_PLORT
			});
			
			#endregion
			
			// Run Injectors
			foreach (Injector inject in injectors)
				inject.IntermodComms();
			
			// Clears all the temporary memory
			GC.Collect();
		}
	}
}