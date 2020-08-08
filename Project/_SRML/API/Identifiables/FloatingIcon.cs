using SRML.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class to make floating icon items
	/// </summary>
	public abstract class FloatingIcon : IdentifiableItem
	{
		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "float";

		/// <summary>The base item to use when creating the one</summary>
		public GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.GLITCH_BUG_REPORT) ?? SRObjects.Get<GameObject>("glitchBugReport");

		/// <summary>Scale of this resource</summary>
		public override Vector3 Scale => Vector3.one * 0.7f;

		/// <summary>Should use the digital effect from bug reports?</summary>
		public virtual bool DigitalEffect { get; } = false;

		/// <summary>Builds this Item</summary>
		public override void Build()
		{
			// Get GameObjects
			Prefab = PrefabUtils.CopyPrefab(BaseItem);
			Prefab.name = NamePrefix + Name;
			Prefab.transform.localScale = Scale;

			// Load Components
			Rigidbody body = Prefab.GetComponent<Rigidbody>();
			Vacuumable vac = Prefab.GetComponent<Vacuumable>();
			Identifiable iden = Prefab.GetComponent<Identifiable>();

			Image front = Prefab.transform.Find("Icon Pivot/Animation Pivot/ShopIconUI/Image").GetComponent<Image>();
			Image back = Prefab.transform.Find("Icon Pivot/Animation Pivot/ShopIconUI/Image Back").GetComponent<Image>();

			// Setup Components
			body.mass = Mass;
			vac.size = Size;
			iden.id = ID;

			front.sprite = Icon;
			back.sprite = Icon;

			if (!DigitalEffect)
			{
				front.material = null;
				Object.Destroy(back.gameObject);
			}
		}
	}
}
