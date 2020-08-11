using System;
using System.Collections.Generic;

using Guu;
using Guu.Language;

using SRML.Debug;
using UnityEngine.SceneManagement;
using SRML.Registries;
using SRML;
using UnityEngine;
using SRML.SR;
using SRML.Areas;

using TMPro;

using UnityEngine.EventSystems;

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
			GameContext.Instance.MessageDirector.RegisterBundlesListener(LanguageChange);
			GameContext.Instance.MessageDirector.RegisterBundlesListener(LanguageHandler.FixLangDisplay);
		}

		// Registers all content to the world
		private static void RegisterWorld(SceneContext ctx)
		{
			// TODO: Change following lines to SRML
			//AreaRegistry.SpawnAreas();
			//DebugHandler.Build();

			GameFixer.FixAtWorldGen();

			// Fixes the objects on the world
			LanguageHandler.FixLangDisplay(GameContext.Instance.MessageDirector);
		}

		// Applies changes to the main menu
		private static void ApplyMenuChanges(MainMenuUI ui)
		{
			LanguageHandler.oldFont = ui.languageDropdown.captionText.font;
			LanguageHandler.newFont.m_FallbackFontAssetTable = new List<TMP_FontAsset>
			{
				LanguageHandler.oldFont
			};
			
			LanguageHandler.ApplyFontChange(GameContext.Instance.MessageDirector);
		}

		// When the language changes
		private static void LanguageChange(MessageDirector dir)
		{
			LanguageController.SetTranslations(Main.execAssembly);
		}
	}
}
