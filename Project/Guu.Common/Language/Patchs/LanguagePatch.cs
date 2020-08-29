using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace Guu.Language.Patchs
{
    [HarmonyPatch(typeof (ResourceBundle))]
    [HarmonyPatch("LoadFromText")]
    internal static class LanguagePatch
    {
        [HarmonyPriority(Priority.Last)]
        private static void Postfix(string path, Dictionary<string, string> __result, string text)
        {
            LanguageController.ResetTranslations(GameContext.Instance.MessageDirector);

            if (!LanguageController.TRANSLATIONS.ContainsKey(path))
            {
                Debug.Log("Missing bundle in translations: " + path);
                return;
            }
            
            foreach (KeyValuePair<string, string> keyValuePair in LanguageController.TRANSLATIONS[path])
            {
                if (__result.ContainsKey(keyValuePair.Key))
                    __result[keyValuePair.Key] = keyValuePair.Value;
                else
                    __result.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}