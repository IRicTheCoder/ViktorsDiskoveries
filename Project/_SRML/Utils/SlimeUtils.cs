using System.Collections.Generic;
using SRML;
using SRML.SR;
using UnityEngine;
using VikDisk.SRML.API;
using static SlimeEat;

namespace VikDisk.SRML
{
	/// <summary>
	/// An utility class to help with Slimes
	/// </summary>
	public static class SlimeUtils
	{
		/// <summary>A constant that represents the min. drive to eat</summary>
		public const float EAT_MIN_DRIVE = 0f;

		/// <summary>A constant that represents the min. drive to eat plorts</summary>
		public const float EAT_PLORT_MIN_DRIVE = 0.5f;

		/// <summary>A constant that represents the default extra drive in diets</summary>
		public const float DEFAULT_EXTRA_DRIVE = 0f;

		/// <summary>A constant that represents the default favorite production count</summary>
		public const int DEFAULT_FAV_COUNT = 2;

		/// <summary>
		/// Add food to a food group for all slimes inside that group
		/// </summary>
		/// <param name="ID">ID of the food to add</param>
		/// <param name="group">Group of food to add in</param>
		/// <param name="favoritedBy">The IDs for the slimes that favorite this food</param>
		public static void AddFoodToGroup(Identifiable.Id ID, FoodGroup group, ICollection<Identifiable.Id> favoritedBy = null)
		{
			foreach (SlimeDefinition def in GameContext.Instance.SlimeDefinitions.Slimes)
			{
				if (def.Diet == null || !HasFoodGroup(def.Diet, group) || def.Diet.EatMap == null || def.Diet.EatMap.Count <= 0)
					continue;

				if (def.Diet.EatMap.Exists((e) => e.eats == ID))
					continue;

				def.Diet.EatMap.Add(new SlimeDiet.EatMapEntry()
				{
					becomesId = Identifiable.Id.NONE,
					driver = SlimeEmotions.Emotion.HUNGER,
					eats = ID,
					extraDrive = DEFAULT_EXTRA_DRIVE,
					favoriteProductionCount = DEFAULT_FAV_COUNT,
					isFavorite = favoritedBy?.Contains(def.IdentifiableId) ?? false,
					minDrive = EAT_MIN_DRIVE,
					producesId = SlimeToPlort(def.IdentifiableId)
				});
			}
		}

		/// <summary>
		/// Add food to all food groups for all slimes
		/// </summary>
		/// <param name="ID">ID of the food to add</param>
		/// <param name="favoritedBy">The IDs for the slimes that favorite this food</param>
		/// <param name="blackList">Black list of slimes to ignore</param>
		public static void AddFoodToAllGroups(Identifiable.Id ID, ICollection<Identifiable.Id> favoritedBy = null, ICollection<Identifiable.Id> blackList = null)
		{
			foreach (SlimeDefinition def in GameContext.Instance.SlimeDefinitions.Slimes)
			{
				if (def.Diet == null || blackList.Contains(def.IdentifiableId) || def.Diet.EatMap == null || def.Diet.EatMap.Count <= 0)
					continue;

				if (def.Diet.EatMap.Exists((e) => e.eats == ID))
					continue;

				def.Diet.EatMap.Add(new SlimeDiet.EatMapEntry()
				{
					becomesId = Identifiable.Id.NONE,
					driver = SlimeEmotions.Emotion.HUNGER,
					eats = ID,
					extraDrive = DEFAULT_EXTRA_DRIVE,
					favoriteProductionCount = DEFAULT_FAV_COUNT,
					isFavorite = favoritedBy?.Contains(def.IdentifiableId) ?? false,
					minDrive = EAT_MIN_DRIVE,
					producesId = SlimeToPlort(def.IdentifiableId)
				});
			}
		}

		/// <summary>
		/// Does this diet belongs to a given food group?
		/// </summary>
		/// <param name="diet">Diet to check</param>
		/// <param name="group">Group to check</param>
		public static bool HasFoodGroup(SlimeDiet diet, FoodGroup group)
		{
			foreach (FoodGroup food in diet.MajorFoodGroups)
			{
				if (food == group)
					return true;
			}

			return false;
		}

