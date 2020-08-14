using Guu;
using UnityEngine;
using SRML.SR;

using TMPro;

using VikDisk.Components.UI;

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

		// Makes a late setup of the remaining callbacks
		internal static void LateSetup()
		{
			// LANGUAGE LISTENERS
			SRGuu.RegisterTranslation(Main.execAssembly, null);
			//SRGuu.RegisterTranslation(Main.execAssembly, LanguageHandler.FixLangDisplay);
		}

		// Registers all content to the world
		private static void RegisterWorld(SceneContext ctx)
		{
			// TODO: Change following lines to Guu
			//AreaRegistry.SpawnAreas();
			//DebugHandler.Build();

			// Fixes the objects on the world
			GameFixer.FixAtWorldGen();
		}

		// Applies changes to the main menu
		private static void ApplyMenuChanges(MainMenuUI ui)
		{
			LanguageHandler.oldFont = ui.languageDropdown.captionText.font;
			LanguageHandler.oldFont.m_FallbackFontAssetTable.Add(LanguageHandler.newFont);
			LanguageHandler.oldFont.m_FallbackFontAssetTable.Add(LanguageHandler.newFontHebrew);
			LanguageHandler.oldFont.m_FallbackFontAssetTable.Add(LanguageHandler.newFontArmenian);

			foreach (TMP_Text text in Resources.FindObjectsOfTypeAll<TMP_Text>())
				text.gameObject.AddComponent<RTLSupport>().SetText(text);
		}
	}
}
