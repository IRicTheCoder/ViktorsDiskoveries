using System.Collections.Generic;
using SRML.Utils;
using UnityEngine;
using SRML;
using Guu.API;
using Guu.API.Identifiables;
using Guu.API.Others;

namespace VikDisk.Game
{
	public class RealGlitchLargos : Largo
	{
		public override string Name => "RealGlitch";

		protected override Sprite Icon => null;

		protected override Color Color => GameContext.Instance.LookupDirector?.GetColor(Identifiable.Id.GLITCH_SLIME) ?? Color.white;

		protected override SlimeDefinition Definition => Others.Get<SlimeDefinitionItem>("defSlimeRealGlitch").Definition;

		public override List<Identifiable.Id> Blacklist => null;
	}
}
