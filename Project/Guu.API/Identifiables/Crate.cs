using System.Collections.Generic;

using SRML;
using SRML.Utils;
using UnityEngine;

namespace Guu.API.Identifiables
{
	/// <summary>
	/// This is the base class to make crates
	/// </summary>
	public abstract class Crate : IdentifiableItem
	{
		// Base item to create this one
		private static GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.CRATE_DESERT_01) ?? 
		                                      SRObjects.Get<GameObject>("crateDesert");
		
		// Material for the Model
		private Material ModelMat { get; set; }
		
		// Overrides
		protected override string NamePrefix => "crate";
		protected override Vector3 Scale => Vector3.one;
		
		protected override Vacuumable.Size Size { get; } = Vacuumable.Size.LARGE;

		// Virtual
		protected virtual int MinSpawn { get; } = 4;
		protected virtual int MaxSpawn { get; } = 6;
		
		protected virtual Mesh Mesh { get; } = SRObjects.Get<Mesh>("objCrate");
		
		protected virtual List<BreakOnImpact.SpawnOption> SpawnOptions { get; } = null;

		// Methods
		protected abstract Material CreateModelMat();
		
		protected override void Build()
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
