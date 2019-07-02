using HarmonyLib;

namespace VikDisk.Patches
{
	// PATCHES THE GARDEN CATCHER TO ALLOW THE PLANTATION OF NEW GROWABLES
	[HarmonyPatch(typeof(GardenCatcher))]
	[HarmonyPatch("Awake")]
	public static class GardenCatcherPatch
	{
		public static void Prefix(GardenCatcher __instance)
		{
			Main.garden.AddGardenGrowables(__instance);
		}
	}
}
