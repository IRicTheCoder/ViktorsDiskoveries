using System.Collections.Generic;
using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class to make slimes
	/// </summary>
	public abstract class Slime : IdentifiableItem
	{
		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "slime";

		/// <summary>The base item to use when creating the one</summary>
		public GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.PINK_SLIME) ?? SRObjects.Get<GameObject>("slimePink");

		/// <summary>List of Favorite Foods</summary>
		public virtual List<Identifiable.Id> FavoriteFoods { get; } = null;

		/// <summary>The definition for this slime (Diets are auto added, it ignores the ones already added)</summary>
		public abstract SlimeDefinition Definition { get; }

		/// <summary>A list with all food groups this slime has (Plorts will be treated as food if added to this list)</summary>
		public abstract List<SlimeEat.FoodGroup> FoodGroups { get; }

		/// <summary>The ID for the plort this slime drops</summary>
		public abstract Identifiable.Id Plort { get; }

		/// <summary>Damage per attack</summary>
		public virtual float DamagePerAttack { get; } = 20;

		/// <summary>The min. drive to eat</summary>
		public virtual float MinDriveToEat { get; } = 0.333f;

		/// <summary>The drive increase per eat</summary>
		public virtual float DrivePerEat { get; } = 0.333f;

		/// <summary>The agitation reduced per eat</summary>
		public virtual float AgitationPerEat { get; } = 0.15f;

		/// <summary>The agitation reduced per fav eat</summary>
		public virtual float AgitationPerFavEat { get; } = 0.3f;

		/// <summary>The max health of this slime</summary>
		public virtual int Health { get; } = 20;

		/// <summary>Checks if it is a valid ammo for any storage type</summary>
		public override bool ValidSiloAmmo(SiloStorage.StorageType type) { return false; }

		/// <summary>Builds this Item</summary>
		public override void Build()
		{
			// Get GameObjects
			Prefab = PrefabUtils.CopyPrefab(BaseItem);
			Prefab.name = NamePrefix + Name;
			Prefab.transform.localScale = Scale * Definition.PrefabScale;

			// Load Components
			SlimeAppearanceApplicator app = Prefab.GetComponent<SlimeAppearanceApplicator>();
			SlimeVarietyModules mod = Prefab.GetComponent<SlimeVarietyModules>();
			SlimeEat eat = Prefab.GetComponent<SlimeEat>();
			SlimeHealth hp = Prefab.GetComponent<SlimeHealth>();

			Rigidbody body = Prefab.GetComponent<Rigidbody>();
			Vacuumable vac = Prefab.GetComponent<Vacuumable>();
			Identifiable iden = Prefab.GetComponent<Identifiable>();

			// Setup Components
			app.SlimeDefinition = Definition;
			mod.baseModule = Definition.BaseModule;
			mod.slimeModules = Definition.SlimeModules;

			eat.slimeDefinition = Definition;
			eat.minDriveToEat = MinDriveToEat;
			eat.drivePerEat = DrivePerEat;
			eat.agitationPerEat = AgitationPerEat;
			eat.agitationPerFavEat = AgitationPerFavEat;

			hp.maxHealth = Health;

			body.mass = Mass;
			vac.size = Size;
			iden.id = ID;

			// Get rid of unneeded components
			Object.Destroy(Prefab.GetComponent<PinkSlimeFoodTypeTracker>());
		}

		/// <summary>Registers the item into it's registry</summary>
		public override IdentifiableItem Register()
		{
			base.Register();

			if (Definition.Diet == null)
				Definition.Diet = new SlimeDiet();

			SlimeDiet diet = Definition.Diet;

			if (FoodGroups.Contains(SlimeEat.FoodGroup.FRUIT))
				SlimeUtils.PopulateDiet(ID, Identifiable.FRUIT_CLASS, diet, FavoriteFoods, this);

			if (FoodGroups.Contains(SlimeEat.FoodGroup.VEGGIES))
				SlimeUtils.PopulateDiet(ID, Identifiable.VEGGIE_CLASS, diet, FavoriteFoods, this);

			if (FoodGroups.Contains(SlimeEat.FoodGroup.MEAT))
				SlimeUtils.PopulateDiet(ID, Identifiable.MEAT_CLASS, diet, FavoriteFoods, this);

			if (FoodGroups.Contains(SlimeEat.FoodGroup.GINGER))
				SlimeUtils.PopulateDiet(ID, new[] { Identifiable.Id.GINGER_VEGGIE }, diet, FavoriteFoods, this);

			if (FoodGroups.Contains(SlimeEat.FoodGroup.PLORTS))
			{
				SlimeUtils.PopulateDiet(ID, Identifiable.PLORT_CLASS, diet, FavoriteFoods, this, true);
				Definition.CanLargofy = false;
			}

			if (FoodGroups.Contains(SlimeEat.FoodGroup.NONTARRGOLD_SLIMES))
			{
				SlimeUtils.PopulateDiet(ID, Identifiable.TOFU_CLASS, diet, FavoriteFoods, this);
			}

			return this;
		}
	}
}
