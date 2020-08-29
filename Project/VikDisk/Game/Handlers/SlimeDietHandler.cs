using System;
using System.Collections.Generic;
using System.Linq;
using Guu.Utils;
using VikDisk.Game.Overrides;

namespace VikDisk.Game
{
    ///<summary>Handles everything related to slime diets for the mod</summary>
    public static class SlimeDietHandler
    {
        //+ CONSTANTS
        /// <summary>A constant that represents the default super food production count</summary>
        public const int DEFAULT_SUPER_COUNT = 3;
        
        //+ VARIABLES
        
        // List of Slimes affected by extra spicy tofu
        private static readonly Dictionary<Identifiable.Id, Identifiable.Id> SPICY_DIETS =
            new Dictionary<Identifiable.Id, Identifiable.Id>();
        
        // List of favorite diets to fix/add to original slimes
        private static readonly Dictionary<Identifiable.Id, Identifiable.Id[]> FAVORITES = 
            new Dictionary<Identifiable.Id, Identifiable.Id[]>();

        // List of Super Foods for each slime
        private static readonly Dictionary<Identifiable.Id, Identifiable.Id[]> SUPER_FOODS =
            new Dictionary<Identifiable.Id, Identifiable.Id[]>();

        // List of all synergies & specific largos
        private static readonly Dictionary<Identifiable.Id, Tuple<Identifiable.Id, Identifiable.Id>> SPEC_LARGOS =
            new Dictionary<Identifiable.Id, Tuple<Identifiable.Id, Identifiable.Id>>();

        //+ DIET CONTROL
        
        // Fixes the Diets for the slimes
        internal static void FixDiets()
        {
            foreach (SlimeDefinition def in GameContext.Instance.SlimeDefinitions.Slimes)
            {
                if (Identifiable.IsLargo(def.IdentifiableId) && !IdentifiableHandler.IsSynergy(def.IdentifiableId))
                {
                    InjectKookadobaDiet(def.BaseSlimes[0].IdentifiableId, def.Diet);
                    InjectKookadobaDiet(def.BaseSlimes[1].IdentifiableId, def.Diet);
                    
                    InjectSpicyDiet(def.BaseSlimes[0].IdentifiableId, def.Diet);
                    InjectSpicyDiet(def.BaseSlimes[1].IdentifiableId, def.Diet);
                    
                    InjectFavoriteDiet(def.BaseSlimes[0].IdentifiableId, def.Diet, def.BaseSlimes[1].IdentifiableId);
                    InjectFavoriteDiet(def.BaseSlimes[1].IdentifiableId, def.Diet, def.BaseSlimes[0].IdentifiableId);
                    
                    InjectSuperDiet(def.BaseSlimes[0].IdentifiableId, def.Diet, def.BaseSlimes[1].IdentifiableId);
                    InjectSuperDiet(def.BaseSlimes[1].IdentifiableId, def.Diet, def.BaseSlimes[0].IdentifiableId);
                }
                else
                {
                    InjectKookadobaDiet(def.IdentifiableId, def.Diet);
                    InjectSpicyDiet(def.IdentifiableId, def.Diet);
                    InjectFavoriteDiet(def.IdentifiableId, def.Diet);
                    InjectSuperDiet(def.IdentifiableId, def.Diet);

                    InjectSpecLargoDiet(def.IdentifiableId, def.Diet);
                }
            }

            ClearMemory();
        }

        // Injects the Kookadoba Diets into the slimes
        private static void InjectKookadobaDiet(Identifiable.Id slime, SlimeDiet diet)
        {
            
        }

        // Injects any extra spicy tofu diet it might find
        private static void InjectSpicyDiet(Identifiable.Id slime, SlimeDiet diet)
        {
            if (!SPICY_DIETS.ContainsKey(slime)) return;
            
            diet.EatMap.Add(new AdvEatMapEntry()
            {
                driver = SlimeEmotions.Emotion.NONE,
                producesId = SPICY_DIETS[slime],
                eats = Enums.Identifiables.EXTRA_SPICY_TOFU,
                isRange = true,
                minCount = 1,
                maxCount = 2
            });
        }

