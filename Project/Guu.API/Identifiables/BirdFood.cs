using System.Collections.Generic;

using Guu.Utils;

using SRML;
using SRML.Utils;
using UnityEngine;

namespace Guu.API.Identifiables
{
	/// <summary>
	/// This is the base class to make hens
	/// </summary>
	public abstract class BirdFood : IdentifiableItem
	{
		// Base item to create this one
		private static GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.HEN) ?? 
		                                      SRObjects.Get<GameObject>("birdHen");
		
		// Material for the Model
		private Material ModelMat { get; set; }
		
		// Overrides
		protected override string NamePrefix => "bird";
		protected override Vector3 Scale => Vector3.one * 0.7f;
		
		// Abstracts
		protected abstract Mesh Mesh { get; }
		
		// Virtual
		protected virtual bool IsFemale { get; } = true;
		protected virtual bool IsElder { get; } = false;
		
		protected virtual float MinReproduceGameHours { get; } = 8f;
		protected virtual float MaxReproduceGameHours { get; } = 16f;
		protected virtual float ElderChance { get; } = 0.05f;
		
		protected virtual Vector3 ModelScale { get; } = Vector3.one * 0.5f;
		
		protected virtual Identifiable.Id MateID { get; } = Identifiable.Id.ROOSTER;
		protected virtual Identifiable.Id ChildID { get; } = Identifiable.Id.CHICK;
		protected virtual Identifiable.Id ElderID { get; } = Identifiable.Id.ELDER_HEN;
		
		protected virtual List<Identifiable.Id> IsFavoritedBy { get; } = null;
		protected virtual List<Identifiable.Id> DensityIDs { get; } = null;

		// Methods
		protected abstract Material CreateModelMat();
		
		protected override bool ValidSiloAmmo(SiloStorage.StorageType type) =>
			type == SiloStorage.StorageType.NON_SLIMES || 
			type == SiloStorage.StorageType.FOOD || 
			(IsElder && type == SiloStorage.StorageType.ELDER);

		protected override void Build()
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
