using System.Collections.Generic;
using Guu.API.Others;
using Guu.Utils;
using SRML;
using UnityEngine;

namespace VikDisk.Chapter1
{
	/// <summary>
	/// The slime appearance for the Albino Slime (Normal Set)
	/// </summary>
	public class AlbinoNormalAppearance : SlimeAppearanceItem
	{
		public override string Name { get; } = "AlbinoNormal";

		protected override SlimeAppearance BaseApp => GameContext.Instance?.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.PINK_SLIME).AppearancesDefault[0].Clone() ?? 
		                                              SRObjects.Get<SlimeAppearance>("PinkNormal");

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconSlimeAlbino");

		protected override SlimeAppearance.Palette? ColorPalette => Colors.Slimes.ALBINO.ToPalette();
	}
}
