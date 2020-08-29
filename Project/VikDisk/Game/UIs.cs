using System.Collections.Generic;
using Guu.API;
using Guu.API.UI;

using VikDisk.Utils;

namespace VikDisk.Game
{
	/// <summary>
	/// Contains all UIs in the Mod
	/// </summary>
	public static class UIs
	{
		// REGISTRY PRIORITY
		private static List<System.Type> priorities = new List<System.Type>()
		{
			typeof(PurchasableUIItem)
		};

		// REGISTRY DICTIONARY
		internal static Dictionary<string, UIItem> Items { get; } = new Dictionary<string, UIItem>();

		// Registers all Land Plots
		internal static void RegisterAll()
		{
			// Register all
			RegistryUtils.RegisterAll<UIItem>(priorities, (item) => Items.Add(item.Name, item.Register()));
		}

		/// <summary>
		/// Gets a land plot item
		/// </summary>
		/// <typeparam name="T">Type of item</typeparam>
		/// <param name="ID">ID of the item</param>
		/// <returns>Item found or null if nothing is found</returns>
		public static T Get<T>(string ID) where T : UIItem
		{
			return !Items.ContainsKey(ID) || !(Items[ID] is T) ? null : Items[ID] as T;
		}
	}
}
