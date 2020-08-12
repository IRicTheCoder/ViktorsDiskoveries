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

using Object = System.Object;

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
			LanguageHandler.oldFont.m_FallbackFontAssetTable = new List<TMP_FontAsset>()
			{
				LanguageHandler.newFont,
				LanguageHandler.oldFont
			};
		}
	}
}
