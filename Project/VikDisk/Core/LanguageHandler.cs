using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

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

        internal static bool isNewFont = true;

        // List of all new languages added by the mod
        private static readonly Dictionary<MessageDirector.Lang, string> LANGUAGES = new Dictionary<MessageDirector.Lang, string>()
        {
            {MessageDirector.Lang.EN, "English"}, // English
            {MessageDirector.Lang.DE, "German"}, // Deutsch
            {MessageDirector.Lang.ES, "Spanish"}, // Español
            {MessageDirector.Lang.FR, "French"}, // Français
            {MessageDirector.Lang.RU, "Russian"}, // Pyccкий
            {MessageDirector.Lang.ZH, "Chinese"}, // 中文
            {MessageDirector.Lang.JA, "Japanese"}, // 日本語
            {MessageDirector.Lang.SV, "Swedish"}, //Svenska 
            {MessageDirector.Lang.PT, "Brazilian Pt"}, // Português-Brasil
            {MessageDirector.Lang.KO, "Korean"}, // 한국어
            {Enums.Langs.CS, "Čeština"}, // Čeština 
            {Enums.Langs.PL, "Polish"}, // Polski
            {Enums.Langs.FIL, "Filipino"} // Tagalog
        };
        
        // List of all the languages that require special symbols 
        private static readonly List<MessageDirector.Lang> KANJI_LANGUAGES = new List<MessageDirector.Lang>()
        {
            MessageDirector.Lang.ZH,
            MessageDirector.Lang.JA,
            MessageDirector.Lang.KO
        };

        // Sets up all the languages
        internal static void Setup()
        {
            newFont = TMP_FontAsset.CreateFontAsset(Packs.Global.Get<Font>("CustomFont"));
            newFont.name = "CustomFont";

            foreach (MessageDirector.Lang lang in LANGUAGES.Keys)
                TranslationPatcher.AddUITranslation("l.lang_" + lang.ToString().ToLowerInvariant(), LANGUAGES[lang]);
        }

        // Fixes the language display of the game
        internal static void FixLangDisplay(MessageDirector dir)
        {
            if (oldFont == null) return;
            
            ApplyFontChange(dir);

            /*MessageBundle actor = dir.GetBundle("actor");
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
                    writer.WriteLine("actor:" + key + ": " + actor.Get(key));
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
                    writer.WriteLine("pedia:" + key + ": " + pedia.Get(key));
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
                    writer.WriteLine("ui:" + key + ": " + ui.Get(key));
                }
            }*/
        }

        // Applies the new font to the game
        internal static void ApplyFontChange(MessageDirector dir)
        {
            isNewFont = !KANJI_LANGUAGES.Contains(dir.GetCultureLang());
        }
    }
}