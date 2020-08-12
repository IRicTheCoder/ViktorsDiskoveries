using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;
using Guu.Utils;

namespace VikDisk.Game
{
	[NoRegister]
	public class PinkSlimeExtract : SlimeResource
	{
		private static readonly int COLOR20 = Shader.PropertyToID("_Color20");
		private static readonly int COLOR21 = Shader.PropertyToID("_Color21");
		private static readonly int COLOR30 = Shader.PropertyToID("_Color30");
		private static readonly int COLOR31 = Shader.PropertyToID("_Color31");
		
		public override string Name => "PinkExtract";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.PINK_SLIME_EXTRACT_CRAFT;

		protected override Mesh Mesh => SRObjects.Get<Mesh>("royalJelly_ld");

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconExtractPink");

		protected override Color Color => ColorUtils.FromHex("ff88ee");

		protected override Vector3 ModelScale => Vector3.one * 0.8f;

		protected override Material CreateModelMat()
		{
			Material mat = SRObjects.GetInst<Material>("pepperJam");

			mat.SetColor(COLOR20, ColorUtils.FromHex("f52f53") * 1.4f);
			mat.SetColor(COLOR21, ColorUtils.FromHex("f52f53") * 0.7f);

			mat.SetColor(COLOR30, ColorUtils.FromHex("f52f53") * 0.3f);
			mat.SetColor(COLOR31, ColorUtils.FromHex("f52f53") * 1.4f);

			return mat;
		}
	}
}
