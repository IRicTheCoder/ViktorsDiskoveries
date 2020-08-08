using System.Collections.Generic;
using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class to make crates
	/// </summary>
	public abstract class Crate : IdentifiableItem
	{
		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "crate";

		/// <summary>The base item to use when creating the one</summary>
		public GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.CRATE_DESERT_01) ?? SRObjects.Get<GameObject>("crateDesert");

		/// <summary>The size of the vacuumable</summary>
		public override Vacuumable.Size Size { get; } = Vacuumable.Size.LARGE;

		/// <summary>The mesh of this resource</summary>
		public virtual Mesh Mesh { get; } = SRObjects.Get<Mesh>("objCrate");

		/// <summary>Scale of this resource</summary>
		public override Vector3 Scale => Vector3.one;

		/// <summary>The material of the model</summary>
		public Material ModelMat { get; private set; } = null;

		/// <summary>The min. items to spawn on break</summary>
		public virtual int MinSpawn { get; } = 4;

		/// <summary>The max. items to spawn on break</summary>
		public virtual int MaxSpawn { get; } = 6;

		/// <summary>The spawning options when the create breaks</summary>
		public virtual List<BreakOnImpact.SpawnOption> SpawnOptions { get; } = null;

		/// <summary>Creates the material for the model</summary>
		public abstract Material CreateModelMat();

		/// <summary>Builds this Item</summary>
		public override void Build()
		{
			// Load Material
			ModelMat = CreateModelMat();

			// Get GameObjects
			Prefab = PrefabUtils.CopyPrefab(BaseItem);
			Prefab.name = NamePrefix + Name;
			Prefab.transform.localScale = Scale;

			// Load Components
			MeshFilter filter = Prefab.GetComponent<MeshFilter>();
			MeshRenderer render = Prefab.GetComponent<MeshRenderer>();
			Rigidbody body = Prefab.GetComponent<Rigidbody>();
			Vacuumable vac = Prefab.GetComponent<Vacuumable>();
			Identifiable iden = Prefab.GetComponent<Identifiable>();

			BreakOnImpact imp = Prefab.GetComponent<BreakOnImpact>();

			// Setup Components
			filter.sharedMesh = Mesh;
			render.sharedMaterial = ModelMat;
			body.mass = Mass;
			vac.size = Size;
			iden.id = ID;

			imp.minSpawns = MinSpawn;
			imp.maxSpawns = MaxSpawn;
			imp.spawnOptions = SpawnOptions;
		}
	}
}
