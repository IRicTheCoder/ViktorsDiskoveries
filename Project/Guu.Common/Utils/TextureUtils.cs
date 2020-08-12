using System.Collections.Generic;

using UnityEngine;

namespace Guu.Utils
{
	/// <summary>
	/// An utility class to help with Textures
	/// </summary>
	public static class TextureUtils
	{
		/// <summary>
		/// Creates a Ramp Gradient from A to B
		/// </summary>
		/// <param name="a">Color A</param>
		/// <param name="b">Color B</param>
		/// <returns>The Texture2D with the Ramp Gradient</returns>
		public static Texture2D CreateRamp(Color a, Color b)
		{
			Texture2D ramp = new Texture2D(128, 32);

			for (int x = 0; x < 128; x++)
			{
				Color curr = Color.Lerp(a, b, x / 127f);
				for (int y = 0; y < 32; y++)
					ramp.SetPixel(x, y, curr);
			}

			ramp.name = $"generatedTexture-{ramp.GetInstanceID()}";
			ramp.Apply();
			return ramp;
		}

		/// <summary>
		/// Creates a Ramp Gradient from all given colors
		/// </summary>
		/// <param name="a">Color A</param>
		/// <param name="b">Color B</param>
		/// <param name="others">All other colors</param>
		/// <returns>The Texture2D with the Ramp Gradient</returns>
		public static Texture2D CreateRamp(Color a, Color b, params Color[] others)
		{
			Texture2D ramp = new Texture2D(128, 32);

			List<Color> colors = new List<Color>() { a, b };
			colors.AddRange(others);

			int stage = Mathf.RoundToInt(128f / (colors.Count - 1));

			for (int x = 0; x < 128; x++)
			{
				Color curr = Color.Lerp(colors[0], colors[1], (x % stage) / (stage - 1));

				if ((x % stage) == stage - 1)
					colors.RemoveAt(0);

				for (int y = 0; y < 32; y++)
					ramp.SetPixel(x, y, curr);
			}

			ramp.name = $"generatedTexture-{ramp.GetInstanceID()}";
			ramp.Apply();
			return ramp;
		}

		/// <summary>
		/// Creates a Ramp Gradient from A to B
		/// </summary>
		/// <param name="hexA">Hexadecimal for Color A (without the #)</param>
		/// <param name="hexB">Hexadecimal for Color B (without the #)</param>
		/// <returns>The Texture2D with the Ramp Gradient</returns>
		public static Texture2D CreateRamp(string hexA, string hexB)
		{

			ColorUtility.TryParseHtmlString("#" + hexA.ToUpper(), out Color a);
			ColorUtility.TryParseHtmlString("#" + hexB.ToUpper(), out Color b);

			return CreateRamp(a, b);
		}

		/// <summary>
		/// Creates a Ramp Gradient from all given colors
		/// </summary>
		/// <param name="hexA">Hexadecimal for Color A (without the #)</param>
		/// <param name="hexB">Hexadecimal for Color B (without the #)</param>
		/// <param name="hexs">Hexadecimal for all other colors (without the #)</param>
		/// <returns>The Texture2D with the Ramp Gradient</returns>
		public static Texture2D CreateRamp(string hexA, string hexB, params string[] hexs)
		{
			ColorUtility.TryParseHtmlString("#" + hexA.ToUpper(), out Color a);
			ColorUtility.TryParseHtmlString("#" + hexB.ToUpper(), out Color b);
			List<Color> colors = new List<Color>();
			foreach (string hex in hexs)
			{
				ColorUtility.TryParseHtmlString("#" + hex.ToUpper(), out Color c);
				colors.Add(c);
			}

			return CreateRamp(a, b, colors.ToArray());
		}
	}
}
