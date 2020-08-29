using Guu.Utils;
using VikDisk.Core;

namespace VikDisk.Game
{
    internal class Chapter1 : Injector
    {
        internal override void SetupMod()
        {
            //+-------------------------------
            //+ GENERATE LARGO IDs
            //+-------------------------------
            //SlimeUtils.GenerateLargoIDs(Enums.Identifiables.REAL_GLITCH_SLIME);
            SlimeUtils.GenerateLargoIDs(Enums.Identifiables.ALBINO_SLIME);
        }
        
        internal override void PopulateMod()
        {
            //+-------------------------------
            //+ INJECT DIETS
            //+-------------------------------
            // Spicy Diets
            SlimeDietHandler.AddSpicyDiet(Identifiable.Id.CRYSTAL_SLIME, Identifiable.Id.STRANGE_DIAMOND_CRAFT);
            SlimeDietHandler.AddSpicyDiet(Identifiable.Id.ROCK_SLIME, Identifiable.Id.SLIME_FOSSIL_CRAFT);
            SlimeDietHandler.AddSpicyDiet(Identifiable.Id.HONEY_SLIME, Identifiable.Id.HEXACOMB_CRAFT);
            
            // Favorites
            SlimeDietHandler.AddFavoriteExtra(Identifiable.Id.PINK_SLIME, false, 
                                              Identifiable.Id.CARROT_VEGGIE, Identifiable.Id.HEN, 
                                              Identifiable.Id.POGO_FRUIT);
            
            SlimeDietHandler.AddFavoriteExtra(Identifiable.Id.SABER_SLIME, false, 
                                              Identifiable.Id.ELDER_HEN, Identifiable.Id.ELDER_ROOSTER);
            
            // Super Foods
            
            //. SPECIFIC LARGOS
            // Synergies
            SlimeDietHandler.AddSpecificLargo(Identifiable.Id.QUANTUM_SLIME, Enums.Identifiables.REAL_GLITCH_SLIME, 
                                        Enums.Identifiables.ELECTRIC_LARGO_SYNERGY);
            
            //+-------------------------------
            //+ INJECT SPAWNS
            //+-------------------------------
            // Mutations
            SlimeSpawnHandler.AddMutationToSpawn(Identifiable.Id.PINK_SLIME, Enums.Identifiables.ALBINO_SLIME, 
                                                 ZoneDirector.Zone.REEF);
        }
    }
}