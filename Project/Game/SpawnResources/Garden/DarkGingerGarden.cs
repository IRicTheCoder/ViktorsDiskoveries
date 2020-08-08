using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;
using Guu.API.SpawnResources;

namespace VikDisk.Game
{
	[NoRegister]
	public class DarkGingerGarden : PatchGardenResource
	{
		public override string Name => "GardenDarkGinger";

		protected override bool IsDeluxe => false;

		protected override Identifiable.Id PlantID => Enums.Identifiables.DARK_GINGER_VEGGIE;

		public override SpawnResource.Id ID => Enums.SpawnResources.DARK_GINGER_PATCH;

		protected override Identifiable.Id[] ToSpawn => new Identifiable.Id[] { Identifiable.Id.BEET_VEGGIE, Identifiable.Id.CARROT_VEGGIE, Identifiable.Id.CARROT_VEGGIE };

		protected override Identifiable.Id[] BonusToSpawn => new Identifiable.Id[] { Enums.Identifiables.DARK_GINGER_VEGGIE };

		protected override int MaxObjectsSpawned => 20;

		protected override int MinObjectsSpawned => 15;

		protected override Mesh PreviewMesh => SRObjects.Get<Mesh>("model_ginger");

		protected override Material PreviewMat => Identifiables.Get<PlantFood>(Enums.Identifiables.DARK_GINGER_VEGGIE).ModelMat;

		protected override float BonusChance => 0.2f;

		protected override int MinBonusSelections => 3;
	}
}
