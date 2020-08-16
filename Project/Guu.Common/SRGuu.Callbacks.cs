using System;

namespace Guu
{
    // TODO: FINISH THIS
    public partial class SRGuu
    {
        // Context Events
        public static event Action<GameContext> GameContextReady;
        
        // Scene Load Events
        public static event Action<MainMenuUI> MainMenuLoaded;

        // Save Game Events
        public static event Action<SceneContext> PreLoadGame;
        public static event Action<SceneContext> GameLoaded;

        // Triggers when the game context is ready
        internal static void OnGameContextReady(GameContext ctx)
        {
            if (!isInitialized) return;
            
            GameContextReady?.Invoke(ctx);
        }

        internal static void OnMainMenuLoaded(MainMenuUI mainMenuUI)
        {
            if (!isInitialized) return;
            
            MainMenuLoaded?.Invoke(mainMenuUI);
        }
        
        internal static void OnPreLoadGame(SceneContext ctx)
        {
            if (!isInitialized || Levels.isMainMenu()) return;
            
            PreLoadGame?.Invoke(ctx);
        }
        
        internal static void OnGameLoaded(SceneContext ctx)
        {
            if (!isInitialized || Levels.isMainMenu()) return;
            
            GameLoaded?.Invoke(ctx);
        }
    }
}