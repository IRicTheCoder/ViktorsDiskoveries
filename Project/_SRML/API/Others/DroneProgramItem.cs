using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML.API
{
	// TODO: Incomplete
	/// <summary>
	/// This is the base class to make drone programs
	/// </summary>
	[System.Obsolete("Marked as obsolete cause it's not yet implemented. Missing the registry for it!")]
	public abstract class DroneProgramItem : OtherItem
	{
		/*/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "echo";

		/// <summary>The base item to use when creating the one</summary>
		public GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.ECHO_NOTE_01) ?? SRObjects.Get<GameObject>("echoNote01");

		/// <summary>The mesh of this resource</summary>
		public virtual Mesh Mesh { get; } = SRObjects.Get<Mesh>("Quad");

		/// <summary>Scale of this resource</summary>
		public override Vector3 Scale => Vector3.one;

		/// <summary>The material of the model</summary>
		public Material ModelMat { get; private set; } = null;

		/// <summary>The scale of the Model</summary>
		public virtual Vector3 ModelScale { get; } = Vector3.one * 1.7f;

		/// <summary>Is this echo a note?</summary>
		public virtual bool IsNote { get; } = false;

		/// <summary>The clip for this note</summary>
		public virtual int Clip { get; } = 1;

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
		}*/
	}
}
