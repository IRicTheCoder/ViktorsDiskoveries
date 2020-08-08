using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;

namespace VikDisk.Game
{
	[NoRegister]
	public class ToyBasketBall : Toy
	{
		private static readonly int COLOR_MASK = Shader.PropertyToID("_ColorMask");
		private static readonly int COLOR30 = Shader.PropertyToID("_Color30");
		private static readonly int COLOR31 = Shader.PropertyToID("_Color31");
		
		public override string Name => "BasketBall";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.BASKET_BALL_TOY;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconToyBasketBall");

		protected override Color Color => ColorUtils.FromHex("ff8800");

		protected override Material CreateModelMat()
		{
			Material mat = SRObjects.GetInst<Material>("Toy_BeechBall");
			mat.SetTexture(COLOR_MASK, Packs.Chapter1.Get<Texture2D>("maskSlimeToysCustom"));

			for (int i = 0; i < 8; i++)
			{
				mat.SetColor("_Color" + i + "0", ColorUtils.FromHex("ff8800"));
				mat.SetColor("_Color" + i + "1", ColorUtils.FromHex("cc4400"));
			}

			mat.SetColor(COLOR30, ColorUtils.FromHex("333333"));
			mat.SetColor(COLOR31, ColorUtils.FromHex("222222"));

			return mat;
		}
	}
}
