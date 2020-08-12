using UnityEngine;

// TODO: Divide object extensions from srml into classes like this one
/// <summary>
/// Contains extension methods for Materials
/// </summary>
// ReSharper disable once CheckNamespace
public static class MaterialExtensions
{
	/// <summary>
	/// Creates a copy of a material
	/// </summary>
	/// <param name="mat">The material to copy</param>
	public static Material Copy(this Material mat)
	{
		Material copy = Object.Instantiate(mat);
		copy.CopyPropertiesFromMaterial(mat);
		return copy;
	}
}