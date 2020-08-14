using System.Collections.Generic;

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
            foreach (KeyValuePair<string, string> keyValuePair in LanguageController.TRANSLATIONS[path])
                __result[keyValuePair.Key] = keyValuePair.Value;
        }
    }
}