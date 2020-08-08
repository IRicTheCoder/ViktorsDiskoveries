using SRML;
using SRML.Console;
using VikDisk.Core;
using VikDisk.Debug;
using VikDisk.Game;
using SRML.Debug;

namespace VikDisk
{
	/// <summary>
	/// The controller for the mod, deals with registries,
	/// loadings, etc.
	/// </summary>
	public static class VikDisk
	{
		// Is the game in testing mode?
		public static bool TESTING = true;

		// Sets up the mod with all the main stuff
		internal static void SetupMod()
		{
			Packs.Setup();
			LanguageHandler.Setup();
			CallbackHandler.Setup();

			//SlimeUtils.GenerateLargoIDs(Enums.Identifiables.REAL_GLITCH_SLIME);
		}

		// Runs the population code for the mod,
		// registering all objects and populating
		// them
		internal static void PopulateMod()
		{
			Others.RegisterAll();
			Identifiables.RegisterAll();
			SpawnResources.RegisterAll();
			Gadgets.RegisterAll();
			Upgrades.RegisterAll();
			UIs.RegisterAll();
			Plots.RegisterAll();

			// TODO: Change these to SRML
			GameContext.Instance.MessageDirector.RegisterBundlesListener(LanguageUtils.LanguageChange);
			//GordoRegistry.Setup();
			
			Console.RegisterCommand(new DumperCommand());
			Console.RegisterCommand(new UnlockAllCommand());
			Console.RegisterCommand(new TPCommand());
			Console.RegisterCommand(new DebugModeCommand());

			if (TESTING)
				Console.RegisterCommand(new TestModeCommand());

			GameFixer.FixAtGameLoad();
			
			// TODO: Remove this code (Glitch Slime version)
			SlimeDiet diet = GameContext.Instance?.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.PINK_SLIME).Diet;
			diet.EatMap.Add(new SlimeDiet.EatMapEntry()
			{
				becomesId = Enums.Identifiables.REAL_GLITCH_SLIME,
				driver = SlimeEmotions.Emotion.HUNGER,
				eats = Identifiable.Id.MANIFOLD_CUBE_CRAFT
			});
		}

		// Takes care of all intermod communication,
		// this will also work with UMF mods as
		// VikDisk has a way to connect to those mods
		internal static void IntermodComms()
		{

		}
	}
}