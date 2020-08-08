using UnityEngine;
using SRML;
using VikDisk.API;
using VikDisk.API.Identifiables;

namespace VikDisk.Game
{
	[NoRegister]
	public class GlitchEffy : Effy
	{
		public override string Name => "Glitch";

		public override Identifiable.Id ID => Enums.Identifiables.GLITCH_EFFY;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconEffyGlitch");

		protected override Color Color => ColorUtils.FromHex("00ffaa");

		protected override GameObject EffyParticles => Packs.Chapter1.Get<GameObject>("prefabGlitchEffy");
	}
}
