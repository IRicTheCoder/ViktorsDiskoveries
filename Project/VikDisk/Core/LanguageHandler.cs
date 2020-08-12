using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using Guu.Language;

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

        // List of all new languages added by the mod
        private static readonly Dictionary<MessageDirector.Lang, string> LANGUAGES = new Dictionary<MessageDirector.Lang, string>()
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
            {Enums.Langs.FIL, "Tagalog"} // Tagalog / Filipino
        };

        // Sets up all the languages
        internal static void Setup()
        {
            newFont = TMP_FontAsset.CreateFontAsset(Packs.Global.Get<Font>("CustomFont"));
            newFont.name = "CustomFont";

            foreach (MessageDirector.Lang lang in LANGUAGES.Keys)
                TranslationPatcher.AddUITranslation("l.lang_" + lang.ToString().ToLowerInvariant(), LANGUAGES[lang]);
            
            LanguageController.AddLanguageFallback(Enums.Langs.FIL, "tl");
        }

        // Fixes the language display of the game
        internal static void FixLangDisplay(MessageDirector dir)
        {
            MessageBundle actor = dir.GetBundle("actor");
            ResourceBundle rActor = actor.GetPrivateField<ResourceBundle>("bundle");

            FileInfo fActor = new FileInfo(Application.dataPath + "/actor.yaml");

            using (StreamWriter writer = fActor.CreateText())
            {
                writer.WriteLine("#=====================================");
                writer.WriteLine("# AUTO GENERATED FROM THE GAME");
                writer.WriteLine("#=====================================");
                writer.WriteLine("");
                
                foreach (string key in rActor.GetKeys())
                {
                    writer.WriteLine("actor:" + key + ": \"" + actor.Get(key).Replace("\"", "\\\"") + "\"");
                }
            }
            
            MessageBundle pedia = dir.GetBundle("pedia");
            ResourceBundle rPedia = pedia.GetPrivateField<ResourceBundle>("bundle");

            FileInfo fPedia = new FileInfo(Application.dataPath + "/pedia.yaml");

            using (StreamWriter writer = fPedia.CreateText())
            {
                writer.WriteLine("#=====================================");
                writer.WriteLine("# AUTO GENERATED FROM THE GAME");
                writer.WriteLine("#=====================================");
                writer.WriteLine("");
                
                foreach (string key in rPedia.GetKeys())
                {
                    writer.WriteLine("pedia:" + key + ": \"" + pedia.Get(key).Replace("\"", "\\\"") + "\"");
                }
            }
            
            MessageBundle ui = dir.GetBundle("ui");
            ResourceBundle rUi = ui.GetPrivateField<ResourceBundle>("bundle");

            FileInfo fUi = new FileInfo(Application.dataPath + "/ui.yaml");

            using (StreamWriter writer = fUi.CreateText())
            {
                writer.WriteLine("#=====================================");
                writer.WriteLine("# AUTO GENERATED FROM THE GAME");
                writer.WriteLine("#=====================================");
                writer.WriteLine("");
                
                foreach (string key in rUi.GetKeys())
                {
                    writer.WriteLine("ui:" + key + ": \"" + ui.Get(key).Replace("\"", "\\\"") + "\"");
                }
            }
        }
    }
}