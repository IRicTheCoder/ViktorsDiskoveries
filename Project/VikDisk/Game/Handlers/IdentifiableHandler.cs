using System.Collections.Generic;

namespace VikDisk.Game
{
    ///<summary>Handles everything related to identifiables for the mod</summary>
    public class IdentifiableHandler
    {
        //+ CONSTANTS
        private const string SYNERGY_SUFFIX = "_LARGO_SYNERGY";
        private const string EXTRACT_SUFFIX = "_EXTRACT";
        private const string EFFY_SUFFIX = "_EFFY";
        private const string CAROL_SUFFIX = "_CAROL";
        
        private const string CRATE_PREFIX = "CRATE_";
        private const string ELDER_PREFIX = "ELDER_";

        //+ CLASS LISTING
        public static HashSet<Identifiable.Id> SYNERGY_CLASS = new HashSet<Identifiable.Id>(Identifiable.idComparer);
        public static HashSet<Identifiable.Id> EXTRACT_CLASS = new HashSet<Identifiable.Id>(Identifiable.idComparer);
        public static HashSet<Identifiable.Id> EFFY_CLASS = new HashSet<Identifiable.Id>(Identifiable.idComparer);
        public static HashSet<Identifiable.Id> CAROL_CLASS = new HashSet<Identifiable.Id>(Identifiable.idComparer);

        //+ SETUP
        /// <summary>
        /// Sets up a new identifiable into it's respective lists
        /// </summary>
        /// <param name="id">The ID to setup</param>
        public static void SetupIdentifiable(Identifiable.Id id)
        {
            string name = id.ToString();
            if (name.StartsWith(CRATE_PREFIX))
            {
                Identifiable.STANDARD_CRATE_CLASS.Add(id);
            }
            else if (name.StartsWith(ELDER_PREFIX))
            {
                Identifiable.ELDER_CLASS.Add(id);
            }
            else if (name.EndsWith(SYNERGY_SUFFIX))
            {
                Identifiable.LARGO_CLASS.Add(id);
                SYNERGY_CLASS.Add(id);
            }
            else if (name.EndsWith(EXTRACT_SUFFIX))
            {
                Identifiable.CRAFT_CLASS.Add(id);
                Identifiable.NON_SLIMES_CLASS.Add(id);
                EXTRACT_CLASS.Add(id);
            }
            else if (name.EndsWith(EFFY_SUFFIX))
            {
                EFFY_CLASS.Add(id);
            }
            else if (name.EndsWith(CAROL_SUFFIX))
            {
                Identifiable.ECHO_NOTE_CLASS.Add(id);
                CAROL_CLASS.Add(id);
            }
            Identifiable.EATERS_CLASS.UnionWith(SYNERGY_CLASS);
        }

        //+ VERIFICATION
        ///<summary>Checks if the ID is a synergy</summary>
        public static bool IsSynergy(Identifiable.Id id)
        {
            return SYNERGY_CLASS.Contains(id);
        }

        ///<summary>Checks if the ID is a crate</summary>
        public static bool IsCrate(Identifiable.Id id)
        {
            return Identifiable.STANDARD_CRATE_CLASS.Contains(id);
        }

        ///<summary>Checks if the ID is an elder</summary>
        public static bool IsElder(Identifiable.Id id)
        {
            return Identifiable.ELDER_CLASS.Contains(id);
        }

        ///<summary>Checks if the ID is an extract</summary>
        public static bool IsExtract(Identifiable.Id id)
        {
            return EXTRACT_CLASS.Contains(id);
        }
        
        ///<summary>Checks if the ID is an effy</summary>
        public static bool IsEffy(Identifiable.Id id)
        {
            return EFFY_CLASS.Contains(id);
        }
        
        ///<summary>Checks if the ID is a carol</summary>
        public static bool IsCarol(Identifiable.Id id)
        {
            return CAROL_CLASS.Contains(id);
        }
    }
}