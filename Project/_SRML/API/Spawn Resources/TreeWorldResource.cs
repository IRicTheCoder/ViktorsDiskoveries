using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class to make world spawn resources (trees)
	/// </summary>
	public abstract class TreeWorldResource : SpawnResourceItem
	{
		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "tree";

		/// <summary>Is this a garden resource?</summary>
		sealed protected override bool IsGarden => false;

		/// <summary>The mesh for the tree</summary>
		public virtual Mesh TreeMesh { get; } = null;

		/// <summary>The material for the tree</summary>
		public Material TreeMat { get; private set; } = null;

		/// <summary>The scale of the tree</summary>
		public virtual Vector3 TreeScale { get; } = Vector3.one * 0.45f;

		/// <summary>The mesh for the leaves</summary>
		public virtual Mesh LeavesMesh { get; } = null;

		/// <summary>The material for the leaves</summary>
		public Material LeavesMat { get; private set; } = null;

		/// <summary>The scale of the leaves</summary>
		public virtual Vector3 LeavesScale { get; } = Vector3.one * 0.5f;

		/// <summary>The base item to use when creating the one</summary>
		protected virtual GameObject BaseItem => SRObjects.Get<GameObject>("treePogo02");

		/// <summary>Creates the material for the tree</summary>
		public virtual Material CreateTreeMat() { return null; }

		/// <summary>Creates the material for the leaves</summary>
		public virtual Material CreateLeavesMat() { return null; }

		/// <summary>Builds this Item</summary>
		public override void Build()
		{
			// Load Material
			TreeMat = CreateTreeMat();
			LeavesMat = CreateLeavesMat();

			// Get GameObjects
			Prefab = PrefabUtils.CopyPrefab(BaseItem);
			Prefab.name = NamePrefix + Name;

			GameObject tree = Prefab.FindChild("tree_pogo");
			tree.transform.localScale = TreeScale;

			GameObject leaves = Prefab.FindChild("leaves_pogo");
			leaves.transform.localScale = LeavesScale;

			// Load Components
			SpawnResource spawn = Prefab.GetComponent<SpawnResource>();

			MeshFilter tMesh = tree.GetComponent<MeshFilter>();
			MeshRenderer tRender = tree.GetComponent<MeshRenderer>();
			MeshCollider tCol = tree.GetComponent<MeshCollider>();

			MeshFilter lMesh = leaves.GetComponent<MeshFilter>();
			MeshRenderer lRender = leaves.GetComponent<MeshRenderer>();
			MeshCollider lCol = tree.GetComponent<MeshCollider>();

			// Setup Components
			spawn.id = ID;
			spawn.ObjectsToSpawn = GameContext.Instance.LookupDirector.GetPrefabs(ToSpawn);
			spawn.BonusObjectsToSpawn = BonusToSpawn != null ? GameContext.Instance.LookupDirector.GetPrefabs(BonusToSpawn) : new GameObject[0];
			spawn.MaxObjectsSpawned = MaxObjectsSpawned;
			spawn.MinObjectsSpawned = MinObjectsSpawned;
			spawn.MinNutrientObjectsSpawned = MinNutrientObjectsSpawned == -1 ? MaxObjectsSpawned : MinNutrientObjectsSpawned;
			spawn.MinSpawnIntervalGameHours = MinSpawnIntervalGameHours;
			spawn.MaxSpawnIntervalGameHours = MaxSpawnIntervalGameHours;
			spawn.BonusChance = BonusChance;
			spawn.minBonusSelections = MinBonusSelections;

			tMesh.sharedMesh = TreeMesh ?? tMesh.sharedMesh;
			tRender.sharedMaterial = TreeMat ?? tRender.sharedMaterial;
			tCol.sharedMesh = TreeMesh ?? tCol.sharedMesh;

			lMesh.sharedMesh = LeavesMesh ?? lMesh.sharedMesh;
			lRender.sharedMaterial = LeavesMat ?? lRender.sharedMaterial;
			lCol.sharedMesh = LeavesMesh ?? lCol.sharedMesh;

			// Builds the Spawn Points
			BuildSpawnPoints(spawn);
		}
	}
}
