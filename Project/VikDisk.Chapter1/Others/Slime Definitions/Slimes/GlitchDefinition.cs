using Guu.API.Others;
using SRML;
using UnityEngine;

namespace VikDisk.Chapter1
{
	/// <summary>
	/// The slime definition for the Real Glitch Slime
	/// </summary>
	public class GlitchDefinition : SlimeDefinitionItem
	{
		public override string Name { get; } = "RealGlitch";

		protected override SlimeDefinition BaseDef { get; } = GameContext.Instance?.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.GLITCH_SLIME) ?? 
		                                                      SRObjects.Get<SlimeDefinition>("Glitch");

		protected override Identifiable.Id IdentifiableId { get; } = Enums.Identifiables.REAL_GLITCH_SLIME;

		protected override bool CanLargofy => true;
		
		protected override void Build()
		{
			// Pre Build Manipulation
			
			// Build Control
			base.Build();

			// Post Build Manipulation
			Object.Destroy(Definition.SlimeModules[0].GetComponent<GlitchSlimeFlee>());
			Object.Destroy(Definition.SlimeModules[0].GetComponent<GlitchVacuumable>());
		}
	}
}
