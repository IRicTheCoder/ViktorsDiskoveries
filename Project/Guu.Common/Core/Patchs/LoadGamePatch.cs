using HarmonyLib;
using TMPro;

namespace Guu.Core.Patchs
{
    [HarmonyPatch(typeof (SceneContext))]
    [HarmonyPatch("Start")]
    internal static class LoadGamePatch
    {
        private static void Postfix(SceneContext __instance)
        {
            SRGuu.OnGameLoaded(__instance);
        }

        private static void Prefix(SceneContext __instance)
        {
            SRGuu.OnPreLoadGame(__instance);
        }
    }
}