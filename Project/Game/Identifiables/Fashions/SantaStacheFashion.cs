using SRML.Utils;
using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;

namespace VikDisk.Game
{
	[NoRegister]
	public class SantaStacheFashion : FashionIcon
	{
		public override string Name => "SantaStache";

		public override Identifiable.Id ID { get; } = Enums.Identifiables.SANTA_STACHE_FASHION;

		protected override Sprite Icon => Packs.Chapter1.Get<Sprite>("iconFashionSantaStache");

		protected override Color Color => ColorUtils.FromHex("dddddd");

		protected override void Build()
		{
			// Pre Build Manipulation

			// Build Control
			base.Build();

			// Post Build Manipulation
			Fashion fash = Prefab.GetComponent<Fashion>();
			Fashion other = SRObjects.Get<GameObject>("fashionHandlebar").GetComponent<Fashion>();

			fash.attachPrefab = PrefabUtils.CopyPrefab(other.attachPrefab);
			Material mat = SRObjects.GetInst<Material>("FashionPod1");

			for (int i = 0; i < 8; i++)
			{
				mat.SetColor("_Color" + i + "0", ColorUtils.FromHex("eeeeee"));
				mat.SetColor("_Color" + i + "1", ColorUtils.FromHex("aaaaaa"));
			}

			fash.attachPrefab.FindChild("model_fp_handlebars").GetComponent<MeshRenderer>().sharedMaterial = mat;
		}
	}
}
