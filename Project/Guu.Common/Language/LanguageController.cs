using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

using SRML.Utils;

using UnityEngine;

namespace Guu.Language
{
	/// <summary>
	/// A controller to deal with language
	/// </summary>
	public static class LanguageController
	{
		// Resource Bundle Constants
		private const string GLOBAL_BUNDLE = "global";
		private const string ACTOR_BUNDLE = "actor";
		private const string PEDIA_BUNDLE = "pedia";
		private const string UI_BUNDLE = "ui";
		private const string RANGE_BUNDLE = "range";
		private const string BUILD_BUNDLE = "build";
		private const string MAIL_BUNDLE = "mail";
		private const string KEYS_BUNDLE = "keys";
		
		// A language fallback for a language added to the game that does not have the right symbol yet (multiple fallbacks
		// are allowed)
		private static readonly Dictionary<MessageDirector.Lang, List<string>> LANG_FALLBACK = new Dictionary<MessageDirector.Lang, List<string>>();
		
		// To lock the system before it actually reads the language
		// This prevents the auto select system from the game from loading files it can't yet access
		private static bool firstLock = true;

		// The current language to prevent the load for happening every time a scene changes
		private static MessageDirector.Lang? currLang;
		
		// A list of all custom translations added into the game
		internal static readonly Dictionary<string, Dictionary<string, string>> TRANSLATIONS = new Dictionary<string, Dictionary<string, string>>
		{
			{GLOBAL_BUNDLE, new Dictionary<string, string>()},
			{ACTOR_BUNDLE, new Dictionary<string, string>()},
			{PEDIA_BUNDLE, new Dictionary<string, string>()},
			{UI_BUNDLE, new Dictionary<string, string>()},
			{RANGE_BUNDLE, new Dictionary<string, string>()},
			{BUILD_BUNDLE, new Dictionary<string, string>()},
			{MAIL_BUNDLE, new Dictionary<string, string>()},
			{KEYS_BUNDLE, new Dictionary<string, string>()}
		};

