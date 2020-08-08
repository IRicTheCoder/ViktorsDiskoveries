using Guu;

using SRML;

namespace VikDisk
{
	/// <summary>
	/// Contains all asset packs in the mod
	/// </summary>
	public static class Packs
	{
		/// <summary>The asset pack for Chapter 1</summary>
		public static AssetPack Chapter1 { get; private set; }

		// Creates all packs
		internal static void Setup()
		{
			Chapter1 = AssetLoader.LoadModBundle("Chapter1");
		}
	}
}
