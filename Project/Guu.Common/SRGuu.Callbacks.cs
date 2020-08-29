using System;

namespace Guu
{
    public partial class SRGuu
    {
        // Context Events
        public static event Action<GameContext> GameContextReady;
        
        // Scene Load Events
        public static event Action<MainMenuUI> MainMenuLoaded;

        // Save Game Events
        public static event Action<SceneContext> PreLoadGame;
        public static event Action<SceneContext> GameLoaded;
        
        // Gameplay Events
        public static event Action Update;
        public static event Action FixedUpdate;
        public static event Action LateUpdate;
        public static event Action TimedUpdate;

        public static event Action<ZoneDirector.Zone, PlayerState> ZoneEnter;
        
        //+ Context Events
        // Triggers when the game context is ready
        internal static void OnGameContextReady(GameContext ctx)
        {
            if (!isInitialized) return;
            
            GameContextReady?.Invoke(ctx);
        }

        //+ Scene Load Events
        // Triggers when the main menu loads
        internal static void OnMainMenuLoaded(MainMenuUI mainMenuUI)
        {
            if (!isInitialized) return;
            
            MainMenuLoaded?.Invoke(mainMenuUI);
        }
        
        //+ Save Game Events
        // Triggers before the game is loaded
        internal static void OnPreLoadGame(SceneContext ctx)
        {
            if (!isInitialized || Levels.isMainMenu()) return;
            
            PreLoadGame?.Invoke(ctx);
        }
        
        // Triggers after the game is loaded
        internal static void OnGameLoaded(SceneContext ctx)
        {
            if (!isInitialized || Levels.isMainMenu()) return;
            
            GameLoaded?.Invoke(ctx);
        }
        
        //+ Gameplay Events
        // Triggers when the game updates
        internal static void OnGameUpdate()
        {
            if (!isInitialized) return;
            
            Update?.Invoke();
        }
        
        // Triggers when the game makes a fixed update
        internal static void OnGameFixedUpdate()
        {
            if (!isInitialized) return;
            
            FixedUpdate?.Invoke();
        }
        
        // Triggers when the game makes a late update
        internal static void OnGameLateUpdate()
        {
            if (!isInitialized) return;
            
            LateUpdate?.Invoke();
        }
        
        // Triggers when the game makes a timed update (every 5 seconds)
        internal static void OnGameTimedUpdate()
        {
            if (!isInitialized) return;
            
            TimedUpdate?.Invoke();
        }

        // Triggers when you enter a zone
        internal static void OnZoneEntered(ZoneDirector.Zone zone, PlayerState player)
        {
            if (!isInitialized) return;
            
            ZoneEnter?.Invoke(zone, player);
        }
    }
}