		/// <summary>
		/// Sets the translations from the language file for the current language
		/// <para>This will load keys from the language file and add them to
		/// the game's translation directly</para>
		/// </summary>
		/// <param name="dir">The MessageDirector to get the language from</param>
		/// <param name="yourAssembly">The assembly you want to get it from 
		/// (if null will get the relevant assembly for your mod)</param>
		[SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
		public static void SetTranslations(MessageDirector dir, Assembly yourAssembly = null)
		{
			/*if (firstLock)
			{
				firstLock = false;
				return;
			}*/

			MessageDirector.Lang lang = dir.GetCultureLang();

			if (currLang == lang)
				return;

			Assembly assembly = yourAssembly ?? ReflectionUtils.GetRelevantAssembly();
			string code = lang.ToString().ToLower();

			string codeBase = assembly.CodeBase;
			UriBuilder uri = new UriBuilder(codeBase);
			string path = Uri.UnescapeDataString(uri.Path);
			
			FileInfo langFile = new FileInfo(Path.Combine(Path.GetDirectoryName(path), $"Resources\\Lang\\{code}.yaml"));
			if (!langFile.Exists)
			{
				if (LANG_FALLBACK.ContainsKey(lang))
				{
					foreach (string fallback in LANG_FALLBACK[lang])
					{
						code = fallback;
						langFile = new FileInfo(Path.Combine(Path.GetDirectoryName(path),
						                                     $"Resources\\Lang\\{code}.yaml"));

						if (langFile.Exists) break;
					}
				}

				if (!langFile.Exists)
				{
					code = MessageDirector.Lang.EN.ToString().ToLower();
					langFile = new FileInfo(Path.Combine(Path.GetDirectoryName(path), $"Resources\\Lang\\{code}.yaml"));
				}
			}

			using (StreamReader reader = new StreamReader(langFile.FullName))
			{
				foreach (string line in reader.ReadToEnd().Split('\n'))
				{
					if (!line.StartsWith("@import ")) continue;
					
					string imp = line.Replace("@import ", "").Trim().Replace("/", "\\");
					FileInfo extFile = new FileInfo(Path.Combine(Path.GetDirectoryName(path), $"Resources\\Lang\\{code}\\{imp}"));

					if (extFile.FullName.Equals(langFile.FullName)) continue;
					if (extFile.Exists) SetTranslations(extFile);
				}
			}

			currLang = lang;
		}

		/// <summary>
		/// Sets the translations from a language file for the current language
		/// <para>USE THIS ONLY FOR CUSTOM OR EXTERNAL FILES</para>
		/// </summary>
		/// <param name="file">The language file to load</param>
		[SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
		// ReSharper disable once MemberCanBePrivate.Global
		public static void SetTranslations(FileInfo file)
		{
			using (StreamReader reader = new StreamReader(file.FullName))
			{
				foreach (string line in reader.ReadToEnd().Split('\n'))
				{
					if (line.StartsWith("@import "))
					{
						string imp = line.Replace("@import ", "").Trim().Replace("/", "\\");
						FileInfo extFile = new FileInfo(Path.Combine(file.DirectoryName, imp));

						if (extFile.FullName.Equals(file.FullName)) continue;
						if (extFile.Exists) SetTranslations(extFile);
					}

					if (line.StartsWith("#") || line.Equals(string.Empty) || !line.Contains(":"))
						continue;

					string[] args = line.Split(':');
					AddTranslation(args[0], args[1], Unescape(args[2]));
				}
			}
		}

		// Unescapes some characters
		private static string Unescape(string toUnescape)
		{
			return toUnescape.TrimStart().TrimStart('"').TrimEnd('"').Replace("\\n", "\n");
		}

		/// <summary>
		/// Adds a fallback code for a given language
		/// </summary>
		/// <param name="lang">The language to add to</param>
		/// <param name="fallback">The fallback to add</param>
		public static void AddLanguageFallback(MessageDirector.Lang lang, string fallback)
		{
			if (!LANG_FALLBACK.ContainsKey(lang)) LANG_FALLBACK.Add(lang, new List<string>());
			if (!LANG_FALLBACK[lang].Contains(fallback)) LANG_FALLBACK[lang].Add(fallback);
		}

		/// <summary>
		/// Adds a new translation to the provided bundle
		/// </summary>
		/// <param name="bundle">Bundle to add to (can be custom)</param>
		/// <param name="key">Key for the translation</param>
		/// <param name="value">Value of the translation</param>
		public static void AddTranslation(string bundle, string key, string value)
		{
			if (!TRANSLATIONS.ContainsKey(bundle))
				TRANSLATIONS.Add(bundle, new Dictionary<string, string>());

			Debug.Log("ADDING TO BUNDLE " + bundle + "  " + key + " : " + value);
			if (TRANSLATIONS[bundle].ContainsKey(key))
				TRANSLATIONS[bundle][key] = value;
			else
				TRANSLATIONS[bundle].Add(key, value);
		}
		
		/// <summary>
		/// Adds a new translation to the global bundle
		/// </summary>
		/// <param name="key">Key for the translation</param>
		/// <param name="value">Value of the translation</param>
		public static void AddGlobalTranslation(string key, string value)
		{
			AddTranslation(GLOBAL_BUNDLE, key, value);
		}

		/// <summary>
		/// Adds a new translation to the actor bundle
		/// </summary>
		/// <param name="key">Key for the translation</param>
		/// <param name="value">Value of the translation</param>
		public static void AddActorTranslation(string key, string value)
		{
			AddTranslation(ACTOR_BUNDLE, key, value);
		}
		
		/// <summary>
		/// Adds a new translation to the pedia bundle
		/// </summary>
		/// <param name="key">Key for the translation</param>
		/// <param name="value">Value of the translation</param>
		public static void AddPediaTranslation(string key, string value)
		{
			AddTranslation(PEDIA_BUNDLE, key, value);
		}
		
		/// <summary>
		/// Adds a new translation to the ui bundle
		/// </summary>
		/// <param name="key">Key for the translation</param>
		/// <param name="value">Value of the translation</param>
		public static void AddUITranslation(string key, string value)
		{
			AddTranslation(UI_BUNDLE, key, value);
		}
		
		/// <summary>
		/// Adds a new translation to the range bundle
		/// </summary>
		/// <param name="key">Key for the translation</param>
		/// <param name="value">Value of the translation</param>
		public static void AddRangeTranslation(string key, string value)
		{
			AddTranslation(RANGE_BUNDLE, key, value);
		}
		
		/// <summary>
		/// Adds a new translation to the build bundle
		/// </summary>
		/// <param name="key">Key for the translation</param>
		/// <param name="value">Value of the translation</param>
		public static void AddBuildTranslation(string key, string value)
		{
			AddTranslation(BUILD_BUNDLE, key, value);
		}
		
		/// <summary>
		/// Adds a new translation to the mail bundle
		/// </summary>
		/// <param name="key">Key for the translation</param>
		/// <param name="value">Value of the translation</param>
		public static void AddMailTranslation(string key, string value)
		{
			AddTranslation(MAIL_BUNDLE, key, value);
		}
	}
}
