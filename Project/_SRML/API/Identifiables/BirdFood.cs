using System.Collections.Generic;
using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class to make hens
	/// </summary>
	public abstract class BirdFood : IdentifiableItem
	{
		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "bird";

		/// <summary>The base item to use when creating the one</summary>
		public GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.HEN) ?? SRObjects.Get<GameObject>("birdHen");

		/// <summary>Which slimes have this food as it's favorite</summary>
		public virtual List<Identifiable.Id> IsFavoritedBy { get; } = null;

		/// <summary>The mesh of this meat food</summary>
		public abstract Mesh Mesh { get; }

		/// <summary>The material of the model</summary>
		public Material ModelMat { get; private set; } = null;

		/// <summary>The scale of the object</summary>
		public override Vector3 Scale => Vector3.one * 0.7f;

		/// <summary>Is this animal a female? If yes it contains the behaviours for reproduction</summary>
		public virtual bool IsFemale { get; } = true;

		/// <summary>Is this animal elder? If not it contains the behaviours for aging</summary>
		public virtual bool IsElder { get; } = false;

		/// <summary>The id for the mate of this animal</summary>
		public virtual Identifiable.Id MateID { get; } = Identifiable.Id.ROOSTER;

		/// <summary>The ids of the animals that count as the same</summary>
		public virtual List<Identifiable.Id> DensityIDs { get; } = null;

		/// <summary>The id for the child of this animal</summary>
		public virtual Identifiable.Id ChildID { get; } = Identifiable.Id.CHICK;

		/// <summary>The min. game hours to reproduce</summary>
		public virtual float MinReproduceGameHours { get; } = 8f;

		/// <summary>The max. game hours to reproduce</summary>
		public virtual float MaxReproduceGameHours { get; } = 16f;

		/// <summary>The id for the elder version of this animal</summary>
		public virtual Identifiable.Id ElderID { get; } = Identifiable.Id.ELDER_HEN;

		/// <summary>Chance to become elder when reproducing</summary>
		public virtual float ElderChance { get; } = 0.05f;

		/// <summary>The scale of the Model</summary>
		public virtual Vector3 ModelScale { get; } = Vector3.one * 0.5f;

		/// <summary>Creates the material for the model</summary>
		public abstract Material CreateModelMat();

		/// <summary>Checks if it is a valid ammo for any storage type</summary>
		public override bool ValidSiloAmmo(SiloStorage.StorageType type)
		{
			return type == SiloStorage.StorageType.NON_SLIMES || type == SiloStorage.StorageType.FOOD || (IsElder && type == SiloStorage.StorageType.ELDER);
		}

		/// <summary>Builds this Item</summary>
		public override void Build()
		{
			// Load Material
			ModelMat = CreateModelMat();

			// Get GameObjects
			Prefab = PrefabUtils.CopyPrefab(BaseItem);
			Prefab.name = NamePrefix + Name;
			Prefab.transform.localScale = Scale;

			GameObject child = Prefab.FindChild("Body");
			child.transform.localScale = ModelScale;

			GameObject child2 = Prefab.transform.Find("Hen Hen/mesh_body1").gameObject;

			// Load Components
			Rigidbody body = Prefab.GetComponent<Rigidbody>();
			Vacuumable vac = Prefab.GetComponent<Vacuumable>();
			Identifiable iden = Prefab.GetComponent<Identifiable>();

			Reproduce rep = Prefab.GetComponent<Reproduce>();
			TransformChanceOnReproduce trans = Prefab.GetComponent<TransformChanceOnReproduce>();

			MeshFilter model = child.GetComponent<MeshFilter>();
			SkinnedMeshRenderer mRender = child2.GetComponent<SkinnedMeshRenderer>();

			// Setup Components
			body.mass = Mass;
			vac.size = Size;
			iden.id = ID;

			if (IsFemale)
			{
				rep.nearMateId = GameContext.Instance.LookupDirector.GetPrefab(MateID).GetComponent<Identifiable>();

				List<Identifiable> ids = new List<Identifiable>()
				{
					GameContext.Instance.LookupDirector.GetPrefab(Identifiable.Id.HEN).GetComponent<Identifiable>(),
					GameContext.Instance.LookupDirector.GetPrefab(Identifiable.Id.PAINTED_HEN).GetComponent<Identifiable>(),
					GameContext.Instance.LookupDirector.GetPrefab(Identifiable.Id.BRIAR_HEN).GetComponent<Identifiable>(),
					GameContext.Instance.LookupDirector.GetPrefab(Identifiable.Id.STONY_HEN).GetComponent<Identifiable>(),
					GameContext.Instance.LookupDirector.GetPrefab(Identifiable.Id.ELDER_HEN).GetComponent<Identifiable>()
				};

				if (DensityIDs != null)
				{
					foreach (Identifiable.Id id in DensityIDs)
					{
						Identifiable toAdd = GameContext.Instance.LookupDirector.GetPrefab(id).GetComponent<Identifiable>();
						if (!ids.Contains(toAdd))
							ids.Add(toAdd);
					}
				}

				rep.densityIds = ids.ToArray();
				rep.childPrefab = GameContext.Instance.LookupDirector.GetPrefab(ChildID);
				rep.minReproduceGameHours = MinReproduceGameHours;
				rep.maxReproduceGameHours = MaxReproduceGameHours;
			}
			else
				Object.Destroy(rep);

			if (!IsElder)
			{
				trans.transformChance = ElderChance;
				trans.targetPrefab = GameContext.Instance.LookupDirector.GetPrefab(ElderID);
			}
			else
				Object.Destroy(trans);

			model.sharedMesh = Mesh;
			mRender.sharedMaterial = ModelMat;

			foreach (MeshRenderer render in Prefab.transform.Find("Hen Hen/root").GetComponentsInChildren<MeshRenderer>())
			{
				if (render.sharedMaterial.name.Equals("HenHen"))
				{
					render.sharedMaterial = ModelMat;
				}
			}
		}

		/// <summary>Registers the item into it's registry</summary>
		public override IdentifiableItem Register()
		{
			base.Register();

			SlimeUtils.AddFoodToGroup(ID, SlimeEat.FoodGroup.MEAT, IsFavoritedBy);
			SlimeUtils.AddFoodToGroup(ID, SlimeEat.FoodGroup.NONTARRGOLD_SLIMES, IsFavoritedBy);

			Prefab.GetComponent<Reproduce>().childPrefab.GetComponent<TransformAfterTime>().options.Add(
				new TransformAfterTime.TransformOpt()
				{
					targetPrefab = Prefab,
					weight = 3
				}
			);

			return this;
		}
	}
}
