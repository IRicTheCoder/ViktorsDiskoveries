using System.Reflection;
using Guu.Language;

using UnityEngine;

namespace Guu
{
    // TODO: FINISH THIS
    public partial class SRGuu
    {
        /// <summary>
        /// Registers this mod for translation
        /// </summary>
        /// <param name="modAssembly">The mod assembly to use to find the files</param>
        /// <param name="listeners">Any listener that needs to be added to the MessageDirector listener pile</param>
        public static void RegisterTranslation(Assembly modAssembly, params MessageDirector.BundlesListener[] listeners)
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
            
            LanguageController.TranslationReset += dir => LanguageController.SetTranslations(dir, modAssembly);

            if (listeners == null) return;
            
            foreach (MessageDirector.BundlesListener listener in listeners)
                GameContext.Instance.MessageDirector.RegisterBundlesListener(listener);
        }
    }
}