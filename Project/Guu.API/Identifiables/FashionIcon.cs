using SRML;
using SRML.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Guu.API.Identifiables
{
	/// <summary>
	/// This is the base class to make fashion items
	/// </summary>
	public abstract class FashionIcon : IdentifiableItem
	{
		// Base item to create this one
		private static GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.CLIP_ON_FASHION) ?? 
		                                      SRObjects.Get<GameObject>("fashionClipOn");
		
		// Overrides
		protected override string NamePrefix => "fashion";
		protected override Vector3 Scale => Vector3.one * 0.7f;

		// Virtual
		protected virtual Fashion.Slot Slot { get; } = Fashion.Slot.FRONT;
		protected virtual GameObject AttachPrefab { get; } = null;

		// Methods
		protected override void Build()
		{
			// Get GameObjects
			Prefab = PrefabUtils.CopyPrefab(BaseItem);
			Prefab.name = NamePrefix + Name;
			Prefab.transform.localScale = Scale;

			// Load Components
			Fashion fash = Prefab.GetComponent<Fashion>();
			Rigidbody body = Prefab.GetComponent<Rigidbody>();
			Vacuumable vac = Prefab.GetComponent<Vacuumable>();
			Identifiable iden = Prefab.GetComponent<Identifiable>();

			Image front = Prefab.transform.Find("Icon Pivot/ShopIconUI/Image").GetComponent<Image>();
			Image back = Prefab.transform.Find("Icon Pivot/ShopIconUI/Image Back").GetComponent<Image>();

			// Setup Components
			fash.slot = Slot;
			fash.attachPrefab = AttachPrefab ? AttachPrefab : fash.attachPrefab;

			body.mass = Mass;
			vac.size = Size;
			iden.id = ID;

			front.sprite = Icon;
			back.sprite = Icon;
		}
	}
}
