using Assets.Script.Util.Extensions;
using Guu.Components.UI;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace Guu.Core.Patchs
{
    [HarmonyPatch(typeof (MainMenuUI))]
    [HarmonyPatch("Start")]
    public class MenuLoadPatch
    {
        public static void Postfix(MainMenuUI __instance)
        {
            SRGuu.OnMainMenuLoaded(__instance);

            foreach (TMP_Text text in Resources.FindObjectsOfTypeAll<TMP_Text>())
            {
                if (text.GetComponentInParent<TMP_Dropdown>() != null) continue;

                if (text.GetComponent<CustomLangSupport>() == null) text.gameObject.AddComponent<CustomLangSupport>();
            }
        }
    }
}