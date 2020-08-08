using System;
using System.Collections.Generic;
using SRML.SR;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class for all Slime Definition Items
	/// </summary>
	public abstract class SlimeDefinitionItem : OtherItem
	{
		/// <summary>Base definition to base this in</summary>
		public abstract SlimeDefinition BaseDef { get; }

		/// <summary>The definition created</summary>
		public SlimeDefinition Definition { get; private set; }

		/// <summary>The default appearances for this slime</summary>
		public virtual SlimeAppearance[] AppearancesDefault => BaseDef.AppearancesDefault;

		/// <summary>The dynamic appearances for this slime</summary>
		public virtual List<SlimeAppearance> AppearancesDynamic => new List<SlimeAppearance>(BaseDef.AppearancesDynamic);

		/// <summary>The base module for this slime</summary>
		public virtual GameObject BaseModule => BaseDef.BaseModule;

		/// <summary>The base slimes in case it's a largo</summary>
		public virtual SlimeDefinition[] BaseSlimes => BaseDef.BaseSlimes;

		/// <summary>Can become a largo?</summary>
		public virtual bool CanLargofy => BaseDef.CanLargofy;

		/// <summary>The diet for this slime</summary>
		public virtual SlimeDiet Diet => BaseDef.Diet;

		/// <summary>The favorite toys of this slime</summary>
		public virtual Identifiable.Id[] FavoriteToys => BaseDef.FavoriteToys;

		/// <summary>The ID of this slime</summary>
		public abstract Identifiable.Id IdentifiableId { get; }

		/// <summary>Is this slime a largo?</summary>
		public virtual bool IsLargo => BaseDef.IsLargo;

		/// <summary>The name of this slime</summary>
		public virtual string DefName => BaseDef.Name;

		/// <summary>The scale for the prefab of this slime</summary>
		public virtual float PrefabScale => BaseDef.PrefabScale;

		/// <summary>The other modules for this slime</summary>
		public virtual GameObject[] SlimeModules => BaseDef.SlimeModules;

		/// <summary>The sounds of this slime</summary>
		public virtual SlimeSounds Sounds => BaseDef.Sounds;

		/// <summary>Builds this Definition</summary>
		public override void Build()
		{
			Definition.AppearancesDefault = AppearancesDefault;
			Definition.AppearancesDynamic = AppearancesDynamic;
			Definition.BaseModule = BaseModule;
			Definition.BaseSlimes = BaseSlimes;
			Definition.CanLargofy = CanLargofy;
			Definition.Diet = Diet;
			Definition.FavoriteToys = FavoriteToys;
			Definition.IdentifiableId = IdentifiableId;
			Definition.IsLargo = IsLargo;
			Definition.Name = DefName;
			Definition.PrefabScale = PrefabScale;
			Definition.SlimeModules = SlimeModules;
			Definition.Sounds = Sounds;
		}

		/// <summary>Registers the slime definition into it's registry</summary>
		public override OtherItem Register()
		{
			Definition = ScriptableObject.CreateInstance<SlimeDefinition>();
			Definition.name = Name;

			Build();

			SlimeRegistry.RegisterSlimeDefinition(Definition);

			return this;
		}
	}
}
