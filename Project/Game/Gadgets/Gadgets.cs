using System.Collections.Generic;
using Guu.API;
using Guu.API.Gadgets;

using VikDisk.Utils;

namespace VikDisk.Game
{
	/// <summary>
	/// Contains all upgrade items in the Mod
	/// </summary>
	public static class Gadgets
	{
		// REGISTRY PRIORITY
		private static List<System.Type> priorities = new List<System.Type>()
		{
			typeof(DecorGadget),
			typeof(FashionPodGadget)
		};

		// REGISTRY DICTIONARY
		internal static Dictionary<string, GadgetItem> Items { get; } = new Dictionary<string, GadgetItem>();

		// Registers all gadgets
		internal static void RegisterAll()
		{
			// Register all
			RegistryUtils.RegisterAll<GadgetItem>(priorities, (item) => Items.Add(item.Name, item.Register()));
		}

		/// <summary>
		/// Gets a gadget item
		/// </summary>
		/// <typeparam name="T">Type of item</typeparam>
		/// <param name="ID">ID of the item</param>
		/// <returns>Item found or null if nothing is found</returns>
		public static T Get<T>(string ID) where T : GadgetItem
		{
			return !Items.ContainsKey(ID) || !(Items[ID] is T) ? null : Items[ID] as T;
		}
	}
}
