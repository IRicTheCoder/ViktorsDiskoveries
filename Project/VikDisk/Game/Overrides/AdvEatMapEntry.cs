using HarmonyLib;
using UnityEngine;

namespace VikDisk.Game.Overrides
{
    /// <summary>
    /// An advanced eat map to allow more control
    /// </summary>
    public class AdvEatMapEntry : SlimeDiet.EatMapEntry
    {
        public bool isRange = false;
        public int minCount = 0;
        public int maxCount = 1;

        [HarmonyPatch(typeof(SlimeDiet.EatMapEntry))]
        [HarmonyPatch("NumToProduce")]
        private static class EatMapPatch
        {
            private static bool Prefix(SlimeDiet.EatMapEntry __instance, ref int __result)
            {
                if (!(__instance is AdvEatMapEntry adv)) return true;

                if (!adv.isRange) return true;
                
                __result = Random.Range(adv.minCount, adv.maxCount);

                return false;

            }
        }
    }
}