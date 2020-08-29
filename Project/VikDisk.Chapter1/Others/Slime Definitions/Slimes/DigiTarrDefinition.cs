using Guu.API.Others;
using SRML;
using UnityEngine;

namespace VikDisk.Chapter1
{
	/// <summary>
	/// The slime definition for the Real Digi-Tarr Slime
	/// </summary>
	public class DigiTarrDefinition : SlimeDefinitionItem
	{
		public override string Name { get; } = "RealGlitchTarr";

		protected override SlimeDefinition BaseDef { get; } = GameContext.Instance?.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.GLITCH_TARR_SLIME) ?? 
		                                                      SRObjects.Get<SlimeDefinition>("Glitch Tarr");

		protected override Identifiable.Id IdentifiableId { get; } = Enums.Identifiables.REAL_DIGITARR_SLIME;

		protected override bool CanLargofy => false;

		protected override float PrefabScale => 2f;

		protected override void Build()
		{
			base.Build();

			Object.Destroy(Definition.SlimeModules[0].GetComponent<DestroyAfterTime>());
			Object.Destroy(Definition.SlimeModules[0].GetComponent<RotTouchedResources>());
			Object.Destroy(Definition.SlimeModules[0].GetComponent<GlitchTarrSterilizeOnWater>());
			Object.Destroy(Definition.SlimeModules[0].GetComponent<AttackPlayer>());
			Object.Destroy(Definition.SlimeModules[0].GetComponent<TarrBite>());
			Object.Destroy(Definition.SlimeModules[0].GetComponent<GotoPlayer>());
		}
	}
}
