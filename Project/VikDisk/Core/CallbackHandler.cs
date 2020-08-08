using SRML.Debug;
using UnityEngine.SceneManagement;
using SRML.Registries;
using SRML;
using UnityEngine;
using SRML.SR;
using SRML.Areas;

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
		}

		// Applies changes to the main menu
		private static void ApplyMenuChanges(MainMenuUI ui)
		{
			foreach (Transform child in ui.transform.Find("StandardModePanel"))
			{
				ModLogger.Log("  " + child.name);
			}
		}
	}
}
