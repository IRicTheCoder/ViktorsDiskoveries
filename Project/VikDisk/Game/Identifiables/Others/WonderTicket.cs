using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;
using Guu.Utils;

namespace VikDisk.Game
{
	[NoRegister]
	public class WonderTicket : FloatingIcon
	{
		public override string Name => "WonderTicket";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.WONDER_TICKET;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconWonderTicket");

		protected override Color Color => ColorUtils.FromHex("ff9999");
	}
}
