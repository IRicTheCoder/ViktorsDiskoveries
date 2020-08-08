using System.Collections.Generic;
using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;

namespace VikDisk.Game
{
	[NoRegister]
	public class FireChick : BirdBaby
	{
		private static readonly int RAMP_GREEN = Shader.PropertyToID("_RampGreen");
		private static readonly int RAMP_BLUE = Shader.PropertyToID("_RampBlue");
		private static readonly int RAMP_BLACK = Shader.PropertyToID("_RampBlack");
		
		public override string Name => "Fire";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.FIRE_CHICK;

		protected override Mesh Mesh { get; } = SRObjects.Get<Mesh>("mesh_body1");

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconBirdChickFire");

		protected override Color Color => ColorUtils.FromHex("ffee00");

		protected override List<TransformAfterTime.TransformOpt> Options => new List<TransformAfterTime.TransformOpt>()
		{
			new TransformAfterTime.TransformOpt()
			{
				targetPrefab = GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.ROOSTER) ?? SRObjects.Get<GameObject>("birdRooster"),
				weight = 1
			}
		};

		protected override Material CreateModelMat()
		{
			Material mat = SRObjects.GetInst<Material>("Chickadoo");
			mat.SetTexture(RAMP_GREEN, TextureUtils.CreateRamp("c30101", "ff0505"));
			mat.SetTexture(RAMP_BLUE, TextureUtils.CreateRamp("fd6c01", "ffcfac"));
			mat.SetTexture(RAMP_BLACK, TextureUtils.CreateRamp("fd6c01", "ffcfac"));

			return mat;
		}
	}
}
