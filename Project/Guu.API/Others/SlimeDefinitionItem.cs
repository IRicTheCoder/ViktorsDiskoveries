using System;
using System.Collections.Generic;
using System.Linq;
using SRML.SR;
using UnityEngine;

namespace Guu.API.Others
{
	/// <summary>
	/// This is the base class for all Slime Definition Items
	/// </summary>
	public abstract class SlimeDefinitionItem : OtherItem
	{
		/// <summary>Base definition to base this in</summary>
		protected abstract SlimeDefinition BaseDef { get; }

		/// <summary>The definition created</summary>
		public SlimeDefinition Definition { get; private set; }

		/// <summary>The default appearances for this slime</summary>
		protected virtual SlimeAppearance[] AppearancesDefault => BaseDef.AppearancesDefault;

		/// <summary>The dynamic appearances for this slime</summary>
		protected virtual List<SlimeAppearance> AppearancesDynamic => new List<SlimeAppearance>(BaseDef.AppearancesDynamic);

		/// <summary>The base module for this slime</summary>
		protected virtual GameObject BaseModule => BaseDef.BaseModule;

		/// <summary>The base slimes in case it's a largo</summary>
		protected virtual SlimeDefinition[] BaseSlimes => BaseDef.BaseSlimes;

		/// <summary>Can become a largo?</summary>
		protected virtual bool CanLargofy => BaseDef.CanLargofy;

		/// <summary>The diet for this slime</summary>
		protected virtual SlimeDiet Diet => BaseDef.Diet;

		/// <summary>The favorite toys of this slime</summary>
		protected virtual Identifiable.Id[] FavoriteToys => BaseDef.FavoriteToys;

		/// <summary>The ID of this slime</summary>
		protected abstract Identifiable.Id IdentifiableId { get; }

		/// <summary>Is this slime a largo?</summary>
		protected virtual bool IsLargo => BaseDef.IsLargo;

		/// <summary>The name of this slime</summary>
		protected virtual string DefName => BaseDef.Name;

		/// <summary>The scale for the prefab of this slime</summary>
		protected virtual float PrefabScale => BaseDef.PrefabScale;

		/// <summary>The other modules for this slime</summary>
		protected virtual GameObject[] SlimeModules => BaseDef.SlimeModules;

		/// <summary>The sounds of this slime</summary>
		protected virtual SlimeSounds Sounds => BaseDef.Sounds;

		/// <summary>Builds this Definition</summary>
		protected override void Build()
		{
			Definition.AppearancesDefault = AppearancesDefault;
			Definition.AppearancesDynamic = AppearancesDynamic;
			Definition.BaseModule = BaseModule;
			Definition.BaseSlimes = BaseSlimes;
			Definition.CanLargofy = CanLargofy;
			
			Definition.Diet = new SlimeDiet
			{
				EatMap = new List<SlimeDiet.EatMapEntry>(Diet.EatMap),
				Favorites = Diet.Favorites,
				Produces = Diet.Produces,
				AdditionalFoods = Diet.AdditionalFoods,
				FavoriteProductionCount = Diet.FavoriteProductionCount,
				MajorFoodGroups = Diet.MajorFoodGroups
			};

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
