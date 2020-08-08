using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;
using Guu.API.SpawnResources;

namespace VikDisk.Game
{
	public class GlitchPogoGardenDeluxe : TreeGardenResource
	{
		public override string Name => "GardenGlitchPogoDlx";

		protected override bool IsDeluxe => true;

		protected override Identifiable.Id PlantID => Identifiable.Id.GLITCH_BUG_REPORT;

		public override SpawnResource.Id ID => Enums.SpawnResources.GLITCH_POGOFRUIT_TREE_DLX;

		protected override Identifiable.Id[] ToSpawn => new Identifiable.Id[] { Identifiable.Id.POGO_FRUIT };

		protected override Identifiable.Id[] BonusToSpawn => new Identifiable.Id[] { Enums.Identifiables.GLITCH_POGO_FRUIT };

		protected override int MaxObjectsSpawned => 20;

		protected override int MinObjectsSpawned => 15;

		protected override Mesh PreviewMesh => SRObjects.Get<Mesh>("model_pogo");

		protected override Material PreviewMat => Identifiables.Get<PlantFood>(Enums.Identifiables.GLITCH_POGO_FRUIT).ModelMat;

		protected override float BonusChance => 0.2f;

		protected override int MinBonusSelections => 12;

		protected override void Build()
		{
			// Pre Build Manipulation
			// TODO: Add material changes

			// Build Control
			base.Build();

			// Post Build Manipulation
		}
	}
}
