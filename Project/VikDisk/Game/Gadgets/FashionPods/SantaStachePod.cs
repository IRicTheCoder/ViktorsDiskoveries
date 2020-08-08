using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Gadgets;

namespace VikDisk.Game
{
	[NoRegister]
	public class SantaStachePod : FashionPodGadget
	{
		public override string Name => "SantaStache";

		protected override Gadget.Id ID => Enums.Gadgets.FASHION_POD_SANTA_STACHE;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconFashionSantaStache");

		protected override Identifiable.Id FashionID => Enums.Identifiables.SANTA_STACHE_FASHION;

		protected override PediaDirector.Id PediaID => PediaDirector.Id.CURIOS;

		protected override int BlueprintCost => 200;

		protected override Gadget.Id[] CountIDs => Gadget.ALL_FASHIONS.ToArray();

		protected override bool StartAvailable => true;

		protected override bool IsDefault => false;

		protected override GadgetDefinition.CraftCost[] CraftCosts => new GadgetDefinition.CraftCost[]
		{
			CraftCost(Identifiable.Id.PINK_PLORT, 10),
			CraftCost(Identifiable.Id.BUZZ_WAX_CRAFT, 5)
		};
	}
}
