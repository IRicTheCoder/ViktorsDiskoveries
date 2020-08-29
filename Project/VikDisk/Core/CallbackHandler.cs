using Guu;

namespace VikDisk.Core
{
	/// <summary>
	/// This class contains all the methods to handle callbacks
	/// either from Unity or SR Itself
	/// </summary>
	internal static class CallbackHandler
	{
		//+ Setup Handling
		// Setup all callbacks
		internal static void Setup()
		{
			// GUU CALLBACKS
			SRGuu.MainMenuLoaded += ApplyMenuChanges;
			SRGuu.PreLoadGame += RegisterWorld;
			SRGuu.TimedUpdate += TimedUpdate;
			
			// TODO: Remove this
			SRGuu.ZoneEnter += (zone, state) => { ModLogger.Log("Entering zone: " + zone); };
		}

		// Makes a late setup of the remaining callbacks
		internal static void LateSetup()
		{
			// LANGUAGE LISTENERS
			SRGuu.RegisterTranslation(Main.execAssembly, LanguageHandler.FixLangDisplay); //LanguageHandler.FixLangDisplay
		}

		//+ Callback Functions
		// Registers all content to the world
		private static void RegisterWorld(SceneContext ctx)
		{
			// TODO: Change following lines to Guu
			//AreaRegistry.SpawnAreas();
			//DebugHandler.Build();

			// Fixes the objects on the world
			GameFixer.FixAtWorldGen();
			
			// Check if certain upgrades are bought
			/*if (ctx.PlayerState.HasUpgrade(Enums.PlayerUpgrades.SLIME_VACCINE))
			{
				SlimeDietHandler.FixDiets();
			}*/
		}

		// Applies changes to the main menu
		private static void ApplyMenuChanges(MainMenuUI ui)
		{
			LanguageHandler.oldFont = ui.languageDropdown.captionText.font;
			LanguageHandler.oldFont.m_FallbackFontAssetTable.Add(LanguageHandler.newFont);
			LanguageHandler.oldFont.m_FallbackFontAssetTable.Add(LanguageHandler.newFontHebrew);
			LanguageHandler.oldFont.m_FallbackFontAssetTable.Add(LanguageHandler.newFontArmenian);
		}

		// The timed update for the mod
		private static void TimedUpdate()
		{
		}
	}
}
