using System.Collections.Generic;
using SRML.SR;
using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;
using Guu.API.Others;

using VikDisk.API.Identifiables;

namespace VikDisk.Game
{
	/*public class ElectricLargo : SynergyLargo
	{
		public override string Name => "Electric";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.ELETRIC_LARGO;

		public PediaDirector.Id PediaID { get; } = Enums.Pedia.ELETRIC_LARGO;

		protected override Sprite Icon => SRObjects.Get<Sprite>("iconLargoElectric");

		protected override Color Color => GameContext.Instance.LookupDirector.GetColor(Identifiable.Id.GLITCH_SLIME);

		protected override SlimeDefinition Definition { get; } = Others.Get<SlimeDefinitionItem>("defLargoElectric").Definition;

		protected override List<Identifiable.Id> FavoriteFoods => new List<Identifiable.Id>() 
			{ Enums.Identifiables.GLITCH_POGO_FRUIT, Identifiable.Id.LEMON_PHASE };
		
		protected override List<Identifiable.Id> SuperFoods => new List<Identifiable.Id>() 
			{ Identifiable.Id.CUBERRY_FRUIT };

		protected override List<SlimeEat.FoodGroup> FoodGroups { get; } = new List<SlimeEat.FoodGroup>()
		{
			SlimeEat.FoodGroup.FRUIT,
			SlimeEat.FoodGroup.NONTARRGOLD_SLIMES
		};

		protected override Identifiable.Id Plort { get; } = Enums.Identifiables.REAL_GLITCH_PLORT;
		
		protected override Identifiable.Id Plort2 { get; } = Identifiable.Id.QUANTUM_PLORT;
		
		protected override Identifiable.Id SynergyPlort { get; } = Enums.Identifiables.DREAM_PLORT;

		protected override void Build()
		{
			// Pre Build Manipulation

			// Build Control
			base.Build();

			// Post Build Manipulation
			GameObject part = Object.Instantiate(Packs.Chapter1.Get<GameObject>("prefabGlitchParticles"), Prefab.transform, true);
			part.name = "GlitchPart";
			part.transform.localScale = Vector3.one * 1.5f;

			ParticleSystem.EmissionModule emit = part.GetComponent<ParticleSystem>().emission;
			emit.rateOverTime = emit.rateOverTime.constant * 2;
		}
	}*/
}
