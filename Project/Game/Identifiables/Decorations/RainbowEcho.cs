using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;

namespace VikDisk.Game
{
	[NoRegister]
	public class RainbowEcho : Echo
	{
		private static readonly int HUE_VARIANCE = Shader.PropertyToID("_HueVariance");
		private static readonly int HUE_SPEED = Shader.PropertyToID("_HueSpeed");
		
		public override string Name => "Rainbow";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.RAINBOW_ECHO;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconEchoRainbow");

		protected override Color Color => ColorUtils.FromHex("ffdddd");

		protected override Material CreateModelMat()
		{
			Material mat = SRObjects.GetInst<Material>("EchoBlue");
			mat.SetFloat(HUE_VARIANCE, 1f);
			mat.SetFloat(HUE_SPEED, 0.1f);

			return mat;
		}
	}
}
