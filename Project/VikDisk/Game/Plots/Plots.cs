using System.Collections.Generic;
using Guu.API;
using Guu.API.Plots;

using VikDisk.Utils;

namespace VikDisk.Game
{
	/// <summary>
	/// Contains all Plots in the Mod
	/// </summary>
	public static class Plots
	{
		// REGISTRY PRIORITY
		private static List<System.Type> priorities = new List<System.Type>()
		{
			typeof(CorralPlot),
			typeof(SiloPlot),
			typeof(CoopPlot),
			typeof(EmptyPlot),
			typeof(IncineratorPlot),
			typeof(PondPlot)
		};

		// REGISTRY DICTIONARY
		internal static Dictionary<LandPlot.Id, PlotItem> Items { get; } = new Dictionary<LandPlot.Id, PlotItem>();

		// Registers all Land Plots
		internal static void RegisterAll()
		{
			// Register all
			//RegistryUtils.RegisterAll<PlotItem>(priorities, (item) => Items.Add(item.ID, item.Register()));
		}

		/// <summary>
		/// Gets a land plot item
		/// </summary>
		/// <typeparam name="T">Type of item</typeparam>
		/// <param name="ID">ID of the item</param>
		/// <returns>Item found or null if nothing is found</returns>
		public static T Get<T>(LandPlot.Id ID) where T : PlotItem
		{
			return !Items.ContainsKey(ID) || !(Items[ID] is T) ? null : Items[ID] as T;
		}
	}
}