		/// <summary>
		/// Gets all slime diets from a Food Group
		/// </summary>
		/// <param name="group">Group to search for</param>
		public static List<SlimeDiet> GetFoodGroupDiets(FoodGroup group)
		{
			List<SlimeDiet> diets = new List<SlimeDiet>();

			foreach (SlimeDefinition def in GameContext.Instance.SlimeDefinitions.Slimes)
			{
				if (def.Diet == null)
					continue;

				if (HasFoodGroup(def.Diet, group))
					diets.Add(def.Diet);
			}

			return diets;
		}

		/// <summary>
		/// Populates a diet with the given foods
		/// </summary>
		/// <param name="slimeID">Slime ID that owns the diet</param>
		/// <param name="foods">Foods to add</param>
		/// <param name="diet">The diet to populate</param>
		/// <param name="favoriteFoods">A list of favorite foods (null for no favorite foods)</param>
		/// <param name="slime">The created slime if any (null will just search for the plort to produce)</param>
		/// <param name="plortAsFood">Are plorts food for this slime?</param>
		public static void PopulateDiet(Identifiable.Id slimeID, ICollection<Identifiable.Id> foods, SlimeDiet diet, ICollection<Identifiable.Id> favoriteFoods = null, Slime slime = null, bool plortAsFood = false)
		{
			if (diet.EatMap == null)
				diet.EatMap = new List<SlimeDiet.EatMapEntry>();

			foreach (Identifiable.Id food in foods)
			{
				if (diet.EatMap.Exists((e) => e.eats == food))
					continue;

				if (Identifiable.IsPlort(food) && !plortAsFood && PlortToLargo(slime?.Plort ?? SlimeToPlort(slimeID), food) == Identifiable.Id.NONE)
					continue;

				diet.EatMap.Add(new SlimeDiet.EatMapEntry()
				{
					becomesId = (Identifiable.IsPlort(food) && !plortAsFood) 
						? PlortToLargo(slime?.Plort ?? SlimeToPlort(slimeID), food)
						: Identifiable.Id.NONE,

					driver = SlimeEmotions.Emotion.HUNGER,
					eats = food,
					extraDrive = DEFAULT_EXTRA_DRIVE,
					favoriteProductionCount = DEFAULT_FAV_COUNT,
					isFavorite = favoriteFoods?.Contains(food) ?? false,
					minDrive = (Identifiable.IsPlort(food) && !plortAsFood) ? EAT_PLORT_MIN_DRIVE : EAT_MIN_DRIVE,
					producesId = (Identifiable.IsPlort(food) && !plortAsFood) ? Identifiable.Id.NONE 
						: (slime?.Plort ?? SlimeToPlort(slimeID))
				});
			}
		}

		/// <summary>
		/// Gets the Plort for a Slime
		/// </summary>
		/// <param name="slime">ID of the slime</param>
		public static Identifiable.Id SlimeToPlort(Identifiable.Id slime)
		{
			SlimeDefinition def = GameContext.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slime);

			if (def.Diet != null && def.Diet.EatMap != null)
			{
				foreach (SlimeDiet.EatMapEntry entry in def.Diet.EatMap)
				{
					if (Identifiable.IsPlort(entry.producesId))
						return entry.producesId;
				}
			}

			return Identifiable.Id.NONE;
		}

		/// <summary>
		/// Gets a Largo from plorts
		/// </summary>
		/// <param name="plortA">ID of the first plort</param>
		/// <param name="plortB">ID of the second plort</param>
		public static Identifiable.Id PlortToLargo(Identifiable.Id plortA, Identifiable.Id plortB)
		{
			return GameContext.Instance.SlimeDefinitions.GetLargoByPlorts(plortA, plortB)?.IdentifiableId ?? Identifiable.Id.NONE;
		}

