using Guu.API.Identifiables;
using Guu.Utils;
using SRML;
using UnityEngine;
using VikDisk.Components;

namespace VikDisk.Chapter1
{
	public class AlbinoPlort : Plort
	{
		public override string Name => "Albino";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.ALBINO_PLORT;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconPlortAlbino");

		protected override Color Color => Colors.Slimes.ALBINO[3];

		protected override bool CanBeSold => true;

		protected override float MarketValue => 14;

		protected override float MarketSaturationValue => 7;

		protected override Material CreateModelMat()
		{
			Material mat = SRObjects.GetInst<Material>("plortPinkBase");

			mat.SetTripleColor(Colors.Slimes.ALBINO);

			return mat;
		}
	}
}
