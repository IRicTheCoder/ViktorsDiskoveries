using Guu.Utils;
using UnityEngine;

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
	/// <param name="copyProps">Should the properties get deep copied? Some might require a deep copy</param>
	public static Material Copy(this Material mat, bool copyProps = false)
	{
		Material copy = Object.Instantiate(mat);
		
		if (copyProps)
			copy.CopyPropertiesFromMaterial(mat);
		
		return copy;
	}

	/// <summary>
	/// Sets the colors for a material (with shader up to three colors)
	/// </summary>
	/// <param name="mat">The material to change in</param>
	/// <param name="colors">The colors to apply</param>
	public static Material SetTripleColor(this Material mat, Color[] colors)
	{
		if (mat.HasProperty(ShaderProps.BOTTOM_COLOR)) mat.SetColor(ShaderProps.BOTTOM_COLOR, colors[0]);
		if (mat.HasProperty(ShaderProps.MID_COLOR)) mat.SetColor(ShaderProps.MID_COLOR, colors[1]);
		if (mat.HasProperty(ShaderProps.TOP_COLOR)) mat.SetColor(ShaderProps.TOP_COLOR, colors[2]);

		return mat;
	}
	
	/// <summary>
	/// Sets the palette for a material (with shader up to three colors)
	/// </summary>
	/// <param name="mat">The material to change in</param>
	/// <param name="pal">The palette to apply</param>
	public static Material SetTripleColor(this Material mat, SlimeAppearance.Palette pal)
	{
		if (mat.HasProperty(ShaderProps.BOTTOM_COLOR)) mat.SetColor(ShaderProps.BOTTOM_COLOR, pal.Bottom);
		if (mat.HasProperty(ShaderProps.MID_COLOR)) mat.SetColor(ShaderProps.MID_COLOR, pal.Middle);
		if (mat.HasProperty(ShaderProps.TOP_COLOR)) mat.SetColor(ShaderProps.TOP_COLOR, pal.Top);

		return mat;
	}
}