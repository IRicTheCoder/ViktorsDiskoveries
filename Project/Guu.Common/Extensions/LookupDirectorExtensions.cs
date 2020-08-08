using System.Collections.Generic;
using UnityEngine;

// TODO: Make the entire extension in this class
// TODO: Revamp old one
/// <summary>
/// Contains extension methods for the LookupDirector
/// </summary>
public static class LookupDirectorExtensions
{
	/// <summary>
	/// Gets all the prefabs from the given IDs
	/// </summary>
	/// <param name="dir">The lookup director</param>
	/// <param name="ids">The IDs</param>
	public static GameObject[] GetPrefabs(this LookupDirector dir, Identifiable.Id[] ids)
	{
		List<GameObject> objs = new List<GameObject>();
		foreach (Identifiable.Id id in ids)
			objs.Add(dir.GetPrefab(id));

		return objs.ToArray();
	}
}