using SRML;
using Guu.API;
using Guu.API.Identifiables;
using Guu.Utils;

using SRML.Utils;
using UnityEngine;
using VikDisk.Components;

namespace VikDisk.API.Identifiables
{
	/// <summary>
	/// This is the base class to make effies
	/// </summary>
	public abstract class Effy : IdentifiableItem
	{
		// Shader Properties
		private static readonly int TINT_COLOR = Shader.PropertyToID("_TintColor");
		private static readonly int BRIGHTNESS = Shader.PropertyToID("_Brightness");
		private static readonly int ANIMATION_SPEED = Shader.PropertyToID("_AnimationSpeed");

		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "effy";

		/// <summary>The base item to use when creating the one</summary>
		private static GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.BLUE_ECHO) ?? SRObjects.Get<GameObject>("echoBlue");

		/// <summary>The particle object for this effy</summary>
		protected abstract GameObject EffyParticles { get; }

		/// <summary>The mesh of this resource</summary>
		protected virtual Mesh Mesh { get; } = SRObjects.Get<Mesh>("Quad");

		/// <summary>Scale of this resource</summary>
		protected override Vector3 Scale => Vector3.one;

		/// <summary>The material of the model</summary>
		private Material ModelMat { get; set; }

		/// <summary>The scale of the Model</summary>
		protected virtual Vector3 ModelScale { get; } = Vector3.one * 1.7f;

		/// <summary>Builds this Item</summary>
		protected override void Build()
		{
			// Load Material
			ModelMat = SRObjects.GetInst<Material>("EchoBlue");
			ModelMat.SetColor(TINT_COLOR, ColorUtils.FromHex("22222222"));
			ModelMat.SetFloat(BRIGHTNESS, 0.1f);
			ModelMat.SetFloat(ANIMATION_SPEED, 0f);

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

			// Setup Proximity
			Prefab.AddComponent<EffyProximity>();

			// Add Particles
			GameObject part = Object.Instantiate(EffyParticles, Prefab.transform, true);
			part.name = "EffyPart";
			part.transform.localScale = Vector3.one;
		}

		/// <summary>Registers the item into it's registry</summary>
		public override IdentifiableItem Register()
		{
			base.Register();

			// Makes the Effy work as an Echo too
			Identifiable.ECHO_CLASS.Add(ID);

			return this;
		}
	}
}