        // Injects the favorites into the slime diet or adapts if present
        private static void InjectFavoriteDiet(Identifiable.Id slime, SlimeDiet diet, Identifiable.Id slimeB = Identifiable.Id.NONE)
        {
            if (!FAVORITES.ContainsKey(slime)) return;

            foreach (Identifiable.Id fav in FAVORITES[slime])
            {
                bool found = false;
                foreach (SlimeDiet.EatMapEntry entry in diet.EatMap.Where(entry => entry.eats == fav))
                {
                    entry.isFavorite = true;
                    entry.favoriteProductionCount = SlimeUtils.DEFAULT_FAV_COUNT;
                    
                    found = true;
                }

                if (found) continue;
                
                diet.EatMap.Add(new SlimeDiet.EatMapEntry
                {
                    eats = fav,
                    isFavorite = true,
                    favoriteProductionCount = SlimeUtils.DEFAULT_FAV_COUNT,
                    driver = SlimeEmotions.Emotion.HUNGER,
                    minDrive = SlimeUtils.EAT_MIN_DRIVE,
                    producesId = SlimeUtils.SlimeToPlort(slime)
                });

                if (slimeB != Identifiable.Id.NONE)
                {
                    diet.EatMap.Add(new SlimeDiet.EatMapEntry
                    {
                        eats = fav,
                        isFavorite = true,
                        favoriteProductionCount = SlimeUtils.DEFAULT_FAV_COUNT,
                        driver = SlimeEmotions.Emotion.HUNGER,
                        minDrive = SlimeUtils.EAT_MIN_DRIVE,
                        producesId = SlimeUtils.SlimeToPlort(slimeB)
                    });
                }
            }
        }
        
        // Injects the super foods into the slime diet or adapts if present
        private static void InjectSuperDiet(Identifiable.Id slime, SlimeDiet diet, Identifiable.Id slimeB = Identifiable.Id.NONE)
        {
            if (!SUPER_FOODS.ContainsKey(slime)) return;

            foreach (Identifiable.Id fav in SUPER_FOODS[slime])
            {
                bool found = false;
                foreach (SlimeDiet.EatMapEntry entry in diet.EatMap.Where(entry => entry.eats == fav))
                {
                    entry.isFavorite = true;
                    entry.favoriteProductionCount = DEFAULT_SUPER_COUNT;
                    
                    found = true;
                }

                if (found) continue;
                
                diet.EatMap.Add(new SlimeDiet.EatMapEntry
                {
                    eats = fav,
                    isFavorite = true,
                    favoriteProductionCount = DEFAULT_SUPER_COUNT,
                    driver = SlimeEmotions.Emotion.HUNGER,
                    minDrive = SlimeUtils.EAT_MIN_DRIVE,
                    producesId = SlimeUtils.SlimeToPlort(slime)
                });

                if (slimeB != Identifiable.Id.NONE)
                {
                    diet.EatMap.Add(new SlimeDiet.EatMapEntry
                    {
                        eats = fav,
                        isFavorite = true,
                        favoriteProductionCount = DEFAULT_SUPER_COUNT,
                        driver = SlimeEmotions.Emotion.HUNGER,
                        minDrive = SlimeUtils.EAT_MIN_DRIVE,
                        producesId = SlimeUtils.SlimeToPlort(slimeB)
                    });
                }
            }
        }
        
