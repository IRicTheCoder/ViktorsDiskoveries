using System.Collections.Generic;
using HarmonyLib;

namespace VikDisk.Handlers
{
	/// <summary>
	/// Handles the food for the slimes
	/// </summary>
	public class FoodHandler : Handler<FoodHandler>
	{
		/// <summary>
		/// Setups the handler
		/// </summary>
		public override void Setup()
		{
			// NO SETUP REQUIRED FOR NOW
		}

		// TODO: Add slime diet processing here

		/// <summary>
		/// Adds Tarr Building recipes (essentially for Casual Mode)
		/// </summary>
		public void AddTarrBuilder(SlimeDiet diet, Identifiable.Id id)
		{
			diet.EatMap.Add(new SlimeDiet.EatMapEntry
			{
				eats = id,
				producesId = Identifiable.Id.NONE,
				becomesId = Identifiable.Id.TARR_SLIME,
				isFavorite = false,
				favoriteProductionCount = 0,
				driver = SlimeEmotions.Emotion.NONE
			});
		}

		/// <summary>
		/// Removes the foods from the old eat maps to add the new ones
		/// </summary>
		public void RemoveFoods(HashSet<Identifiable.Id> plortSet, SlimeDiet diet)
		{
			foreach (Identifiable.Id id in plortSet)
			{
				// Remove all kookadoba results for any of the changed results
				if (Configs.Food.kookaResults.ContainsKey(id))
					diet.EatMap.RemoveAll((SlimeDiet.EatMapEntry entry) => entry.eats == Identifiable.Id.KOOKADOBA_FRUIT && Identifiable.IsPlort(entry.producesId));

				// Remove all spicy tofu results for any of the changed results
				if (Configs.Food.tofuResults.ContainsKey(id))
					diet.EatMap.RemoveAll((SlimeDiet.EatMapEntry entry) => entry.eats == Identifiable.Id.SPICY_TOFU && Identifiable.IsPlort(entry.producesId));

				// Removes all gilded ginger results from all slimes except Gold Slime
				if (id != Identifiable.Id.GOLD_PLORT)
					diet.EatMap.RemoveAll((SlimeDiet.EatMapEntry entry) => entry.eats == Identifiable.Id.GINGER_VEGGIE && Identifiable.IsPlort(entry.producesId) && !entry.isFavorite);

				// Removes new saber favorites from the list
				if (id == Identifiable.Id.SABER_PLORT)
					diet.EatMap.RemoveAll((SlimeDiet.EatMapEntry entry) => Configs.Food.saberFavs.Contains(entry.eats) && Identifiable.IsPlort(entry.producesId) && !entry.isFavorite);

				// Removes new pink favorites from the list
				if (id == Identifiable.Id.PINK_PLORT)
					diet.EatMap.RemoveAll((SlimeDiet.EatMapEntry entry) => Configs.Food.pinkFavs.Contains(entry.eats) && Identifiable.IsPlort(entry.producesId) && !entry.isFavorite);
			}
		}

		/// <summary>
		/// Registers the new foods
		/// </summary>
		public void RegisterFoods(HashSet<Identifiable.Id> plortSet, SlimeDiet diet)
		{
			foreach (Identifiable.Id id in plortSet)
			{
				Identifiable.Id producesId; // The resulting id from dictionaries

				// Adds the tofu results when acceptable
				if (Configs.Food.tofuResults.TryGetValue(id, out producesId))
				{
					//foreach (Identifiable.Id plortId in plortSet)
					//{
					diet.EatMap.Add(new SlimeDiet.EatMapEntry
					{
						eats = Identifiable.Id.SPICY_TOFU,
						producesId = producesId,
						isFavorite = true,
						favoriteProductionCount = Configs.Food.SCI_PROD,
						driver = SlimeEmotions.Emotion.HUNGER
					});
					//}
					diet.Favorites.AddItem(Identifiable.Id.SPICY_TOFU);
				}

				// Adds the kookadoba results when acceptable
				if (Configs.Food.kookaResults.TryGetValue(id, out producesId))
				{
					diet.EatMap.Add(new SlimeDiet.EatMapEntry
					{
						eats = Identifiable.Id.KOOKADOBA_FRUIT,
						producesId = producesId,
						isFavorite = true,
						favoriteProductionCount = Configs.Food.SCI_PROD,
						driver = SlimeEmotions.Emotion.HUNGER
					});
					diet.Favorites.AddItem(Identifiable.Id.KOOKADOBA_FRUIT);
				}

				// Adds the favorites for the Saber Slime
				if (id == Identifiable.Id.SABER_PLORT)
				{
					foreach (Identifiable.Id favId in Configs.Food.saberFavs)
					{
						diet.EatMap.Add(new SlimeDiet.EatMapEntry
						{
							eats = favId,
							producesId = id,
							isFavorite = true,
							favoriteProductionCount = Configs.Food.FAV_PROD,
							driver = SlimeEmotions.Emotion.HUNGER
						});
						diet.Favorites.AddItem(favId);
					}
				}

				// Adds the favorites for the Pink Slime
				if (id == Identifiable.Id.PINK_PLORT)
				{
					foreach (Identifiable.Id favId in Configs.Food.pinkFavs)
					{
						diet.EatMap.Add(new SlimeDiet.EatMapEntry
						{
							eats = favId,
							producesId = id,
							isFavorite = true,
							favoriteProductionCount = Configs.Food.FAV_PROD,
							driver = SlimeEmotions.Emotion.HUNGER
						});
						diet.Favorites.AddItem(favId);
					}
				}

				// Adds gilded ginger as a super favorite
				if (id != Identifiable.Id.GOLD_PLORT)
				{
					diet.EatMap.Add(new SlimeDiet.EatMapEntry
					{
						eats = Identifiable.Id.GINGER_VEGGIE,
						producesId = id,
						isFavorite = true,
						favoriteProductionCount = Configs.Food.SFAV_PROD,
						driver = SlimeEmotions.Emotion.HUNGER
					});
					diet.Favorites.AddItem(Identifiable.Id.GINGER_VEGGIE);
				}
			}
		}
	}
}
