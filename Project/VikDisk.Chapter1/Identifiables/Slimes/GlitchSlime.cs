using System.Collections.Generic;
using Guu.API.Identifiables;
using Guu.API.Others;
using SRML;
using UnityEngine;
using VikDisk.Game;

namespace VikDisk.Chapter1
{
	public class GlitchSlime : Slime
	{
		public override string Name => "RealGlitch";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.REAL_GLITCH_SLIME;

		public PediaDirector.Id PediaID { get; } = Enums.Pedia.REAL_GLITCH_SLIME;

		protected override Sprite Icon => SRObjects.Get<Sprite>("iconSlimeGlitch");

		protected override Color Color => GameContext.Instance.LookupDirector.GetColor(Identifiable.Id.GLITCH_SLIME);

		protected override SlimeDefinition Definition { get; } = Others.Get<SlimeDefinitionItem>("RealGlitch").Definition;

		protected override List<Identifiable.Id> FavoriteFoods => new List<Identifiable.Id>() { Enums.Identifiables.GLITCH_POGO_FRUIT };

		protected override List<SlimeEat.FoodGroup> FoodGroups { get; } = new List<SlimeEat.FoodGroup>()
		{
			SlimeEat.FoodGroup.FRUIT
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
