using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;
using Guu.Utils;

namespace VikDisk.Game
{
	[NoRegister]
	public class GuuLiquid : Liquid
	{
		// TODO: Change all properties to their base classes for easy use
		private static readonly int COLOR_RAMP = Shader.PropertyToID("_ColorRamp");
		private static readonly int WAVE_FADE = Shader.PropertyToID("_WaveFade");
		private static readonly int WAVE_SPEED = Shader.PropertyToID("_WaveSpeed");
		private static readonly int WAVE_NOISE = Shader.PropertyToID("_WaveNoise");
		private static readonly int WAVE_HEIGHT = Shader.PropertyToID("_WaveHeight");
		private static readonly int REFRACTED_LIGHT_FADE = Shader.PropertyToID("_RefractedLightFade");
		private static readonly int REFRACTION_AMOUNT = Shader.PropertyToID("_RefractionAmount");
		private static readonly int DIRT = Shader.PropertyToID("_Dirt");
		private static readonly int DIRT_FADE = Shader.PropertyToID("_DirtFade");
		
		public override string Name => "Guu";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.GUU_LIQUID;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconGuu");

		protected override Color Color => ColorUtils.FromHex("44cc00");

		protected override Material CreateModelMat()
		{
			Material mat = SRObjects.GetInst<Material>("Depth Water Ball");

			mat.SetTexture(COLOR_RAMP, TextureUtils.CreateRamp("5b6f09", "68bb30"));
			mat.SetFloat(WAVE_FADE, 0);
			mat.SetFloat(WAVE_SPEED, 0);
			mat.SetFloat(WAVE_NOISE, 0);
			mat.SetFloat(WAVE_HEIGHT, 0);

			mat.SetFloat(REFRACTED_LIGHT_FADE, 0);
			mat.SetFloat(REFRACTION_AMOUNT, 0);

			mat.SetTexture(DIRT, null);
			mat.SetFloat(DIRT_FADE, 0);

			return mat;
		}

		protected override void Build()
		{
			// Pre Build Manipulation

			// Build Control
			base.Build();

			// Post Build Manipulation
			Object.Destroy(Prefab.FindChild("Sphere").FindChild("FX Sprinkler 1"));

			DestroyOnTouching dest = Prefab.GetComponent<DestroyOnTouching>();

			Object.Destroy(dest.destroyFX.FindChild("Bubbles"));
			Object.Destroy(dest.destroyFX.FindChild("Sparkles"));
			Object.Destroy(dest.destroyFX.FindChild("Wave"));

			ParticleSystem.MainModule main = dest.destroyFX.FindChild("Hit").GetComponent<ParticleSystem>().main;
			main.startColor = ColorUtils.FromHex("68bb30");
		}
	}
}
