using System.Collections.Generic;
using SRML.SR;
using UnityEngine;
using VikDisk.Components;
using SRML;
using Guu.API;
using Guu.API.Identifiables;

namespace VikDisk.Game
{
	[NoRegister]
	// TODO: Fix collisions
	public class DarkGinger : PlantFood, IPediaRegistry
	{
		private static readonly int RAMP_GREEN = Shader.PropertyToID("_RampGreen");
		private static readonly int RAMP_BLUE = Shader.PropertyToID("_RampBlue");
		private static readonly int RAMP_BLACK = Shader.PropertyToID("_RampBlack");
		
		public override string Name => "DarkGinger";

		protected override bool IsFruit { get; } = false;

		protected override Mesh Mesh { get; } = SRObjects.Get<Mesh>("model_ginger");

		public override Identifiable.Id ID { get; } = Enums.Identifiables.DARK_GINGER_VEGGIE;

		public PediaDirector.Id PediaID { get; } = Enums.Pedia.DARK_GINGER_VEGGIE;

		public PediaRegistry.PediaCategory PediaCat { get; } = PediaRegistry.PediaCategory.RESOURCES;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconVeggieDarkGinger");

		protected override Color Color => ColorUtils.FromHex("433452");

		protected override Vector3 ModelScale => Vector3.one * 0.15f;

		protected override Material CreateModelMat()
		{
			Material mat = SRObjects.GetInst<Material>("ginger");
			mat.SetTexture(RAMP_GREEN, TextureUtils.CreateRamp("5a3d75", "74598e"));
			mat.SetTexture(RAMP_BLUE, TextureUtils.CreateRamp("292929", "393939"));
			mat.SetTexture(RAMP_BLACK, TextureUtils.CreateRamp("8e8e8e", "e7e7e7"));

			return mat;
		}

		protected override void Build()
		{
			// Pre Build Manipulation

			// Build Control
			base.Build();

			// Post Build Manipulation
			GameObject part = Object.Instantiate(Packs.Chapter1.Get<GameObject>("prefabCorrusiveParticles"), Prefab.transform, true);
			part.name = "CurrPart";
			part.transform.localScale = Vector3.one;

			Prefab.AddComponent<CorrusiveAura>();
		}
	}
}
