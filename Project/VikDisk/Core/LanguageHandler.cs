using System.Collections.Generic;

using JetBrains.Annotations;

using SRML.SR;

using UnityEngine;

namespace VikDisk.Core
{
    /// <summary>
    /// Handles the game language additions
    /// </summary>
    public static class LanguageHandler
    {
        // The new font to work with languages not supported by the game natively
        private static readonly Font newFont = Packs.Chapter1.Get<Font>("CustomFont");
        
        // List of the default languages, used to fix language translation
        private static readonly List<MessageDirector.Lang> DEFAULT_LANGUAGES = new List<MessageDirector.Lang>()
        {
            
        };
        
        // List of all new languages added by the mod
        private static readonly Dictionary<MessageDirector.Lang, string> NEW_LANGUAGES = new Dictionary<MessageDirector.Lang, string>()
        {
            {Enums.Langs.CZ, "Czech"},
            {Enums.Langs.PL, "Polish"}
        };

        // Sets up all the languages
        internal static void Setup()
        {
            foreach (MessageDirector.Lang lang in DEFAULT_LANGUAGES)
            {
                
            }
            
            foreach (MessageDirector.Lang lang in NEW_LANGUAGES.Keys)
                TranslationPatcher.AddUITranslation("l.lang_" + lang.ToString().ToLowerInvariant(), NEW_LANGUAGES[lang]);
        }
    }
}