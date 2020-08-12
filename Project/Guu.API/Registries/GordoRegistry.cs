using System.Collections.Generic;

using Guu.Utils;

using UnityEngine;

namespace SRML.Registries
{
	// TODO: Turn this into an utility class
	/// <summary>
	/// The registry for gordos
	/// </summary>
	public static class GordoRegistry
	{
		// Contains all the icons for the Gordos
		private static readonly Dictionary<Identifiable.Id, Sprite> gordoIcons = new Dictionary<Identifiable.Id, Sprite>();

		// Sets up this registry
		internal static void Setup()
		{
			// Normal Gordos
			gordoIcons.Add(Identifiable.Id.BOOM_GORDO, SRObjects.Get<Sprite>("iconGordoBoom"));
			gordoIcons.Add(Identifiable.Id.CRYSTAL_GORDO, SRObjects.Get<Sprite>("iconGordoCrystal"));
			gordoIcons.Add(Identifiable.Id.DERVISH_GORDO, SRObjects.Get<Sprite>("iconGordoDervish"));
			gordoIcons.Add(Identifiable.Id.GOLD_GORDO, SRObjects.Get<Sprite>("iconGordoGold"));
			gordoIcons.Add(Identifiable.Id.HONEY_GORDO, SRObjects.Get<Sprite>("iconGordoHoney"));
			gordoIcons.Add(Identifiable.Id.HUNTER_GORDO, SRObjects.Get<Sprite>("iconGordoHunter"));
			gordoIcons.Add(Identifiable.Id.MOSAIC_GORDO, SRObjects.Get<Sprite>("iconGordoMosaic"));
			gordoIcons.Add(Identifiable.Id.PARTY_GORDO, SRObjects.Get<Sprite>("iconSlimePartyGordo"));
			gordoIcons.Add(Identifiable.Id.PHOSPHOR_GORDO, SRObjects.Get<Sprite>("iconGordoPhosphor"));
			gordoIcons.Add(Identifiable.Id.PINK_GORDO, SRObjects.Get<Sprite>("iconGordoPink"));
			gordoIcons.Add(Identifiable.Id.QUANTUM_GORDO, SRObjects.Get<Sprite>("iconGordoQuantum"));
			gordoIcons.Add(Identifiable.Id.RAD_GORDO, SRObjects.Get<Sprite>("iconGordoRad"));
			gordoIcons.Add(Identifiable.Id.ROCK_GORDO, SRObjects.Get<Sprite>("iconGordoRock"));
			gordoIcons.Add(Identifiable.Id.TABBY_GORDO, SRObjects.Get<Sprite>("iconGordoTabby"));
			gordoIcons.Add(Identifiable.Id.TANGLE_GORDO, SRObjects.Get<Sprite>("iconGordoTangle"));

			// Special Cases
			gordoIcons.Add(Identifiable.Id.PUDDLE_GORDO, SRObjects.Get<Sprite>("iconSlimePuddle"));
		}

		/// <summary>
		/// Gets the icon from a Gordo
		/// </summary>
		/// <param name="gordoID">Gordo Slime ID</param>
		/// <returns>The sprite found or null it nothing is found</returns>
		public static Sprite GetGordoIcon(Identifiable.Id gordoID)
		{
			return gordoIcons.ContainsKey(gordoID)
				? gordoIcons[gordoID]
				: GameContext.Instance.LookupDirector.GetIcon(GetSlimeFromGordo(gordoID));
		}

		/// <summary>
		/// Converts a Gordo Slime ID to its base Slime ID
		/// </summary>
		/// <param name="gordoID">Gordo Slime ID</param>
		/// <returns>The base slime ID</returns>
		public static Identifiable.Id GetSlimeFromGordo(Identifiable.Id gordoID)
		{
			string name = gordoID.ToString().Replace("_GORDO", "_SLIME");
			return EnumUtils.Parse<Identifiable.Id>(name);
		}
	}
}
