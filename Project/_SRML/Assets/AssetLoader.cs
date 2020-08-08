using System;
using System.IO;
using System.Reflection;
using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML
{
	/// <summary>
	/// Class to help load assets from bundles
	/// </summary>
	public static class AssetLoader
	{
		/// <summary>
		/// Loads a bundle from path
		/// </summary>
		/// <param name="path">Path to the bundle</param>
		public static AssetPack LoadBundle(string path)
		{
			return new AssetPack(path);
		}

		/// <summary>
		/// Loads a bundle from the mod's folder
		/// </summary>
		/// <param name="relPath">Relative path to the bundle</param>
		public static AssetPack LoadModBundle(string relPath)
		{
			Assembly assembly = ReflectionUtils.GetRelevantAssembly();
			string codeBase = assembly.CodeBase;
			UriBuilder uri = new UriBuilder(codeBase);
			string path = Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path)), "Resources/Bundles");

			return new AssetPack(Path.Combine(path, relPath));
		}
	}
}
