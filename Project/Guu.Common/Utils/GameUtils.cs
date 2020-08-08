using System.Collections.Generic;
using UnityEngine;

namespace SRML
{
	/// <summary>
	/// An utility class to help with various things in the game
	/// </summary>
	public static class GameUtils
	{
		/// <summary>
		/// Adds a component to any identifiable (or the ones in the white list)
		/// </summary>
		/// <typeparam name="T">Type of component to add</typeparam>
		/// <param name="whiteList">White list to add component to (null will add to all)</param>
		public static void AddComponentToIdentifiable<T>(ICollection<Identifiable.Id> whiteList = null) where T : Component
		{
			foreach (string name in System.Enum.GetValues(typeof(Identifiable.Id)))
			{
				Identifiable.Id ID = EnumUtils.Parse<Identifiable.Id>(name);
				if (whiteList != null && !whiteList.Contains(ID))
					continue;

				GameContext.Instance.LookupDirector.GetPrefab(ID).AddComponent<T>();
			}
		}

		/// <summary>
		/// Adds a component to any land plot (or the ones in the white list)
		/// </summary>
		/// <typeparam name="T">Type of component to add</typeparam>
		/// <param name="whiteList">White list to add component to (null will add to all)</param>
		public static void AddComponentToLand<T>(ICollection<LandPlot.Id> whiteList = null) where T : Component
		{
			foreach (string name in System.Enum.GetValues(typeof(LandPlot.Id)))
			{
				LandPlot.Id ID = EnumUtils.Parse<LandPlot.Id>(name);
				if (whiteList != null && !whiteList.Contains(ID))
					continue;

				GameContext.Instance.LookupDirector.GetPlotPrefab(ID).AddComponent<T>();
			}
		}

		/// <summary>
		/// Adds a component to any spawn resource (or the ones in the white list)
		/// </summary>
		/// <typeparam name="T">Type of component to add</typeparam>
		/// <param name="whiteList">White list to add component to (null will add to all)</param>
		public static void AddComponentToSpawnResource<T>(ICollection<SpawnResource.Id> whiteList = null) where T : Component
		{
			foreach (string name in System.Enum.GetValues(typeof(SpawnResource.Id)))
			{
				SpawnResource.Id ID = EnumUtils.Parse<SpawnResource.Id>(name);
				if (whiteList != null && !whiteList.Contains(ID))
					continue;

				GameContext.Instance.LookupDirector.GetResourcePrefab(ID).AddComponent<T>();
			}
		}

		/// <summary>
		/// Adds a component to any gordo (or the ones in the white list)
		/// </summary>
		/// <typeparam name="T">Type of component to add</typeparam>
		/// <param name="whiteList">White list to add component to (null will add to all)</param>
		public static void AddComponentToGordo<T>(ICollection<Identifiable.Id> whiteList = null) where T : Component
		{
			foreach (string name in System.Enum.GetValues(typeof(Identifiable.Id)))
			{
				Identifiable.Id ID = EnumUtils.Parse<Identifiable.Id>(name);
				if (!Identifiable.GORDO_CLASS.Contains(ID))
					continue;

				if (whiteList != null && !whiteList.Contains(ID))
					continue;

				GameContext.Instance.LookupDirector.GetGordo(ID).AddComponent<T>();
			}
		}

		/// <summary>
		/// Adds a component to any gadget (or the ones in the white list)
		/// </summary>
		/// <typeparam name="T">Type of component to add</typeparam>
		/// <param name="whiteList">White list to add component to (null will add to all)</param>
		public static void AddComponentToGadget<T>(ICollection<Gadget.Id> whiteList = null) where T : Component
		{
			foreach (string name in System.Enum.GetValues(typeof(Gadget.Id)))
			{
				Gadget.Id ID = EnumUtils.Parse<Gadget.Id>(name);
				if (whiteList != null && !whiteList.Contains(ID))
					continue;

				GameContext.Instance.LookupDirector.GetGadgetDefinition(ID).prefab.AddComponent<T>();
			}
		}
	}
}
