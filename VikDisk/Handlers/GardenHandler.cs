using HarmonyLib;

namespace VikDisk.Handlers
{
	/// <summary>
	/// Handles everything related to the gardens
	/// </summary>
	public class GardenHandler : Handler<GardenHandler>
	{
		/// <summary>
		/// Setups the handler
		/// </summary>
		public override void Setup()
		{
			// NO SETUP REQUIRED FOR NOW
		}

		// ADDS GROWABLES TO THE GARDEN
		public void AddGardenGrowables(GardenCatcher catcher)
		{
			/*// Gets plantable dictionary
			FieldInfo plantDict = typeof(GardenCatcher).GetField("plantableDict", BindingFlags.NonPublic | BindingFlags.Instance);
			Dictionary<Identifiable.Id, GameObject> plantableDict = plantDict.GetValue(catcher) as Dictionary<Identifiable.Id, GameObject>;

			// Gets deluxe dictionary
			FieldInfo plantDexDict = typeof(GardenCatcher).GetField("deluxeDict", BindingFlags.NonPublic | BindingFlags.Instance);
			Dictionary<Identifiable.Id, GameObject> deluxeDict = plantDexDict.GetValue(catcher) as Dictionary<Identifiable.Id, GameObject>;*/

			// Adds new growables
			foreach (Identifiable.Id id in Configs.Food.growables.Keys)
			{
				catcher.plantable.AddItem(new GardenCatcher.PlantSlot
				{
					id = id,
					plantedPrefab = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(id),
					deluxePlantedPrefab = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(id)
				});
			}
		}
	}
}
