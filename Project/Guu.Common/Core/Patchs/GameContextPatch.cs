using Guu.Language;
using HarmonyLib;

namespace Guu.Core.Patchs
{
    [HarmonyPatch(typeof (GameContext))]
    [HarmonyPatch("Start")]
    public class GameContextPatch
    {
        private static void Postfix(GameContext __instance)
        {
            SRGuu.OnGameContextReady(__instance);
        }
    }
}