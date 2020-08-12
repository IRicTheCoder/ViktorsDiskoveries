using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;
using Guu.Utils;

namespace VikDisk.Game
{
	[NoRegister]
	public class FireHen : BirdFood
	{
		private static readonly int RAMP_GREEN = Shader.PropertyToID("_RampGreen");
		private static readonly int RAMP_BLUE = Shader.PropertyToID("_RampBlue");
		private static readonly int RAMP_BLACK = Shader.PropertyToID("_RampBlack");
		
		public override string Name => "Fire";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.FIRE_HEN;

		protected override Mesh Mesh { get; } = SRObjects.Get<Mesh>("mesh_body1");

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconBirdHenFire");

		protected override Color Color => ColorUtils.FromHex("ffee00");

		protected override Identifiable.Id ChildID => Enums.Identifiables.FIRE_CHICK;

		protected override Material CreateModelMat()
		{
			Material mat = SRObjects.GetInst<Material>("HenHen");
			mat.SetTexture(RAMP_GREEN, TextureUtils.CreateRamp("c30101", "ff0505"));
			mat.SetTexture(RAMP_BLUE, TextureUtils.CreateRamp("fd6c01", "ffcfac"));
			mat.SetTexture(RAMP_BLACK, TextureUtils.CreateRamp("fd6c01", "ffcfac"));

			return mat;
		}
	}
}
