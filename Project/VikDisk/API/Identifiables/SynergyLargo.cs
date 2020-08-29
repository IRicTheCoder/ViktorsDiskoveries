using System.Collections.Generic;

using Guu;
using Guu.API.Identifiables;
using Guu.Utils;

using SRML;
using SRML.Utils;
using UnityEngine;

namespace VikDisk.API.Identifiables
{
	/// <summary>
	/// This is the base class to make synergistic largos
	/// </summary>
	public abstract class SynergyLargo : IdentifiableItem
	{
		// Base item to create this one
		private static GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.PINK_SLIME) ?? 
		                                      SRObjects.Get<GameObject>("slimePink");
		 
		// Overrides
		protected override string NamePrefix => "largo";
		
		// Abstracts
		protected abstract Identifiable.Id Plort { get; }
		protected abstract Identifiable.Id Plort2 { get; }
		protected abstract Identifiable.Id SynergyPlort { get; }
		
		protected abstract SlimeDefinition Definition { get; }
		protected abstract List<SlimeEat.FoodGroup> FoodGroups { get; }
		
		// Virtual
		protected virtual int Health { get; } = 20;
		
		public virtual float DamagePerAttack { get; } = 20;
		protected virtual float MinDriveToEat { get; } = 0.333f;
		protected virtual float DrivePerEat { get; } = 0.333f;
		protected virtual float AgitationPerEat { get; } = 0.15f;
		protected virtual float AgitationPerFavEat { get; } = 0.3f;
		
		protected virtual GameObject CustomBase { get; } = null;

		protected virtual List<Identifiable.Id> FavoriteFoods { get; } = null;
		protected virtual List<Identifiable.Id> SuperFoods { get; } = null;
		
		// Methods
		protected override bool ValidSiloAmmo(SiloStorage.StorageType type) => false;

		protected override void Build()
		{
			// Get GameObjects
			Prefab = CustomBase != null ? PrefabUtils.CopyPrefab(CustomBase) : PrefabUtils.CopyPrefab(BaseItem);
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
			Identifiable.LARGO_CLASS.Add(ID);

			base.Register();

			if (Definition.Diet == null)
				Definition.Diet = new SlimeDiet();
			
			if (SuperFoods != null)
				FavoriteFoods.AddRange(SuperFoods);
			
			SlimeDiet diet = Definition.Diet;

			// TODO: Add the new diets
			if (FoodGroups.Contains(SlimeEat.FoodGroup.FRUIT))
			{
				SlimeUtils.PopulateDiet(ID, Identifiable.FRUIT_CLASS, diet, FavoriteFoods, Plort);
				SlimeUtils.PopulateDiet(ID, Identifiable.FRUIT_CLASS, diet, FavoriteFoods, Plort2);
			}

			if (FoodGroups.Contains(SlimeEat.FoodGroup.VEGGIES))
			{
				SlimeUtils.PopulateDiet(ID, Identifiable.VEGGIE_CLASS, diet, FavoriteFoods, Plort);
				SlimeUtils.PopulateDiet(ID, Identifiable.VEGGIE_CLASS, diet, FavoriteFoods, Plort2);
			}

			if (FoodGroups.Contains(SlimeEat.FoodGroup.MEAT))
			{
				SlimeUtils.PopulateDiet(ID, Identifiable.MEAT_CLASS, diet, FavoriteFoods, Plort);
				SlimeUtils.PopulateDiet(ID, Identifiable.MEAT_CLASS, diet, FavoriteFoods, Plort2);
			}

			if (FoodGroups.Contains(SlimeEat.FoodGroup.GINGER))
			{
				SlimeUtils.PopulateDiet(ID, new[] { Identifiable.Id.GINGER_VEGGIE }, diet, FavoriteFoods, Plort);
				SlimeUtils.PopulateDiet(ID, new[] { Identifiable.Id.GINGER_VEGGIE }, diet, FavoriteFoods, Plort2);
			}

			if (FoodGroups.Contains(SlimeEat.FoodGroup.NONTARRGOLD_SLIMES))
			{
				SlimeUtils.PopulateDiet(ID, Identifiable.TOFU_CLASS, diet, FavoriteFoods, Plort);
				SlimeUtils.PopulateDiet(ID, Identifiable.TOFU_CLASS, diet, FavoriteFoods, Plort2);
			}
			
			// Populates the synergy part
			SlimeUtils.PopulateDiet(ID, SuperFoods, diet, SuperFoods, SynergyPlort);

			return this;
		}
	}
}
