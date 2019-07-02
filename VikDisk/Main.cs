using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using SRML;
using VikDisk.Handlers;
using VikDisk.LoreGen;

namespace VikDisk
{
	/// <summary>
	/// The main class and entry point for the mod
	/// </summary>
	public class Main : ModEntryPoint
	{
		// THE EXECUTING ASSEMBLY
		public static Assembly execAssembly;

		// ALL THE HANDLERS
		public static FoodHandler food = new FoodHandler();
		public static GardenHandler garden = new GardenHandler();
		public static MailHandler mail = new MailHandler();
		public static PediaHandler pedia = new PediaHandler();
		public static VacHandler vac = new VacHandler();

		// ALL LORE GENERATORS
		public static ViktorLoreGen viktor = new ViktorLoreGen();

		// PRE LOAD MOD
		public override void PreLoad()
		{
			// Gets the Assembly being executed
			execAssembly = Assembly.GetExecutingAssembly();
			HarmonyInstance.PatchAll(execAssembly);

			// Add mails into the game
			mail.AddMails();

			// Fixes the Slimepedia Entries
			pedia.FixEntries();

			// Register callbacks for the lore generators
			viktor.RegisterCallbacks();
		}

		// POST LOAD MOD
		public override void PostLoad()
		{
			// TODO: Add this to their respective places

			// Go through all slimes to change the needed stuff
			foreach (SlimeDefinition slimeDefinition in SRSingleton<GameContext>.Instance.SlimeDefinitions.Slimes)
			{
				HashSet<Identifiable.Id> plortSet = new HashSet<Identifiable.Id>();
				SlimeDiet diet = slimeDefinition.Diet;

				// Grabs all plot results
				foreach (SlimeDiet.EatMapEntry eatMapEntry in diet.EatMap)
				{
					if (Identifiable.IsPlort(eatMapEntry.producesId))
						plortSet.Add(eatMapEntry.producesId);
				}

				// Removes all foods to prevent the game from yeilding multiple results
				food.RemoveFoods(plortSet, diet);

				// Registers all new foods
				food.RegisterFoods(plortSet, diet);
			}
			
			// Makes all craft resources also plorts
			foreach (Identifiable.Id item in Identifiable.CRAFT_CLASS)
			{
				Identifiable.PLORT_CLASS.AddItem(item);
			}

			// Ensures the new growables are in the right classes
			if (!Identifiable.FRUIT_CLASS.Contains(Identifiable.Id.KOOKADOBA_FRUIT))
				Identifiable.FRUIT_CLASS.Add(Identifiable.Id.KOOKADOBA_FRUIT);

			if (!Identifiable.VEGGIE_CLASS.Contains(Identifiable.Id.GINGER_VEGGIE))
				Identifiable.VEGGIE_CLASS.Add(Identifiable.Id.GINGER_VEGGIE);

			vac.RegisterNewVacSlimes();
		}
	}
}
