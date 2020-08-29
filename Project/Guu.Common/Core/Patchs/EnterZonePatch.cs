using Guu.Language;
using HarmonyLib;

namespace Guu.Core.Patchs
{
    [HarmonyPatch(typeof (PlayerState))]
    [HarmonyPatch("OnEnteredZone")]
    public class EnterZonePatch
    {
        private static void Postfix(ZoneDirector.Zone zone, PlayerState __instance)
        {
            SRGuu.OnZoneEntered(zone, __instance);
        }
    }
}