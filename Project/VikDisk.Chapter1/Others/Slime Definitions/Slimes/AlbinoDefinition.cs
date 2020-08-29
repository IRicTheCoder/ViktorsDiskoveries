using Guu.API.Others;
using SRML;
using UnityEngine;
using VikDisk.Game;

namespace VikDisk.Chapter1
{
	/// <summary>
	/// The slime definition for the Albino Slime
	/// </summary>
	public class AlbinoDefinition : SlimeDefinitionItem
	{
		public override string Name { get; } = "Albino";

		protected override SlimeDefinition BaseDef { get; } = GameContext.Instance?.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.PINK_SLIME) ?? 
		                                                      SRObjects.Get<SlimeDefinition>("Pink");

		protected override Identifiable.Id IdentifiableId { get; } = Enums.Identifiables.ALBINO_SLIME;

		protected override bool CanLargofy => true;

		protected override SlimeAppearance[] AppearancesDefault => new[]
			{ Others.Get<SlimeAppearanceItem>("AlbinoNormal").Appearance };

		protected override void Build()
		{
			// Build Control
			base.Build();

			// Post Build Manipulation
		}
	}
}
