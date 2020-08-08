using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Others;

namespace VikDisk.Game
{
	/// <summary>
	/// The slime definition for the Electric Largo
	/// </summary>
	/*public class ElectricLargoDefinition : SlimeDefinitionItem
	{
		public override string Name { get; } = "defLargoElectric";

		protected override SlimeDefinition BaseDef { get; } = GameContext.Instance?.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.QUANTUM_SLIME) ??
		                                                      SRObjects.Get<SlimeDefinition>("Quantum");

		protected override Identifiable.Id IdentifiableId { get; } = Enums.Identifiables.ELETRIC_LARGO;

		protected override bool CanLargofy => false;

		protected override bool IsLargo => true;

		protected override float PrefabScale => 2f;

		protected override void Build()
		{
			base.Build();

			Object.Destroy(Definition.SlimeModules[0].GetComponent<QuantumVibration>());
			Object.Destroy(Definition.SlimeModules[0].GetComponent<QuantumSlimeSuperposition>());
			Object.Destroy(Definition.SlimeModules[0].GetComponent<GenerateQuantumQubit>());

			GameObject part = Object.Instantiate(Packs.Chapter1.Get<GameObject>("prefabGlitchParticles"), Definition.SlimeModules[0].transform, true);
			part.name = "GlitchPart";
			part.transform.localScale = Vector3.one * 1.5f;

			ParticleSystem.EmissionModule emit = part.GetComponent<ParticleSystem>().emission;
			emit.rateOverTime = emit.rateOverTime.constant * 2;
		}
	}*/
}
