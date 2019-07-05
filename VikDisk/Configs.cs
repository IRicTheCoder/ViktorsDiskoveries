using System.Collections.Generic;
using VikDisk.Handlers;
using UnityEngine;

namespace VikDisk
{
	/// <summary>
	/// Contains all internal configs
	/// </summary>
	public static class Configs
	{
		//---------------------------------------------------
		// MODS RELATED
		//---------------------------------------------------

		public static class Mods
		{
			// CONSTANTS WITH MOD NAMES/ASSEMBLIES
			public const string UMF_ITSELF = "UMF:uModFramework";
			public const string VACUUM_MANIA = "UMF:SRVacuumMania";
		}

		//---------------------------------------------------
		// AMMO RELATED
		//---------------------------------------------------

		public static class Ammo
		{
			// BASE VAC ENTRIES (ALREADY HAVE AN ENTRY JUST NEED THE AMMO)
			public static readonly Identifiable.Id[] vanillaVacs = new[]
			{
				Identifiable.Id.SABER_SLIME,
				Identifiable.Id.LUCKY_SLIME,
				Identifiable.Id.GOLD_SLIME,
				Identifiable.Id.TARR_SLIME
			};

			// FIXES THE COLORS OF CERTAIN VAC ENTRIES
			public static readonly Dictionary<Identifiable.Id, Color> fixColors = new Dictionary<Identifiable.Id, Color>
			{
				{
					Identifiable.Id.SABER_SLIME,
					Color.yellow * 0.5f
				}
			};
		}

		//---------------------------------------------------
		// FOOD RELATED
		//---------------------------------------------------

		public static class Food
		{
			// CONSTANTS
			public const int FAV_PROD = 2; // Amount produced by normal favorite foods
			public const int SCI_PROD = 6; // Amount of slime resources produced by Kookadoba and Spicy Tofu
			public const int SFAV_PROD = 3; // Amount produced by super favorite foods

			// TARR SLIME RECIPES
			// KEY = SLIME TO TURN; VALUE = STUFF TO EAT IN ORDER TO TURN
			public static readonly Dictionary<Identifiable.Id, Identifiable.Id> tarrRecipe = new Dictionary<Identifiable.Id, Identifiable.Id>
			{
				{
					Identifiable.Id.LUCKY_SLIME,
					Identifiable.Id.MANIFOLD_CUBE_CRAFT
				}
			};

			// KOOKADOBA FOOD RESULTS
			// KEY = TARGET; VALUE = NEW RESULT
			public static readonly Dictionary<Identifiable.Id, Identifiable.Id> kookaResults = new Dictionary<Identifiable.Id, Identifiable.Id>
			{
				{
					Identifiable.Id.PINK_PLORT,
					Identifiable.Id.JELLYSTONE_CRAFT
				},
				{
					Identifiable.Id.ROCK_PLORT,
					Identifiable.Id.SLIME_FOSSIL_CRAFT
				},
				{
					Identifiable.Id.PHOSPHOR_PLORT,
					Identifiable.Id.DEEP_BRINE_CRAFT
				},
				{
					Identifiable.Id.TABBY_PLORT,
					Identifiable.Id.ROYAL_JELLY_CRAFT
				},
				{
					Identifiable.Id.RAD_PLORT,
					Identifiable.Id.SPIRAL_STEAM_CRAFT
				},
				{
					Identifiable.Id.HONEY_PLORT,
					Identifiable.Id.WILD_HONEY_CRAFT
				},
				{
					Identifiable.Id.BOOM_PLORT,
					Identifiable.Id.LAVA_DUST_CRAFT
				},
				{
					Identifiable.Id.CRYSTAL_PLORT,
					Identifiable.Id.GLASS_SHARD_CRAFT
				},
				{
					Identifiable.Id.QUANTUM_PLORT,
					Identifiable.Id.PRIMORDY_OIL_CRAFT
				},
				{
					Identifiable.Id.DERVISH_PLORT,
					Identifiable.Id.SILKY_SAND_CRAFT
				},
				{
					Identifiable.Id.HUNTER_PLORT,
					Identifiable.Id.HEXACOMB_CRAFT
				},
				{
					Identifiable.Id.MOSAIC_PLORT,
					Identifiable.Id.INDIGONIUM_CRAFT
				},
				{
					Identifiable.Id.TANGLE_PLORT,
					Identifiable.Id.BUZZ_WAX_CRAFT
				},
				{
					Identifiable.Id.SABER_PLORT,
					Identifiable.Id.PEPPER_JAM_CRAFT
				}
			};

			// SPICY TOFU RESULTS
			// KEY = TARGET; VALUE = NEW RESULT
			public static readonly Dictionary<Identifiable.Id, Identifiable.Id> tofuResults = new Dictionary<Identifiable.Id, Identifiable.Id>
			{
				{
					Identifiable.Id.PINK_PLORT,
					Identifiable.Id.MANIFOLD_CUBE_CRAFT
				},
				{
					Identifiable.Id.CRYSTAL_PLORT,
					Identifiable.Id.STRANGE_DIAMOND_CRAFT
				}
			};

			// PINK SLIME FAVORITES
			public static readonly List<Identifiable.Id> pinkFavs = new List<Identifiable.Id>
			{
				Identifiable.Id.CARROT_VEGGIE,
				Identifiable.Id.POGO_FRUIT,
				Identifiable.Id.HEN
			};

			// SABER SLIME FAVORITES
			public static readonly List<Identifiable.Id> saberFavs = new List<Identifiable.Id>
			{
				Identifiable.Id.ELDER_HEN,
				Identifiable.Id.ELDER_ROOSTER
			};

			// GROWABLES TO ADD TO GARDENS
			// KEY = STUFF TO GROW; VALUE = IS VEGETABLE?
			public static readonly Dictionary<Identifiable.Id, bool> growables = new Dictionary<Identifiable.Id, bool>
			{
				{
					Identifiable.Id.KOOKADOBA_FRUIT,
					false
				},
				{
					Identifiable.Id.GINGER_VEGGIE,
					true
				}
			};
		}

		//---------------------------------------------------
		// MAIL RELATED
		//---------------------------------------------------

		public static class Mails
		{
			// ALL MAIL KEYS
			public const string INTRO_MAIL_KEY = "ViktorIntro";

			// MAIL INFOS
			public static readonly List<MailHandler.MailInfo> mails = new List<MailHandler.MailInfo>
			{
				new MailHandler.MailInfo(INTRO_MAIL_KEY, "New Findings on Slimes!!", "Viktor Humphries")
			};
		}
	}
}
