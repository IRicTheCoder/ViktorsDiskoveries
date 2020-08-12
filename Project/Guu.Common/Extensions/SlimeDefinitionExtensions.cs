using System.Collections.Generic;

using Guu;
using Guu.Utils;

using UnityEngine;
using SRML;

// TODO: This works ok, but might need more content
/// <summary>
/// Contains extension methods for Slime Definitions
/// </summary>
// ReSharper disable once CheckNamespace
public static class SlimeDefinitionExtensions
{
	/// <summary>
	/// Combines two Slime Definitions to create one for a Largo
	/// <para>The Identifiable ID for the Largo is Auto Generated</para>
	/// </summary>
	/// <param name="our">Our slime definition</param>
	/// <param name="other">The one to make a Largo with</param>
	/// <returns>The definition or null if it couldn't be created</returns>
	public static SlimeDefinition CombineForLargo(this SlimeDefinition our, SlimeDefinition other)
	{
		if (!other.CanLargofy)
			return null;

		SlimeDefinition def = ScriptableObject.CreateInstance<SlimeDefinition>();

		def.name = our.name + other.name;

		def.BaseModule = our.BaseModule;
		def.BaseSlimes = new SlimeDefinition[] { our, other };
		def.CanLargofy = false;

		def.IdentifiableId = SlimeUtils.GetLargoID(our.IdentifiableId, other.IdentifiableId);
		def.IsLargo = true;

		def.Name = "Largo" + our.Name + other.name;
		def.PrefabScale = 2f;

		List<GameObject> modules = new List<GameObject>(our.SlimeModules);
		modules.AddRange(other.SlimeModules);
		def.SlimeModules = modules.ToArray();

		def.AppearancesDefault = our.GetCombinedAppearancesDefault(other.AppearancesDefault);
		def.AppearancesDynamic = our.GetCombinedAppearancesDynamic(other.AppearancesDynamic);
		def.LoadDietFromBaseSlimes();
		def.LoadFavoriteToysFromBaseSlimes();

		def.Sounds = our.Sounds;

		return def;
	}

	/// <summary>
	/// Gets a list of combined default appearances
	/// </summary>
	/// <param name="our">Our slime definition</param>
	/// <param name="other">The default appearances to combine with</param>
	public static SlimeAppearance[] GetCombinedAppearancesDefault(this SlimeDefinition our, SlimeAppearance[] other)
	{
		List<SlimeAppearance> apps = new List<SlimeAppearance>();
		for (int i = 0; i < our.AppearancesDefault.Length; i++)
		{
			if (other.Length >= i)
				break;

			apps.Add(SlimeAppearance.CombineAppearances(our.AppearancesDefault[i], other[i]));
		}

		return apps.ToArray();
	}

	/// <summary>
	/// Gets a list of combined dynamic appearances
	/// </summary>
	/// <param name="our">Our slime definition</param>
	/// <param name="other">The dynamic appearances to combine with</param>
	public static List<SlimeAppearance> GetCombinedAppearancesDynamic(this SlimeDefinition our, List<SlimeAppearance> other)
	{
		List<SlimeAppearance> apps = new List<SlimeAppearance>();
		for (int i = 0; i < our.AppearancesDynamic.Count; i++)
		{
			if (other.Count >= i)
				break;

			apps.Add(SlimeAppearance.CombineAppearances(our.AppearancesDynamic[i], other[i]));
		}

		return apps;
	}
}