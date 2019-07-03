using HarmonyLib;
using VikDisk.Handlers;

namespace VikDisk.Patches
{
	// PATCHES THE GARDEN CATCHER TO ALLOW THE PLANTATION OF NEW GROWABLES
	[HarmonyPatch(typeof(GardenCatcher))]
	[HarmonyPatch("Awake")]
	public static class GardenCatcherPatch
	{
		public static void Prefix(GardenCatcher __instance)
		{
			GardenHandler.Instance.AddGardenGrowables(__instance);
		}
	}
}