        // Injects the specific largos into the slime diet or adapts if present
        private static void InjectSpecLargoDiet(Identifiable.Id slime, SlimeDiet diet)
        {
            if (!SPEC_LARGOS.ContainsKey(slime)) return;

            Identifiable.Id plort = SPEC_LARGOS[slime].Item1;
            Identifiable.Id result = SPEC_LARGOS[slime].Item2;
            
            bool found = false;
            foreach (SlimeDiet.EatMapEntry entry in diet.EatMap.Where(entry => entry.eats == plort))
            {
                entry.becomesId = result;
                
                found = true;
                break;
            }

            if (!found)
            {
                diet.EatMap.Add(new SlimeDiet.EatMapEntry
                {
                    eats = plort,
                    becomesId = result
                });
            }
        }

        //+ ADDING TO
        
        /// <summary>
        /// Adds a new spicy diet (a slime can only give one result)
        /// </summary>
        /// <param name="slime">The slime to add to</param>
        /// <param name="result">The result</param>
        public static void AddSpicyDiet(Identifiable.Id slime, Identifiable.Id result)
        {
            if (SPICY_DIETS.ContainsKey(slime))
                SPICY_DIETS[slime] = result;
            else
                SPICY_DIETS.Add(slime, result);
        }

        /// <summary>
        /// Adds a favorite extra to a slime
        /// </summary>
        /// <param name="slime">The slime to add to</param>
        /// <param name="isOverride">Should override the current extra list?</param>
        /// <param name="favorites">The favorites to add</param>
        public static void AddFavoriteExtra(Identifiable.Id slime, bool isOverride, params Identifiable.Id[] favorites)
        {
            if (FAVORITES.ContainsKey(slime))
            {
                if (isOverride) 
                    FAVORITES[slime] = favorites;
                else
                {
                    List<Identifiable.Id> favs = new List<Identifiable.Id>(FAVORITES[slime]);
                    favs.AddRange(favorites);
                    FAVORITES[slime] = favs.ToArray();
                }
            }
            else
            {
                FAVORITES.Add(slime, favorites);
            }
        }

        /// <summary>
        /// Adds a super food to a slime
        /// </summary>
        /// <param name="slime">The slime to add to</param>
        /// <param name="isOverride">Should override the current list?</param>
        /// <param name="supers">The supers to add</param>
        public static void AddSuperFood(Identifiable.Id slime, bool isOverride, params Identifiable.Id[] supers)
        {
            if (SUPER_FOODS.ContainsKey(slime))
            {
                if (isOverride) 
                    SUPER_FOODS[slime] = supers;
                else
                {
                    List<Identifiable.Id> favs = new List<Identifiable.Id>(SUPER_FOODS[slime]);
                    favs.AddRange(supers);
                    SUPER_FOODS[slime] = favs.ToArray();
                }
            }
            else
            {
                SUPER_FOODS.Add(slime, supers);
            }
        }
        
        /// <summary>
        /// Adds a specific largo to a slime
        /// </summary>
        /// <param name="slimeA">The slime A to add to</param>
        /// <param name="slimeB">The slime B to add to</param>
        /// <param name="result">The result to generate</param>
        public static void AddSpecificLargo(Identifiable.Id slimeA, Identifiable.Id slimeB, Identifiable.Id result)
        {
            Tuple<Identifiable.Id, Identifiable.Id> mixA = Tuple.Create(SlimeUtils.SlimeToPlort(slimeB), result);
            Tuple<Identifiable.Id, Identifiable.Id> mixB = Tuple.Create(SlimeUtils.SlimeToPlort(slimeA), result);
            
            if (SPEC_LARGOS.ContainsKey(slimeA))
                SPEC_LARGOS[slimeA] = mixA;
            else
                SPEC_LARGOS.Add(slimeA, mixA);
            
            if (SPEC_LARGOS.ContainsKey(slimeB))
                SPEC_LARGOS[slimeB] = mixB;
            else
                SPEC_LARGOS.Add(slimeB, mixB);
        }
        
        //+ FINALIZATION

        // Clears the memory footprint of this class
        private static void ClearMemory()
        {
            SPICY_DIETS.Clear();
            FAVORITES.Clear();
            SUPER_FOODS.Clear();
            SPEC_LARGOS.Clear();
        }
    }
}