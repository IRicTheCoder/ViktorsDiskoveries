using System.Collections.Generic;
using Guu.API.Others;
using SRML;
using UnityEngine;
using VikDisk.API.Identifiables;
using VikDisk.Game;

namespace VikDisk.Chapter1
{
	public class ElectricLargo : SynergyLargo
	{
		public override string Name => "Electric";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.ELECTRIC_LARGO_SYNERGY;

		public PediaDirector.Id PediaID { get; } = Enums.Pedia.ELECTRIC_LARGO_SYNERGY;

		protected override Sprite Icon => SRObjects.Get<Sprite>("iconLargoElectric");

		protected override Color Color => GameContext.Instance.LookupDirector.GetColor(Identifiable.Id.GLITCH_SLIME);

		protected override SlimeDefinition Definition { get; } = Others.Get<SlimeDefinitionItem>("ElectricLargo").Definition;

		protected override List<Identifiable.Id> FavoriteFoods => new List<Identifiable.Id> 
			{ Enums.Identifiables.GLITCH_POGO_FRUIT, Identifiable.Id.LEMON_PHASE };
		
		protected override List<Identifiable.Id> SuperFoods => new List<Identifiable.Id> 
			{ Identifiable.Id.CUBERRY_FRUIT }; // TODO: Change this

		protected override List<SlimeEat.FoodGroup> FoodGroups { get; } = new List<SlimeEat.FoodGroup>
			{ SlimeEat.FoodGroup.FRUIT };

		protected override Identifiable.Id Plort { get; } = Enums.Identifiables.REAL_GLITCH_PLORT;
		
		protected override Identifiable.Id Plort2 { get; } = Identifiable.Id.QUANTUM_PLORT;
		
		// TODO: Change this
		protected override Identifiable.Id SynergyPlort { get; } = Enums.Identifiables.DREAM_PLORT;

		protected override GameObject CustomBase => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.QUANTUM_SLIME) ?? 
		                                            SRObjects.Get<GameObject>("slimeQuantum");

		protected override void Build()
		{
			// Pre Build Manipulation

			// Build Control
			base.Build();

			// Post Build Manipulation
			Object.Destroy(Prefab.GetComponent<QuantumVibration>());
			Object.Destroy(Prefab.GetComponent<QuantumSlimeSuperposition>());
			Object.Destroy(Prefab.GetComponent<GenerateQuantumQubit>());
			
			GameObject part = Object.Instantiate(Packs.Chapter1.Get<GameObject>("prefabGlitchParticles"), Prefab.transform, true);
			part.name = "GlitchPart";
			part.transform.localScale = Vector3.one * 2.5f;

			ParticleSystem.EmissionModule emit = part.GetComponent<ParticleSystem>().emission;
			emit.rateOverTime = emit.rateOverTime.constant * 2;
		}
	}
}
