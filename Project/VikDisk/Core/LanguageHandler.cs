using System;
using System.Collections.Generic;

using SRML.SR;

namespace VikDisk.Core
{
    /// <summary>
    /// Handles the game language additions
    /// </summary>
    public static class LanguageHandler
    {
        // The new font to work with languages not supported by the game natively
        //private static readonly Font newFont = Packs.Chapter1.Get<Font>("CustomFont");
        
        // List of the default languages, used to fix language translation
        private static readonly List<MessageDirector.Lang> DEFAULT_LANGUAGES = new List<MessageDirector.Lang>()
        {
            MessageDirector.Lang.DE,
            MessageDirector.Lang.EN,
            MessageDirector.Lang.ES,
            MessageDirector.Lang.FR,
            MessageDirector.Lang.JA,
            MessageDirector.Lang.KO,
            MessageDirector.Lang.PT,
            MessageDirector.Lang.RU,
            MessageDirector.Lang.SV,
            MessageDirector.Lang.ZH
        };
        
        // List of all new languages added by the mod
        private static readonly Dictionary<MessageDirector.Lang, string> NEW_LANGUAGES = new Dictionary<MessageDirector.Lang, string>()
        {
            {Enums.Langs.CZ, "Čeština"},
            {Enums.Langs.PL, "Polski"}
        };

        // Sets up all the languages
        internal static void Setup()
        {
            foreach (MessageDirector.Lang lang in NEW_LANGUAGES.Keys)
                TranslationPatcher.AddUITranslation("l.lang_" + lang.ToString().ToLowerInvariant(), NEW_LANGUAGES[lang]);
        }

        // Fixes the language display of the game
        internal static void FixLangs()
        {
            foreach (MessageDirector.Lang lang in DEFAULT_LANGUAGES)
            {
                string toFix = GameContext.Instance.MessageDirector.Get("ui", "l.lang_" + lang.ToString().ToLowerInvariant());
                toFix = toFix.Substring(toFix.IndexOf("(", StringComparison.Ordinal));
                TranslationPatcher.AddUITranslation("l.lang_" + lang.ToString().ToLowerInvariant(), toFix);
            }
        }
    }
}