		/// <summary>
		/// Gets a Slime Definition by ID (force if needed)
		/// </summary>
		/// <param name="id">ID of the slime</param>
		public static SlimeDefinition GetDefinitionByID(Identifiable.Id ID)
		{
			if (GameContext.Instance != null)
				return GameContext.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(ID);

			foreach (SlimeDefinition def in SRObjects.GetAll<SlimeDefinition>())
			{
				if (def.IdentifiableId == ID)
					return def;
			}

			return null;
		}

		/// <summary>
		/// Generates all the IDs for the largos.
		/// <para>NOTE that all IDs are generated even if they don't get used.</para>
		/// </summary>
		/// <param name="slimeToLargo">ID of the slime to create largos</param>
		public static void GenerateLargoIDs(Identifiable.Id slimeToLargo)
		{
			foreach (Identifiable.Id id in Identifiable.SLIME_CLASS)
			{
				if (GetLargoID(slimeToLargo, id) != Identifiable.Id.NONE)
					continue;

				string name = $"{slimeToLargo.ToString().Replace("_SLIME", "")}_{id.ToString().Replace("_SLIME", "")}_LARGO";

				var newVal = EnumPatcher.GetFirstFreeValue(typeof(Identifiable.Id));
				IdentifiableRegistry.CreateIdentifiableId(newVal, name, false);
			}
		}

		/// <summary>
		/// Creates a new Largo ID or gets the one already created (This can only run on PRE LOAD)
		/// </summary>
		/// <param name="slimeA">ID of base slime A</param>
		/// <param name="slimeB">ID of base slime B</param>
		public static Identifiable.Id GetLargoID(Identifiable.Id slimeA, Identifiable.Id slimeB)
		{
			string name = $"{slimeA.ToString().Replace("_SLIME", "")}_{slimeB.ToString().Replace("_SLIME", "")}_LARGO";
			string name2 = $"{slimeB.ToString().Replace("_SLIME", "")}_{slimeA.ToString().Replace("_SLIME", "")}_LARGO";

			Identifiable.Id newID = Largo(name);

			if (newID != Identifiable.Id.NONE)
				return newID;

			newID = Largo(name2);

			return newID != Identifiable.Id.NONE ? newID : Identifiable.Id.NONE;
		}

		/// <summary>
		/// Gets a Largo from the given slimes
		/// </summary>
		/// <param name="slimeA">ID of slime A</param>
		/// <param name="slimeB">ID of slime B</param>
		public static Identifiable.Id Largo(Identifiable.Id slimeA, Identifiable.Id slimeB)
		{
			SlimeDefinitions defs = GameContext.Instance.SlimeDefinitions;
			return defs.GetLargoByBaseSlimes(defs.GetSlimeByIdentifiableId(slimeA), defs.GetSlimeByIdentifiableId(slimeB))?.IdentifiableId ?? Identifiable.Id.NONE;
		}

		/// <summary>
		/// Gets a Largo from a given ID name
		/// </summary>
		/// <param name="name">ID name to search for</param>
		public static Identifiable.Id Largo(string name)
		{
			return EnumUtils.Parse<Identifiable.Id>(name);
		}

		// TODO: Check this method later
		/*public static Sprite GetLargoIcon(Sprite slimeA, Sprite slimeB)
		{
			Texture2D result = new Texture2D(512, 512);

			Texture2D texA = slimeA.texture;
			Texture2D texB = new Texture2D(512, 512);
			texB.SetPixels(0, 0, 512, 512, slimeB.texture.GetPixels(0, 0, 512, 512));
			texB.Apply();
			texB.Resize(256, 256, TextureFormat.DXT5Crunched, true);
			texB.Apply();

			result.SetPixels(0, 0, 512, 512, texA.GetPixels(0, 0, 512, 512));
			result.SetPixels(255, 255, 512, 512, texB.GetPixels(0, 0, 256, 256));
			result.Apply();

			return Sprite.Create(result, new Rect(Vector2.zero, Vector2.one * 512), Vector2.one * 256, 50, 1, SpriteMeshType.Tight);
		}*/
	}
}
