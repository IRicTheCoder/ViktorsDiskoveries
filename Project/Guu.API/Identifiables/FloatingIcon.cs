using SRML;
using SRML.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Guu.API.Identifiables
{
	/// <summary>
	/// This is the base class to make floating icon items
	/// </summary>
	public abstract class FloatingIcon : IdentifiableItem
	{
		// Base item to create this one
		private static GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.GLITCH_BUG_REPORT) ?? 
		                                      SRObjects.Get<GameObject>("glitchBugReport");
		
		// Overrides
		protected override string NamePrefix => "float";
		protected override Vector3 Scale => Vector3.one * 0.7f;

		// Virtual
		protected virtual bool DigitalEffect { get; } = false;

		// Methods
		protected override void Build()
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

			if (DigitalEffect) return;
			
			front.material = null;
			Object.Destroy(back.gameObject);
		}
	}
}
