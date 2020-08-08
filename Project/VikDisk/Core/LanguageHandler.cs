using System;
using System.Collections.Generic;

using JetBrains.Annotations;

using SRML.SR;

using TMPro;

using UnityEngine;

using Object = UnityEngine.Object;

namespace VikDisk.Core
{
    /// <summary>
    /// Handles the game language additions
    /// </summary>
    public static class LanguageHandler
    {
        // The fonts to use for the game, the new font is to support special symbols
        internal static TMP_FontAsset oldFont;
        internal static TMP_FontAsset newFont;
        
        // The type of font and the cache of that value
        internal static bool symbolFont = false;
        internal static bool hasSymbolFont = false;

        // List of all new languages added by the mod
        private static readonly Dictionary<MessageDirector.Lang, string> LANGUAGES = new Dictionary<MessageDirector.Lang, string>()
        {
            {MessageDirector.Lang.EN, "English"},
            {MessageDirector.Lang.DE, "Deutsch"},
            {MessageDirector.Lang.ES, "Español"},
            {MessageDirector.Lang.FR, "Français"},
            {MessageDirector.Lang.RU, "Pyccкий"},
            {MessageDirector.Lang.ZH, "中文"},
            {MessageDirector.Lang.JA, "日本語"},
            {MessageDirector.Lang.SV, "Svenska"},
            {MessageDirector.Lang.PT, "Português-Brasil"},
            {MessageDirector.Lang.KO, "한국어"},
            {Enums.Langs.CZ, "Čeština"},
            {Enums.Langs.PL, "Polski"}
        };
        
        // List of all the languages that require special symbols 
        private static readonly List<MessageDirector.Lang> SYMBOL_LANGUAGES = new List<MessageDirector.Lang>()
        {
            Enums.Langs.CZ,
            Enums.Langs.PL
        };

        // Sets up all the languages
        internal static void Setup()
        {
            newFont = TMP_FontAsset.CreateFontAsset(Packs.Global.Get<Font>("CustomFont"));

            foreach (MessageDirector.Lang lang in LANGUAGES.Keys)
                TranslationPatcher.AddUITranslation("l.lang_" + lang.ToString().ToLowerInvariant(), LANGUAGES[lang]);
        }

        // Fixes the language display of the game
        internal static void FixLangDisplay(MessageDirector dir)
        {
            if (oldFont == null) return;
            
            /*hasSymbolFont = symbolFont;
            symbolFont = SYMBOL_LANGUAGES.Contains(dir.GetCultureLang());

            if (symbolFont)
            {
                TMP_Text[] texts = Resources.FindObjectsOfTypeAll<TMP_Text>();
                foreach (TMP_Text text in texts) text.font = newFont;
            }
            else if (hasSymbolFont)
            {
                TMP_Text[] texts = Resources.FindObjectsOfTypeAll<TMP_Text>();
                foreach (TMP_Text text in texts) text.font = oldFont;
            }*/
            
            
        }
    }
}