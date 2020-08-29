using Guu.API.Identifiables;
using Guu.Utils;
using SRML;
using UnityEngine;
using VikDisk.Components;

namespace VikDisk.Chapter1
{
	[NoRegister]
	// TODO: Fix Smoke
	public class TarrPlort : Plort
	{
		private static readonly int TOP_COLOR = Shader.PropertyToID("_TopColor");
		private static readonly int MIDDLE_COLOR = Shader.PropertyToID("_MiddleColor");
		private static readonly int BOTTOM_COLOR = Shader.PropertyToID("_BottomColor");
		
		public override string Name => "Tarr";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.PURE_TARR_PLORT;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconPlortTarr");

		protected override Color Color => ColorUtils.FromHex("5e5451");

		protected override Material CreateModelMat()
		{
			Material mat = SRObjects.GetInst<Material>("slimeTarr");

			mat.SetColor(TOP_COLOR, ColorUtils.FromHex("5e5451"));
			mat.SetColor(MIDDLE_COLOR, ColorUtils.FromHex("140a07"));
			mat.SetColor(BOTTOM_COLOR, ColorUtils.FromHex("000000"));

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

			PlortSmoke smoke = Prefab.AddComponent<PlortSmoke>();
			smoke.SetMaxCooldown(40);
		}
	}
}
