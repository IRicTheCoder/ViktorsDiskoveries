using System.Collections.Generic;
using Guu.API.Identifiables;
using Guu.API.Others;
using UnityEngine;
using VikDisk.Game;

namespace VikDisk.Chapter1
{
	public class AlbinoLargos : Largo
	{
		public override string Name => "Albino";

		protected override Sprite Icon => null;

		protected override Color Color => Colors.Slimes.ALBINO[3];

		protected override SlimeDefinition Definition => Others.Get<SlimeDefinitionItem>("Albino").Definition;

		public override List<Identifiable.Id> Blacklist => null;
	}
}
