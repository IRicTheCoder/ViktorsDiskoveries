using SRML;
using SRML.Utils;
using UnityEngine;

namespace Guu.API.Identifiables
{
	/// <summary>
	/// This is the base class to make echos
	/// </summary>
	public abstract class Echo : IdentifiableItem
	{
		// Base item to create this one
		private static GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.ECHO_NOTE_01) ??
		                                      SRObjects.Get<GameObject>("echoNote01");
		
		// Material for the Model
		private Material ModelMat { get; set; }
		
		// Overrides
		protected override string NamePrefix => "echo";
		protected override Vector3 Scale => Vector3.one;

		// Virtual
		protected virtual bool IsNote { get; } = false;
		protected virtual int Clip { get; } = 1;
		
		protected virtual Vector3 ModelScale { get; } = Vector3.one * 1.7f;
		
		protected virtual Mesh Mesh { get; } = SRObjects.Get<Mesh>("Quad");
		
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

			GameObject child = Prefab.FindChild("model");
			child.transform.localScale = ModelScale;

			// Load Components
			Rigidbody body = Prefab.GetComponent<Rigidbody>();
			Vacuumable vac = Prefab.GetComponent<Vacuumable>();
			Identifiable iden = Prefab.GetComponent<Identifiable>();

			MeshFilter filter = child.GetComponent<MeshFilter>();
			MeshRenderer render = child.GetComponent<MeshRenderer>();

			// Setup Components
			body.mass = Mass;
			vac.size = Size;
			iden.id = ID;

			filter.sharedMesh = Mesh;
			render.sharedMaterial = ModelMat;

			// Echo Note
			if (IsNote)
			{
				EchoNote note = Prefab.FindChild("echo_note").GetComponent<EchoNote>();
				note.clip = Clip;
			}
			else
				Object.Destroy(Prefab.FindChild("echo_note"));
		}
	}
}
