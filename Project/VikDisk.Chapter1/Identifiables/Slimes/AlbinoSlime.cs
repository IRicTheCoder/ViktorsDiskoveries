using System.Collections.Generic;
using Guu.API.Identifiables;
using Guu.API.Others;
using SRML;
using SRML.Debug;
using UnityEngine;
using VikDisk.Game;

namespace VikDisk.Chapter1
{
	public class AlbinoSlime : Slime
	{
		public override string Name => "Albino";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.ALBINO_SLIME;

		public PediaDirector.Id PediaID { get; } = Enums.Pedia.ALBINO_SLIME;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconSlimeAlbino");

		protected override Color Color => Colors.Slimes.ALBINO[3];

		protected override SlimeDefinition Definition { get; } = Others.Get<SlimeDefinitionItem>("Albino").Definition;

		protected override List<Identifiable.Id> FavoriteFoods => new List<Identifiable.Id> 
			{ Identifiable.Id.CARROT_VEGGIE, Identifiable.Id.HEN, Identifiable.Id.POGO_FRUIT };

		protected override List<SlimeEat.FoodGroup> FoodGroups { get; } = new List<SlimeEat.FoodGroup>
		{
			SlimeEat.FoodGroup.FRUIT,
			SlimeEat.FoodGroup.VEGGIES,
			SlimeEat.FoodGroup.MEAT
		};

		protected override Identifiable.Id Plort { get; } = Enums.Identifiables.ALBINO_PLORT;
	}
}
