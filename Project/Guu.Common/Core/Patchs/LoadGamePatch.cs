using Guu.Components.System;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace Guu.Core.Patchs
{
    [HarmonyPatch(typeof (SceneContext))]
    [HarmonyPatch("Start")]
    internal static class LoadGamePatch
    {
        private static void Postfix(SceneContext __instance)
        {
            SRGuu.OnGameLoaded(__instance);
            
            // ReSharper disable once ObjectCreationAsStatement
            new GameObject("SystemUpdater", typeof(SystemUpdater));
        }

        private static void Prefix(SceneContext __instance)
        {
            SRGuu.OnPreLoadGame(__instance);
        }
    }
}