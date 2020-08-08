namespace VikDisk
{
	/// <summary>
	/// Contains all QOL features in the mod
	/// </summary>
	public static class QOL
	{
		//====================
		// GAME IMPROVEMENTS
		//====================

		// Fixes the scales of certain objects to be similar to others
		public static bool FixScales { get; internal set; } = true;

		// Improves the collisions of certain objects
		public static bool ImpCollisions { get; internal set; } = true;

		// TODO: Implement this
		// If a key is pressed doubles the speed of shoots and vaccing for the vac pack
		public static bool SpeedUpVac { get; internal set; } = true;
	}
}
