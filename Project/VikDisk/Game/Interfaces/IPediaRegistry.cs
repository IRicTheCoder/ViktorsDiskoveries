using SRML.SR;

namespace VikDisk.Game
{
	/// <summary>
	/// Used to tag the registry items to also register to
	/// the slime pedia
	/// </summary>
	public interface IPediaRegistry
	{
		/// <summary>Pedia ID to register</summary>
		PediaDirector.Id PediaID { get; }

		/// <summary>Pedia Category to register</summary>
		PediaRegistry.PediaCategory PediaCat { get; }
	}
}
