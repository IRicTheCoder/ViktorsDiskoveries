using System;
using System.Collections.Generic;
using SRML.Utils;
using UnityEngine;

namespace VikDisk.Game
{
	/// <summary>
	/// Contains fixes for some garden resources
	/// </summary>
	public static class GardenResourceFixes
	{
		// Contains all the fixes
		private static readonly Dictionary<Identifiable.Id, GameObject> fixes = new Dictionary<Identifiable.Id, GameObject>();

		/// <summary>
		/// Gets a fixed prefab for an ID
		/// </summary>
		/// <param name="id">ID to search for</param>
		/// <param name="fix">Fix to apply if not present</param>
		public static GameObject GetFixedPrefab(Identifiable.Id id, Action<GameObject> fix)
		{
			if (fixes.ContainsKey(id))
				return fixes[id];

			GameObject obj = PrefabUtils.CopyPrefab(GameContext.Instance.LookupDirector.GetPrefab(id));
			fix.Invoke(obj);

			fixes.Add(id, obj);

			return fixes[id];
		}
	}
}
