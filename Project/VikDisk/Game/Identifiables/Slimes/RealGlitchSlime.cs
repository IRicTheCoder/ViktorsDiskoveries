using System.Collections.Generic;
using SRML.SR;
using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;
using Guu.API.Others;

namespace VikDisk.Game
{
	public class RealGlitchSlime : Slime
	{
		public override string Name => "RealGlitch";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.REAL_GLITCH_SLIME;

		public PediaDirector.Id PediaID { get; } = Enums.Pedia.REAL_GLITCH_SLIME;

		protected override Sprite Icon => SRObjects.Get<Sprite>("iconSlimeGlitch");

		protected override Color Color => GameContext.Instance.LookupDirector.GetColor(Identifiable.Id.GLITCH_SLIME);

		protected override SlimeDefinition Definition { get; } = Others.Get<SlimeDefinitionItem>("defSlimeRealGlitch").Definition;

		protected override List<Identifiable.Id> FavoriteFoods => new List<Identifiable.Id>() { Enums.Identifiables.GLITCH_POGO_FRUIT };

		protected override List<SlimeEat.FoodGroup> FoodGroups { get; } = new List<SlimeEat.FoodGroup>()
		{
			SlimeEat.FoodGroup.FRUIT,
			SlimeEat.FoodGroup.NONTARRGOLD_SLIMES
		};

		protected override Identifiable.Id Plort { get; } = Enums.Identifiables.REAL_GLITCH_PLORT;

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
	}
}
