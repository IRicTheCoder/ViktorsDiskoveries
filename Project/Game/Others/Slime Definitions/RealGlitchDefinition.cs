using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Others;

namespace VikDisk.Game
{
	/// <summary>
	/// The slime definition for the Real Glitch Slime
	/// </summary>
	public class RealGlitchDefinition : SlimeDefinitionItem
	{
		public override string Name { get; } = "defSlimeRealGlitch";

		protected override SlimeDefinition BaseDef { get; } = GameContext.Instance?.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.GLITCH_SLIME) ?? 
		                                                      SRObjects.Get<SlimeDefinition>("Glitch");

		protected override Identifiable.Id IdentifiableId { get; } = Enums.Identifiables.REAL_GLITCH_SLIME;

		protected override bool CanLargofy => true;

		protected override void Build()
		{
			base.Build();

			Object.Destroy(Definition.SlimeModules[0].GetComponent<GlitchSlimeFlee>());
			Object.Destroy(Definition.SlimeModules[0].GetComponent<GlitchVacuumable>());

			GameObject part = Object.Instantiate(Packs.Chapter1.Get<GameObject>("prefabGlitchParticles"), Definition.SlimeModules[0].transform, true);
			part.name = "GlitchPart";
			part.transform.localScale = Vector3.one * 1.5f;

			ParticleSystem.EmissionModule emit = part.GetComponent<ParticleSystem>().emission;
			emit.rateOverTime = emit.rateOverTime.constant * 2;
		}
	}
}
