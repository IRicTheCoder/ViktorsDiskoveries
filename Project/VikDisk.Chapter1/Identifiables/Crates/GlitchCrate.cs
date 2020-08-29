using System.Collections.Generic;
using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;

namespace VikDisk.Game
{
	[NoRegister]
	public class GlitchCrate : Crate
	{
		public override string Name => "Glitch";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.GLITCH_CRATE;

		protected override List<BreakOnImpact.SpawnOption> SpawnOptions => new List<BreakOnImpact.SpawnOption>()
		{
			new BreakOnImpact.SpawnOption()
			{
				spawn = GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.GLITCH_SLIME) ?? SRObjects.Get<GameObject>("slimeGlitch"),
				weight = 1
			}
		};

		protected override Material CreateModelMat() { return SRObjects.Get<Material>("objWoodKit01"); }

		protected override void Build()
		{
			// Pre Build Manipulation

			// Build Control
			base.Build();

			// Post Build Manipulation
			GameObject part = Object.Instantiate(Packs.Chapter1.Get<GameObject>("prefabGlitchParticles"), Prefab.transform, true);
			part.name = "GlitchPart";
			part.transform.localScale = Vector3.one * 2f;
		}
	}
}
