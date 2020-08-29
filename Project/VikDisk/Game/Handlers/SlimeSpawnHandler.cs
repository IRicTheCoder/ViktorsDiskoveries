using System;
using System.Collections.Generic;
using System.Linq;
using MonomiPark.SlimeRancher.Regions;
using SRML;

namespace VikDisk.Game
{
    ///<summary>Handles everything related to slime diets for the mod</summary>
    public static class SlimeSpawnHandler
    {
        //+ VARIABLES
        
        // List of mutations to add to the spawn list
        private static readonly Dictionary<Identifiable.Id, Tuple<ZoneDirector.Zone, Identifiable.Id>> MUTATIONS =
            new Dictionary<Identifiable.Id, Tuple<ZoneDirector.Zone, Identifiable.Id>>();

        //+ SPAWN CONTROL
        
        // Fixes the Spawns for the slimes
        internal static void FixSpawns()
        {
            foreach (DirectedActorSpawner spawn in SRObjects.GetAllWorld<DirectedActorSpawner>())
            {
                AddMutations(spawn);
            }
            
            ClearMemory();
        }

        // Injects the mutations into the spanwers
        private static void AddMutations(DirectedActorSpawner spawn)
        {
            ZoneDirector.Zone zone = spawn.GetComponentInParent<Region>().GetZoneId();

            foreach (DirectedActorSpawner.SpawnConstraint constraint in spawn.constraints)
            {
                foreach (Identifiable.Id id in MUTATIONS.Keys)
                {
                    if (MUTATIONS[id].Item1 != ZoneDirector.Zone.NONE && zone != MUTATIONS[id].Item1)
                        continue;

                    SlimeSet.Member slime = constraint.slimeset.members
                                                      .FirstOrDefault(member => member.prefab.GetComponent<Identifiable>().id == id);

                    if (slime == null) continue;

                    List<SlimeSet.Member> members = new List<SlimeSet.Member>(constraint.slimeset.members)
                    {
                        new SlimeSet.Member
                        {
                            prefab = GameContext.Instance.LookupDirector.GetPrefab(MUTATIONS[id].Item2),
                            weight = slime.weight / 3
                        }
                    };

                    constraint.slimeset.members = members.ToArray();
                }
            }
        }
        
        //+ ADDING TO

        /// <summary>
        /// Adds a new mutation to the spawn
        /// </summary>
        /// <param name="slime">The base slime</param>
        /// <param name="mutation">The mutation slime</param>
        /// <param name="zone">The zone restriction or NONE to be everywhere</param>
        public static void AddMutationToSpawn(Identifiable.Id slime, Identifiable.Id mutation, ZoneDirector.Zone zone = ZoneDirector.Zone.NONE)
        {
            if (MUTATIONS.ContainsKey(slime))
                MUTATIONS[slime] = Tuple.Create(zone, mutation);
            else
                MUTATIONS.Add(slime, Tuple.Create(zone, mutation));
        }
        
        //+ FINALIZATION

        // Clears the memory footprint of this class
        private static void ClearMemory()
        {
            MUTATIONS.Clear();
        }
    }
}