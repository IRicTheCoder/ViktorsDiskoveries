using System.Collections.Generic;
using System.IO;
using Guu.Language;
using TMPro;
using UnityEngine;

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
        internal static TMP_FontAsset newFontHebrew;
        internal static TMP_FontAsset newFontArmenian;

        // List of all new languages added by the mod
        private static readonly Dictionary<MessageDirector.Lang, string> LANGUAGES = new Dictionary<MessageDirector.Lang, string>
        {
            {MessageDirector.Lang.EN, "English"}, // English
            {MessageDirector.Lang.DE, "Deutsch"}, // German
            {MessageDirector.Lang.ES, "Español"}, // Spanish
            {MessageDirector.Lang.FR, "Français"}, // French
            {MessageDirector.Lang.RU, "Pyccкий"}, // Russian
            {MessageDirector.Lang.ZH, "中文"}, // Chinese
            {MessageDirector.Lang.JA, "日本語"}, // Japanese
            {MessageDirector.Lang.SV, "Svenska"}, // Swedish 
            {MessageDirector.Lang.PT, "Português-Brasil"}, // Brasilian Portuguese
            {MessageDirector.Lang.KO, "한국어"}, // Korean
            {Enums.Langs.CS, "Čeština"}, // Czech 
            {Enums.Langs.PL, "Polski"}, // Polish
            {Enums.Langs.FIL, "Tagalog"}, // Tagalog / Filipino
            {Enums.Langs.HE, "עברית"}, // Hebrew
            {Enums.Langs.HY, "հայերեն"} // Armenian
        };
        
        // List of all languages that are RTL
        internal static readonly List<MessageDirector.Lang> RTL_LANGUAGES = new List<MessageDirector.Lang>
        {
            Enums.Langs.HE
        };

        // Sets up all the languages
        internal static void Setup()
        {
            newFont = TMP_FontAsset.CreateFontAsset(Packs.Global.Get<Font>("CustomFont"));
            newFont.name = "CustomFont";
            
            newFontHebrew = TMP_FontAsset.CreateFontAsset(Packs.Global.Get<Font>("CustomFontHebrew"));
            newFontHebrew.name = "CustomFontHebrew";
            
            newFontArmenian = TMP_FontAsset.CreateFontAsset(Packs.Global.Get<Font>("CustomFontArmenian"));
            newFontArmenian.name = "CustomFontArmenian";

            foreach (MessageDirector.Lang lang in LANGUAGES.Keys)
            {
                LanguageController.AddUITranslation("l.lang_" + lang.ToString().ToLowerInvariant(),
                                                    RTL_LANGUAGES.Contains(lang) ? LANGUAGES[lang].Reverse() : LANGUAGES[lang]);
            }

            LanguageController.AddLanguageFallback(Enums.Langs.FIL, "tl");
            LanguageController.AddLanguageFallback(Enums.Langs.HY, "arm");
        }

        // Fixes the language display of the game
        internal static void FixLangDisplay(MessageDirector dir)
        {
            string[] bundles = {
                "actor", "pedia", "ui", "range", "build", "mail", "keys"
            };

            foreach (string bundle in bundles)
            {
                MessageBundle actor = dir.GetBundle(bundle);
                ResourceBundle rActor = actor.GetPrivateField<ResourceBundle>("bundle");

                FileInfo fActor = new FileInfo(Application.dataPath + $"/{bundle}.yaml");

                using (StreamWriter writer = fActor.CreateText())
                {
                    writer.WriteLine("#=====================================");
                    writer.WriteLine("# AUTO GENERATED FROM THE GAME");
                    writer.WriteLine("#=====================================");
                    writer.WriteLine("");

                    foreach (string key in rActor.GetKeys())
                    {
                        writer.WriteLine($"{bundle}:" + key + ": \"" +
                                         actor.Get(key).Replace("\"", "\\\"").Replace("\n", "\\n") + "\"");
                    }
                }
            }
        }
    }
}