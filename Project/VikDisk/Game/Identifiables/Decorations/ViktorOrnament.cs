using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;
using Guu.Utils;

namespace VikDisk.Game
{
	[NoRegister]
	public class ViktorOrnament : Ornament
	{
		private static readonly int TESTCUBE = Shader.PropertyToID("_testcube");
		
		public override string Name => "Viktor";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.VIKTOR_ORNAMENT;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconOrnamentViktor");

		protected override Color Color => ColorUtils.FromHex("6793a6");

		protected override Material CreateModelMat()
		{
			Material mat = SRObjects.GetInst<Material>("ornament_tarr");
			mat.SetTexture(TESTCUBE, Packs.Chapter1.Get<Cubemap>("ornamentViktor"));

			return mat;
		}
	}
}
