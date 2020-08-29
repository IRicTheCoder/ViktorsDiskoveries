using System.Collections.Generic;
using Guu.API.Others;
using Guu.Utils;
using SRML;
using UnityEngine;

namespace VikDisk.Chapter1
{
	/// <summary>
	/// The slime appearance for the Electric Largo (Normal Set)
	/// </summary>
	public class ElectricNormalAppearance : SlimeAppearanceItem
	{
		public override string Name { get; } = "ElectricNormal";

		protected override SlimeAppearance BaseApp => GameContext.Instance?.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.QUANTUM_SLIME).AppearancesDefault[0].Clone() ?? 
		                                              SRObjects.Get<SlimeAppearance>("QuantumNormal");

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconLargoElectric");

		protected override SlimeAppearance.Palette? ColorPalette => Colors.Largos.ELECTRIC.ToPalette();
	}
}
