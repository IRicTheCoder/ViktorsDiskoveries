using System.Collections.Generic;
using Guu.API.Identifiables;
using Guu.API.Others;
using SRML;
using UnityEngine;
using VikDisk.Game;

namespace VikDisk.Chapter1
{
	public class DigiTarrSlime : Slime
	{
		public override string Name => "RealDigiTarr";

		protected override GameObject CustomBase => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.GLITCH_TARR_SLIME) ?? 
		                                            SRObjects.Get<GameObject>("slimeGlitchTarr");

		public override Identifiable.Id ID { get; } = Enums.Identifiables.REAL_DIGITARR_SLIME;

		public PediaDirector.Id PediaID { get; } = Enums.Pedia.REAL_DIGITARR_SLIME;

		protected override Sprite Icon => SRObjects.Get<Sprite>("iconSlimeTarr");

		protected override Color Color => GameContext.Instance.LookupDirector.GetColor(Identifiable.Id.GLITCH_TARR_SLIME);

		protected override SlimeDefinition Definition { get; } = Others.Get<SlimeDefinitionItem>("RealGlitchTarr").Definition;

		protected override List<SlimeEat.FoodGroup> FoodGroups { get; } = new List<SlimeEat.FoodGroup>();

		protected override Identifiable.Id Plort { get; } = Identifiable.Id.NONE;

		protected override void Build()
		{
			// Pre Build Manipulation

			// Build Control
			base.Build();

			// Post Build Manipulation
			Object.Destroy(Prefab.GetComponent<DestroyAfterTime>());
			Object.Destroy(Prefab.GetComponent<RotTouchedResources>());
			Object.Destroy(Prefab.GetComponent<GlitchTarrSterilizeOnWater>());
			Object.Destroy(Prefab.GetComponent<AttackPlayer>());
			Object.Destroy(Prefab.GetComponent<TarrBite>());
			Object.Destroy(Prefab.GetComponent<GotoPlayer>());
			
			/*GameObject part = Object.Instantiate(Packs.Chapter1.Get<GameObject>("prefabGlitchParticles"), Prefab.transform, true);
			part.name = "GlitchPart";
			part.transform.localScale = Vector3.one * 1.5f;

			ParticleSystem.EmissionModule emit = part.GetComponent<ParticleSystem>().emission;
			emit.rateOverTime = emit.rateOverTime.constant * 2;*/
		}
	}
}
