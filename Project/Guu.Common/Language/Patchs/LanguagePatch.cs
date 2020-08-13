using System.Collections.Generic;

using HarmonyLib;

namespace Guu.Language.Patchs
{
    [HarmonyPatch(typeof (ResourceBundle))]
    [HarmonyPatch("LoadFromText")]
    internal static class LanguagePatch
    {
        private static void Postfix(string path, Dictionary<string, string> __result, string text)
        {
            if (!LanguageController.translations.TryGetValue(path, out var dictionary))
                return;

            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                if (__result.ContainsKey(keyValuePair.Key))
                    __result[keyValuePair.Key] = keyValuePair.Value;
                else
                    __result.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}