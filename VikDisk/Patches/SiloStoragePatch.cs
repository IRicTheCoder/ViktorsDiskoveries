using HarmonyLib;

namespace VikDisk.Patches
{
	// PATCHES THE SILO STORAGE TO ALLOW PLORT COLLECTORS TO ACCEPT CRAFT RESOURCES TOO
	[HarmonyPatch(typeof(SiloStorage))]
	[HarmonyPatch("Awake")]
	public static class SiloStoragePatch
	{
		public static void Postfix(SiloStorage __instance)
		{
			if (__instance.type == SiloStorage.StorageType.PLORT)
			{
				foreach (Identifiable.Id id in Identifiable.CRAFT_CLASS)
				{
					__instance.LocalAmmo.RegisterPotentialAmmo(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(id));
				}
			}
		}
	}
}
