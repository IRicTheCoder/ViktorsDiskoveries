using System.Collections.Generic;
using Guu.API.Identifiables;
using Guu.Utils;
using SRML;
using SRML.SR;
using UnityEngine;
using VikDisk.Game;

namespace VikDisk.Chapter1
{
	public class ExtraSpicyTofu : SpecialFood, IPediaRegistry
	{
		private static readonly int RAMP_RED = Shader.PropertyToID("_RampRed");
		private static readonly int RAMP_GREEN = Shader.PropertyToID("_RampGreen");
		private static readonly int RAMP_BLUE = Shader.PropertyToID("_RampBlue");
		private static readonly int RAMP_BLACK = Shader.PropertyToID("_RampBlack");
		
		public override string Name => "ExtraSpicy";

		protected override bool IsTofu { get; } = true;

		protected override Mesh Mesh { get; } = SRObjects.Get<Mesh>("tofu4");

		public override Identifiable.Id ID { get; } = Enums.Identifiables.EXTRA_SPICY_TOFU;

		public PediaDirector.Id PediaID { get; } = Enums.Pedia.EXTRA_SPICY_TOFU;

		public PediaRegistry.PediaCategory PediaCat { get; } = PediaRegistry.PediaCategory.RESOURCES;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconVeggieTofuExtra");

		protected override Color Color => GameContext.Instance.LookupDirector.GetColor(Identifiable.Id.SPICY_TOFU) * 0.5f;

		protected override List<Identifiable.Id> IsFavoritedBy => null;

		protected override Vector3 ModelScale => Vector3.one * 0.3f;

		protected override Material CreateModelMat()
		{
			Material mat = SRObjects.GetInst<Material>("spicyTofu");
			mat.SetTexture(RAMP_RED, TextureUtils.CreateRamp("8f1717", "e52600"));
			mat.SetTexture(RAMP_GREEN, TextureUtils.CreateRamp("b01c1c", "eb6200"));
			mat.SetTexture(RAMP_BLUE, TextureUtils.CreateRamp("8f1717", "e52600"));
			mat.SetTexture(RAMP_BLACK, TextureUtils.CreateRamp("a15636", "b37a57"));

			return mat;
		}
	}
}
