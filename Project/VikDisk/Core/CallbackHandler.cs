using SRML.Debug;
using UnityEngine.SceneManagement;
using SRML.Registries;
using SRML;
using UnityEngine;
using SRML.SR;
using SRML.Areas;

using TMPro;

namespace VikDisk.Core
{
	/// <summary>
	/// This class contains all the methods to handle callbacks
	/// either from Unity or SR Itself
	/// </summary>
	internal static class CallbackHandler
	{
		// Setup all callbacks
		internal static void Setup()
		{
			// UNITY CALLBACKS
			SRCallbacks.OnMainMenuLoaded += ApplyMenuChanges;
			SRCallbacks.OnSaveGameLoaded += RegisterWorld;
		}

		// Registers all content to the world
		private static void RegisterWorld(SceneContext ctx)
		{
			// TODO: Change following lines to SRML
			//AreaRegistry.SpawnAreas();
			//DebugHandler.Build();

			GameFixer.FixAtWorldGen();

			// Fixes the objects on the world
			/*if (LanguageHandler.symbolFont)
			{
				TMP_Text[] texts = Resources.FindObjectsOfTypeAll<TMP_Text>();
				foreach (TMP_Text text in texts) text.font = LanguageHandler.newFont;
			}
			else if (LanguageHandler.hasSymbolFont)
			{
				TMP_Text[] texts = Resources.FindObjectsOfTypeAll<TMP_Text>();
				foreach (TMP_Text text in texts) text.font = LanguageHandler.oldFont;
			}*/
		}

		// Applies changes to the main menu
		private static void ApplyMenuChanges(MainMenuUI ui)
		{
			LanguageHandler.oldFont = ui.languageDropdown.captionText.font;

			ui.languageDropdown.itemText.font = LanguageHandler.newFont;
			TMP_Text[] texts = Object.FindObjectsOfType<TMP_Text>();
			foreach (TMP_Text text in texts) text.font = LanguageHandler.newFont;
		}
	}
}
