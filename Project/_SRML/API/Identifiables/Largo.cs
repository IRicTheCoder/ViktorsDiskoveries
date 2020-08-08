using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML.API
{
	// TODO: Fix largos, as they seem to not be able to be created

	/// <summary>
	/// This is the base class to make largos
	/// </summary>
	public abstract class Largo : IdentifiableItem
	{
		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "slime";

		/// <summary>The base item to use when creating the one</summary>
		public GameObject BaseItem => SRObjects.BaseLargo;

		/// <summary>The ID is not needed for largos</summary>
		sealed public override Identifiable.Id ID => Identifiable.Id.NONE;

		/// <summary>Make the vac size Large as this is a Largo</summary>
		public override Vacuumable.Size Size => Vacuumable.Size.LARGE;

		/// <summary>The definition for the base slime of this largo</summary>
		public abstract SlimeDefinition Definition { get; }

		/// <summary>A list of Slime IDs to blacklist</summary>
		public abstract List<Identifiable.Id> Blacklist { get; }

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

		/// <summary>The prefabs of each Largo</summary>
		public Dictionary<Identifiable.Id, GameObject> Prefabs { get; } = new Dictionary<Identifiable.Id, GameObject>();

		/// <summary>The vac entries of each Largo</summary>
		public Dictionary<Identifiable.Id, VacItemDefinition> VacEntries { get; } = new Dictionary<Identifiable.Id, VacItemDefinition>();

		/// <summary>The default translation of each Largo</summary>
		public Dictionary<Identifiable.Id, string> DefTranslation { get; } = new Dictionary<Identifiable.Id, string>();

		/// <summary>Checks if it is a valid ammo for any storage type</summary>
		public override bool ValidSiloAmmo(SiloStorage.StorageType type) { return false; }

		/// <summary>Builds this Item</summary>
		public override void Build()
		{
			//foreach (Identifiable.Id slime in Identifiable.SLIME_CLASS)
			//{
				// Make new Definition
				SlimeDefinition other = SlimeUtils.GetDefinitionByID(Identifiable.Id.PINK_SLIME);
				SlimeDefinition newDef = Definition.CombineForLargo(other);

				// Translation
				DefTranslation.Add(newDef.IdentifiableId, Regex.Replace(Definition.Name, "(\\B[A-Z])", " $1") + " " + Regex.Replace(other.Name, "(\\B[A-Z])", " $1") + " Largo");

				//if (newDef == null)
				//	continue;

				// Get GameObjects
				Prefab = PrefabUtils.CopyPrefab(BaseItem);
				Prefab.name = NamePrefix + Definition.Name + other.Name;
				Prefab.transform.localScale = Scale * newDef.PrefabScale;

				// PRIMARY SETUP
				// Load Components
				SlimeAppearanceApplicator app = Prefab.GetComponent<SlimeAppearanceApplicator>();
				SlimeVarietyModules mod = Prefab.GetComponent<SlimeVarietyModules>();

				// Setup Components
				app.SlimeDefinition = newDef;
				mod.baseModule = newDef.BaseModule;
				mod.slimeModules = newDef.SlimeModules;

				mod.Assemble();

				// SECONDARY SETUP
				// Load Components
				SlimeEat eat = Prefab.GetComponent<SlimeEat>();
				SlimeHealth hp = Prefab.GetComponent<SlimeHealth>();

				Rigidbody body = Prefab.GetComponent<Rigidbody>();
				Vacuumable vac = Prefab.GetComponent<Vacuumable>();
				Identifiable iden = Prefab.GetComponent<Identifiable>();

				// Setup Components
				eat.slimeDefinition = newDef;
				eat.minDriveToEat = MinDriveToEat;
				eat.drivePerEat = DrivePerEat;
				eat.agitationPerEat = AgitationPerEat;
				eat.agitationPerFavEat = AgitationPerFavEat;

				hp.maxHealth = Health;

				body.mass = Mass;
				vac.size = Size;
				iden.id = newDef.IdentifiableId;

				// TERCIARY SETUP
				// Load Components
				SlimeEmotions emot = Prefab.GetComponent<SlimeEmotions>();
				SlimeEmotions emot2 = BaseItem.GetComponent<SlimeEmotions>();

				emot.initAgitation = emot2.initAgitation;
				emot.initFear = emot2.initFear;
				emot.initHunger = emot2.initHunger;

				// Add to Largo List
				Prefabs.Add(newDef.IdentifiableId, Prefab);
			//}
		}

		/// <summary>Registers the item into it's registry</summary>
		public override IdentifiableItem Register()
		{
			Build();

			foreach(Identifiable.Id ID in Prefabs.Keys)
			{
				IdentifiableRegistry.CategorizeId(ID);

				if (GameContext.Instance.MessageDirector.Get("actor", "l." + ID.ToString().ToLower()) == null)
					TranslationPatcher.AddActorTranslation("l." + ID.ToString().ToLower(), DefTranslation[ID]);

				LookupRegistry.RegisterIdentifiablePrefab(Prefabs[ID]);

				VacEntry = VacItemDefinition.CreateVacItemDefinition(ID, Color, Icon ?? SRObjects.MissingIcon);
				LookupRegistry.RegisterVacEntry(VacEntry);
				VacEntries.Add(ID, VacEntry);

				if (IsVacuumableOverride || Size == Vacuumable.Size.NORMAL)
					AmmoRegistry.RegisterPlayerAmmo(PlayerState.AmmoMode.DEFAULT, ID);

				if (IsRefineryResource)
					AmmoRegistry.RegisterRefineryResource(ID);

				AmmoRegistry.RegisterSiloAmmo(ValidSiloAmmo, ID);
			}

			VacEntry = null;
			return this;
		}
	}
}
