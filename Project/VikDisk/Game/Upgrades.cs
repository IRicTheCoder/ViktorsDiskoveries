using System.Collections.Generic;
using Guu.API;
using Guu.API.Upgrades;

using VikDisk.Utils;

namespace VikDisk.Game
{
	/// <summary>
	/// Contains all upgrade items in the Mod
	/// </summary>
	public static class Upgrades
	{
		// REGISTRY PRIORITY
		private static List<System.Type> priorities = new List<System.Type>()
		{
			typeof(PlayerUpgrade),
			typeof(PlotUpgrade)
		};

		// REGISTRY DICTIONARY
		internal static Dictionary<string, UpgradeItem> Items { get; } = new Dictionary<string, UpgradeItem>();

		// Registers all upgrades
		internal static void RegisterAll()
		{
			// Register all
			RegistryUtils.RegisterAll<UpgradeItem>(priorities, (item) => Items.Add(item.Name, item.Register()));
		}

		/// <summary>
		/// Gets a upgrade item
		/// </summary>
		/// <typeparam name="T">Type of item</typeparam>
		/// <param name="ID">ID of the item</param>
		/// <returns>Item found or null if nothing is found</returns>
		public static T Get<T>(string ID) where T : UpgradeItem
		{
			return !Items.ContainsKey(ID) || !(Items[ID] is T) ? null : Items[ID] as T;
		}
	}
}
