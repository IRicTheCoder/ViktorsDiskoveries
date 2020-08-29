using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.SpawnResources;

namespace VikDisk.Game
{
	[NoRegister]
	public class GingerGardenDeluxe : PatchGardenResource
	{
		public override string Name => "GardenGingerDlx";

		protected override bool IsDeluxe => true;

		protected override Identifiable.Id PlantID => Identifiable.Id.GINGER_VEGGIE;

		public override SpawnResource.Id ID => SpawnResource.Id.GINGER_PATCH_DLX;

		protected override Identifiable.Id[] ToSpawn => new Identifiable.Id[] { Identifiable.Id.PARSNIP_VEGGIE };

		protected override Identifiable.Id[] BonusToSpawn => null;

		protected override int MaxObjectsSpawned => 30;

		protected override int MinObjectsSpawned => 23;

		protected override Mesh PreviewMesh => SRObjects.Get<Mesh>("model_ginger");

		protected override Material PreviewMat => SRObjects.Get<Material>("ginger");

		protected override float BonusChance => 0.1f;

		protected override int MinBonusSelections => 4;

		protected override void Build()
		{
			// Pre Build Manipulation

			// Build Control
			base.Build();

			// Post Build Manipulation
			SpawnResource spawn = Prefab.GetComponent<SpawnResource>();

			GameObject fixedVeggie = GardenResourceFixes.GetFixedPrefab(Identifiable.Id.GINGER_VEGGIE, (obj) => obj.GetComponent<ResourceCycle>().unripeGameHours = 12);
			spawn.BonusObjectsToSpawn = new[] { fixedVeggie };

			// Fix Sprouts
			foreach (GameObject sprout in Prefab.FindChildren("Sprout"))
			{
				sprout.GetComponent<MeshFilter>().sharedMesh = SRObjects.Get<Mesh>("sprout_parsnip");
				sprout.GetComponent<MeshRenderer>().sharedMaterial = SRObjects.Get<Material>("parsnip NoSway");
			}
		}
	}
}
