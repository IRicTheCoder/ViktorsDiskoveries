using System.Reflection;

using Guu.Language;

using HarmonyLib;

using SRML;

using UnityEngine;

namespace Guu
{
    // TODO: FINISH THIS
    public class SRGuu
    {
        // Is the services initialized
        private static bool isInitialized = false;

        // The assembly from Guu
        internal static Assembly assembly;

        // The harmony instance for Guu
        internal static Harmony harmony;
        
        /// <summary>
        /// Registers this mod for translation
        /// </summary>
        /// <param name="modAssembly">The mod assembly to use to find the files</param>
        /// <param name="otherListeners">Any other listener that needs to be added to the listener pile</param>
        public static void RegisterTranslation(Assembly modAssembly, params MessageDirector.BundlesListener[] otherListeners)
        {
            if (!isInitialized)
            {
                Debug.Log("Guu is not yet initialized, no services can be used until it is!");
                return;
            }
            
            if (GameContext.Instance == null)
            {
                Debug.Log("GameContext is not created yet, can't register translations before the 'Load' step");
                return;
            }
            
            GameContext.Instance.MessageDirector.RegisterBundlesListener((dir) => LanguageController.SetTranslations(dir, modAssembly));

            if (otherListeners == null) return;
            
            foreach (MessageDirector.BundlesListener listener in otherListeners)
                GameContext.Instance.MessageDirector.RegisterBundlesListener(listener);
        }

        /// <summary>
        /// Enables the Guu services for your mod
        /// </summary>
        public static void Init()
        {
            if (isInitialized)
                return;

            harmony = new Harmony("Guu");
            
            assembly = Assembly.GetAssembly(typeof(SRGuu));
            harmony.PatchAll(assembly);
            
            isInitialized = true;
        }
    }
}