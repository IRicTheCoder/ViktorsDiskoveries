using Guu.API.Identifiables;
using Guu.Utils;
using SRML;
using UnityEngine;
using VikDisk.Components;

namespace VikDisk.Chapter1
{
	public class GlitchPlort : Plort
	{
		private static readonly int TOP_COLOR = Shader.PropertyToID("_TopColor");
		private static readonly int MIDDLE_COLOR = Shader.PropertyToID("_MiddleColor");
		private static readonly int BOTTOM_COLOR = Shader.PropertyToID("_BottomColor");
		private static readonly int AVG_CYCLE_LENGTH = Shader.PropertyToID("_AvgCycleLength");
		private static readonly int CYCLE_GLITCH_RATIO = Shader.PropertyToID("_CycleGlitchRatio");
		private static readonly int CUTOFF = Shader.PropertyToID("_Cutoff");
		
		public override string Name => "RealGlitch";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.REAL_GLITCH_PLORT;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconPlortGlitch");

		protected override Color Color => GameContext.Instance.LookupDirector.GetColor(Identifiable.Id.GLITCH_SLIME);

		protected override Material CreateModelMat()
		{
			Material mat = SRObjects.GetInst<Material>("plortQuantumBase");

			// TODO: Fix coloring
			mat.SetColor(TOP_COLOR, ColorUtils.FromHex("0cb485"));
			mat.SetColor(MIDDLE_COLOR, ColorUtils.FromHex("912b66"));
			mat.SetColor(BOTTOM_COLOR, ColorUtils.FromHex("e64499"));

			mat.SetFloat(AVG_CYCLE_LENGTH, 5.97f);
			mat.SetFloat(CYCLE_GLITCH_RATIO, 0.75f);
			mat.SetFloat(CUTOFF, 0.25f);

			return mat;
		}

		protected override void Build()
		{
			// Pre Build Manipulation

			// Build Control
			base.Build();

			// Post Build Manipulation
			GameObject part = Object.Instantiate(Packs.Chapter1.Get<GameObject>("prefabGlitchParticles"), Prefab.transform, true);
			part.name = "GlitchPart";
			part.transform.localScale = Vector3.one;

			Prefab.AddComponent<GlitchedPlort>();
		}
	}
}
