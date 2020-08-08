using System.Collections.Generic;
using Guu.API;
using Guu.API.Others;

using VikDisk.Utils;

namespace VikDisk.Game
{
	/// <summary>
	/// Contains all other items in the Mod
	/// </summary>
	public static class Others
	{
		// REGISTRY PRIORITY
		private static List<System.Type> priorities = new List<System.Type>()
		{
			typeof(SlimeDefinitionItem)
		};

		// REGISTRY DICTIONARY
		internal static Dictionary<string, OtherItem> Items { get; } = new Dictionary<string, OtherItem>();

		// Registers all Identifiables
		internal static void RegisterAll()
		{
			// Register all
			RegistryUtils.RegisterAll<OtherItem>(priorities, (item) => Items.Add(item.Name, item.Register()));
		}

		/// <summary>
		/// Gets a definition item
		/// </summary>
		/// <typeparam name="T">Type of item</typeparam>
		/// <param name="ID">ID of the item</param>
		/// <returns>Item found or null if nothing is found</returns>
		public static T Get<T>(string ID) where T : OtherItem
		{
			return !Items.ContainsKey(ID) || !(Items[ID] is T) ? null : Items[ID] as T;
		}
	}
}
