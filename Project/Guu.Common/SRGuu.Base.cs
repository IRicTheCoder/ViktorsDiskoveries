using System.Reflection;
using Guu.Core;

using HarmonyLib;

using UnityEngine;

namespace Guu
{
    public partial class SRGuu
    {
        // Is the services initialized
        private static bool isInitialized = false;

        // The assembly from Guu
        internal static Assembly assembly;

        // The harmony instance for Guu
        internal static Harmony harmony;

        // The object that controls the services update
        internal static GameObject serviceObj;

        /// <summary>
        /// Enables the Guu services for your mod
        /// </summary>
        public static void Init()
        {
            if (isInitialized)
                return;

            // Creates an Harmony instance and patches the game
            harmony = new Harmony("Guu");
            
            assembly = Assembly.GetAssembly(typeof(SRGuu));
            harmony.PatchAll(assembly);
            
            isInitialized = true;
        }
    }
}