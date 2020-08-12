using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;
using Guu.Utils;

namespace VikDisk.Game
{
	[NoRegister]
	// TODO: Fix Dream Ornament cubemap
	public class DreamOrnament : Ornament
	{
		private static readonly int TINT_COLOR = Shader.PropertyToID("_TintColor");
		
		public override string Name => "Dream";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.DREAM_ORNAMENT;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconOrnamentDream");

		protected override Color Color => ColorUtils.FromHex("9afcff");

		protected override Material CreateModelMat()
		{
			Material mat = SRObjects.GetInst<Material>("ornament_tarr");
			//ModLogger.Log(mat.GetColor("_Color"));
			//mat.SetTexture("_testcube", );
			mat.SetColor(TINT_COLOR, Color);

			return mat;
		}
	}
}
