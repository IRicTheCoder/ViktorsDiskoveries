using System.Collections.Generic;
using SRML.SR;
using UnityEngine;
using VikDisk.Components;
using SRML;
using Guu.API;
using Guu.API.Identifiables;
using Guu.Utils;

namespace VikDisk.Game
{
	[NoRegister]
	// TODO: Fix Smoke
	public class DreamPlort : Plort
	{
		private static readonly int TOP_COLOR = Shader.PropertyToID("_TopColor");
		private static readonly int MIDDLE_COLOR = Shader.PropertyToID("_MiddleColor");
		private static readonly int BOTTOM_COLOR = Shader.PropertyToID("_BottomColor");
		private static readonly int COLOR_RAMP = Shader.PropertyToID("_ColorRamp");
		
		public override string Name => "Dream";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.DREAM_PLORT;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconPlortDream");

		protected override Color Color => ColorUtils.FromHex("9afcff");

		protected override Material CreateModelMat()
		{
			Material mat = SRObjects.GetInst<Material>("slimeTarr");

			mat.SetColor(TOP_COLOR, ColorUtils.FromHex("9afcff"));
			mat.SetColor(MIDDLE_COLOR, ColorUtils.FromHex("3194b2"));
			mat.SetColor(BOTTOM_COLOR, ColorUtils.FromHex("0e708f"));

			mat.SetTexture(COLOR_RAMP, TextureUtils.CreateRamp("ffffff", "86e9ff", "ffffff"));

			return mat;
		}

		protected override void Build()
		{
			// Pre Build Manipulation

			// Build Control
			base.Build();

			// Post Build Manipulation
			GameObject part = Object.Instantiate(Packs.Chapter1.Get<GameObject>("prefabSmokeParticles"), Prefab.transform, true);
			part.name = "SmokePart";
			part.transform.localScale = Vector3.one * 0.5f;

			ParticleSystem.MainModule main = part.GetComponent<ParticleSystem>().main;
			main.startColor = ColorUtils.FromHex("3194b2");

			ParticleSystem.EmissionModule emit = part.GetComponent<ParticleSystem>().emission;
			emit.rateOverTime = 15;

			PlortSmoke smoke = Prefab.AddComponent<PlortSmoke>();
			smoke.SetMaxCooldown(20);
		}
	}
}
