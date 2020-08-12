using Guu.Utils;

using UnityEngine;
using SRML;
using VikDisk.API;
using VikDisk.API.Identifiables;

namespace VikDisk.Game
{
	[NoRegister]
	public class CartoonStartsEffy : Effy
	{
		public override string Name => "CartoonStars";

		public override Identifiable.Id ID => Enums.Identifiables.CARTOON_STARS_EFFY;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconEffyCartoonStars");

		protected override Color Color => ColorUtils.FromHex("aaaaaa");

		protected override GameObject EffyParticles => Packs.Chapter1.Get<GameObject>("prefabCartoonStarsEffy");
	}
}
