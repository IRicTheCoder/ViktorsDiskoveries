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
		private static readonly List<System.Type> PRIORITIES = new List<System.Type>()
		{
			typeof(SlimeAppearanceItem),
			typeof(SlimeDefinitionItem)
		};

		// REGISTRY DICTIONARY
		internal static Dictionary<string, OtherItem> Items { get; } = new Dictionary<string, OtherItem>();

		// Registers all Identifiables
		internal static void RegisterAll()
		{
			// Register all
			RegistryUtils.RegisterAll<OtherItem>(PRIORITIES, (item) => Items.Add(item.Name, item.Register()));
		}

		/// <summary>
		/// Gets a definition item
		/// </summary>
		/// <typeparam name="T">Type of item</typeparam>
		/// <param name="id">ID of the item</param>
		/// <returns>Item found or null if nothing is found</returns>
		public static T Get<T>(string id) where T : OtherItem
		{
			return !Items.ContainsKey(id) || !(Items[id] is T) ? null : Items[id] as T;
		}
	}
}
