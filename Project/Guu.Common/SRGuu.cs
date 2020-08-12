using System.Reflection;

using Guu.Language;

using UnityEngine;

namespace Guu
{
    // TODO: FINISH THIS
    public class SRGuu
    {
        /// <summary>
        /// Registers this mod for translation
        /// </summary>
        /// <param name="modAssembly">The mod assembly to use to find the files</param>
        /// <param name="otherListeners">Any other listener that needs to be added to the listener pile</param>
        public static void RegisterTranslation(Assembly modAssembly, params MessageDirector.BundlesListener[] otherListeners)
        {
            if (GameContext.Instance == null)
            {
                Debug.Log("GameContext is not created yet, can't register translations before the 'Load' step");
                return;
            }
            
            GameContext.Instance.MessageDirector.RegisterBundlesListener((dir) => LanguageController.SetTranslations(modAssembly));

            if (otherListeners == null) return;
            
            foreach (MessageDirector.BundlesListener listener in otherListeners)
            {
                GameContext.Instance.MessageDirector.RegisterBundlesListener(listener);
            }
        }
    }
}