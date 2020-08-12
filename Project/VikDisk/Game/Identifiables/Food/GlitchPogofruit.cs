using System.Collections.Generic;
using SRML.SR;
using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;
using Guu.Utils;

namespace VikDisk.Game
{
	public class GlitchPogofruit : PlantFood, IPediaRegistry
	{
		private static readonly int RAMP_GREEN = Shader.PropertyToID("_RampGreen");
		private static readonly int RAMP_BLUE = Shader.PropertyToID("_RampBlue");
		private static readonly int RAMP_BLACK = Shader.PropertyToID("_RampBlack");
		
		public override string Name => "GlitchPogo";

		protected override bool IsFruit { get; } = true;

		protected override Mesh Mesh { get; } = SRObjects.Get<Mesh>("model_pogofruit");

		public override Identifiable.Id ID { get; } = Enums.Identifiables.GLITCH_POGO_FRUIT;

		public PediaDirector.Id PediaID { get; } = Enums.Pedia.GLITCH_POGO_FRUIT;

		public PediaRegistry.PediaCategory PediaCat { get; } = PediaRegistry.PediaCategory.RESOURCES;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconFruitGlitch");

		protected override Color Color => GameContext.Instance.LookupDirector.GetColor(Identifiable.Id.POGO_FRUIT) * 1.6f;

		protected override List<Identifiable.Id> IsFavoritedBy => new List<Identifiable.Id>() { Enums.Identifiables.REAL_GLITCH_SLIME };

		protected override Material CreateModelMat()
		{
			Material mat = SRObjects.GetInst<Material>("pogo");
			mat.SetTexture(RAMP_GREEN, TextureUtils.CreateRamp("178f61", "00dee5"));
			mat.SetTexture(RAMP_BLUE, TextureUtils.CreateRamp("a6418c", "d453b4"));
			mat.SetTexture(RAMP_BLACK, TextureUtils.CreateRamp("3c115d", "d453b4"));

			return mat;
		}

		protected override void Build()
		{
			// Pre Build Manipulation

			// Build Control
			base.Build();

			// Post Build Manipulation
			GameObject part = Object.Instantiate(Packs.Chapter1.Get<GameObject>("prefabGlitchParticles"), Prefab.transform, true);
			part.name = "GlitchPart";
			part.transform.localScale = Vector3.one;
		}
	}
}
