using Guu.Utils;
using UnityEngine;

/// <summary>
/// Contains extension methods for Colors
/// </summary>
// ReSharper disable once CheckNamespace
public static class ColorExtensions
{
	/// <summary>
	/// Converts a color array to a Color Palette
	/// </summary>
	/// <param name="color">The color array to convert</param>
	public static SlimeAppearance.Palette ToPalette(this Color[] color)
	{
		return new SlimeAppearance.Palette()
		{
			Ammo = color[3],
			Bottom = color[0],
			Middle = color[1],
			Top = color[2]
		};
	}
}