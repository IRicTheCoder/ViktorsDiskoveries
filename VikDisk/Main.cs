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

		// ALL LORE GENERATORS
		public static ViktorLoreGen viktor = new ViktorLoreGen();

		// PRE LOAD MOD
		public override void PreLoad()
		{
			Console.Init();

			// Gets the Assembly being executed
			execAssembly = Assembly.GetExecutingAssembly();
			HarmonyInstance.PatchAll(execAssembly);

			// Reads all Mods loaded
			Mods.CheckMods();

			Console.RegisterDumpAction("vacColors", (writer) =>
			{
				foreach (LookupDirector.VacEntry entry in GameContext.Instance.LookupDirector.vacEntries)
				{
					writer.WriteLine($"{entry.id.ToString()} [R: {(int)(entry.color.r * 255f)} G: {(int)(entry.color.g * 255f)} B: {(int)(entry.color.b * 255f)} HEX: #{UnityEngine.ColorUtility.ToHtmlStringRGB(entry.color)}]");
				}
			});

			// Setup each handler
			FoodHandler.Instance.Setup();
			GardenHandler.Instance.Setup();
			MailHandler.Instance.Setup();
			PediaHandler.Instance.Setup();
			AmmoHandler.Instance.Setup();

			// Register callbacks for the lore generators
			viktor.RegisterCallbacks();

			UnityEngine.SceneManagement.SceneManager.activeSceneChanged += ChangeScene;
		}

		public void ChangeScene(UnityEngine.SceneManagement.Scene old, UnityEngine.SceneManagement.Scene scene)
		{
			Console.Log($"{scene.buildIndex}: {scene.name ?? "NoName"}");
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

				if (Configs.Food.tarrRecipe.ContainsKey(slimeDefinition.IdentifiableId))
					FoodHandler.Instance.AddTarrBuilder(diet, Configs.Food.tarrRecipe[slimeDefinition.IdentifiableId]);

				// Grabs all plot results
				foreach (SlimeDiet.EatMapEntry eatMapEntry in diet.EatMap)
				{
					if (Identifiable.IsPlort(eatMapEntry.producesId))
						plortSet.Add(eatMapEntry.producesId);
				}

				// Removes all foods to prevent the game from yeilding multiple results
				FoodHandler.Instance.RemoveFoods(plortSet, diet);

				// Registers all new foods
				FoodHandler.Instance.RegisterFoods(plortSet, diet);
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

			AmmoHandler.Instance.RegisterSlimes();
			AmmoHandler.Instance.RegisterStorage();
		}
	}

	// DELEGATES FOR USE WITH THE MOD
	public delegate void ModAction();
	public delegate void ModAction<A>(A a);
	public delegate void ModAction<A1, A2>(A1 a1, A2 a2);
	public delegate void ModAction<A1, A2, A3>(A1 a1, A2 a2, A3 a3);

	public delegate R ModFunc<out R>();
	public delegate R ModFunc<A, out R>(A a);
	public delegate R ModFunc<A1, A2, out R>(A1 a1, A2 a2);
	public delegate R ModFunc<A1, A2, A3, out R>(A1 a1, A2 a2, A3 a3);

}
