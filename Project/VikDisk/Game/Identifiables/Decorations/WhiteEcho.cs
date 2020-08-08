using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;

namespace VikDisk.Game
{
	[NoRegister]
	public class WhiteEcho : Echo
	{
		public override string Name => "White";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.WHITE_ECHO;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconEchoWhite");

		protected override Color Color => ColorUtils.FromHex("bbbbbb");

		protected override Material CreateModelMat()
		{
			Material mat = SRObjects.GetInst<Material>("EchoBlue");
			mat.SetColor("_TintColor", Color);

			return mat;
		}
	}
}
