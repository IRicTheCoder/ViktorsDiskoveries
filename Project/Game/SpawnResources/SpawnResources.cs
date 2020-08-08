using System.Collections.Generic;
using Guu.API;
using Guu.API.SpawnResources;

using VikDisk.Utils;

namespace VikDisk.Game
{
	/// <summary>
	/// Contains all Spawn Resources in the Mod
	/// </summary>
	public static class SpawnResources
	{
		// REGISTRY PRIORITY
		private static readonly List<System.Type> priorities = new List<System.Type>()
		{
			typeof(PatchGardenResource),
			typeof(PatchWorldResource),
			typeof(TreeGardenResource),
			typeof(TreeWorldResource)
		};

		// REGISTRY DICTIONARY
		internal static Dictionary<SpawnResource.Id, SpawnResourceItem> Items { get; } = new Dictionary<SpawnResource.Id, SpawnResourceItem>();

		// Registers all Spawn Resources
		internal static void RegisterAll()
		{
			// Register all
			RegistryUtils.RegisterAll<SpawnResourceItem>(priorities, (item) => Items.Add(item.ID, item.Register()));
		}

		/// <summary>
		/// Gets a spawn resource item
		/// </summary>
		/// <typeparam name="T">Type of item</typeparam>
		/// <param name="id">ID of the item</param>
		/// <returns>Item found or null if nothing is found</returns>
		public static T Get<T>(SpawnResource.Id id) where T : SpawnResourceItem
		{
			return !Items.ContainsKey(id) || !(Items[id] is T) ? null : Items[id] as T;
		}
	}
}
