using Guu.API.Others;
using SRML;
using UnityEngine;
using VikDisk.Game;

namespace VikDisk.Chapter1
{
	/// <summary>
	/// The slime definition for the Electric Largo
	/// </summary>
	public class ElectricLargoDefinition : SlimeDefinitionItem
	{
		public override string Name { get; } = "ElectricLargo";

		protected override SlimeDefinition BaseDef { get; } = GameContext.Instance?.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.QUANTUM_SLIME) ??
		                                                      SRObjects.Get<SlimeDefinition>("Quantum");

		protected override Identifiable.Id IdentifiableId { get; } = Enums.Identifiables.ELECTRIC_LARGO_SYNERGY;

		protected override bool CanLargofy => false;

		// This is needed to prevent registration issues
		protected override bool IsLargo => false;

		protected override float PrefabScale => 2f;

		protected override SlimeAppearance[] AppearancesDefault => new []
			{ Others.Get<SlimeAppearanceItem>("ElectricNormal").Appearance };

		protected override void Build()
		{
			// Pre Build Manipulation
			
			// Build Control
			base.Build();

			// Post Build Manipulation
			Object.Destroy(Definition.SlimeModules[0].GetComponent<QuantumVibration>());
			Object.Destroy(Definition.SlimeModules[0].GetComponent<QuantumSlimeSuperposition>());
			Object.Destroy(Definition.SlimeModules[0].GetComponent<GenerateQuantumQubit>());
		}
	}
}
