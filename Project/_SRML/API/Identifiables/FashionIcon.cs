using SRML.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class to make fashion items
	/// </summary>
	public abstract class FashionIcon : IdentifiableItem
	{
		/// <summary>The name prefix for this object</summary>
		protected override string NamePrefix => "fashion";

		/// <summary>The base item to use when creating the one</summary>
		public GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPrefab(Identifiable.Id.CLIP_ON_FASHION) ?? SRObjects.Get<GameObject>("fashionClipOn");

		/// <summary>Scale of this resource</summary>
		public override Vector3 Scale => Vector3.one * 0.7f;

		/// <summary>Slot to place the fashion on</summary>
		public virtual Fashion.Slot Slot { get; } = Fashion.Slot.FRONT;

		/// <summary>The prefab to attach</summary>
		public virtual GameObject AttachPrefab { get; } = null;

		/// <summary>Builds this Item</summary>
		public override void Build()
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
			fash.attachPrefab = AttachPrefab ?? fash.attachPrefab;

			body.mass = Mass;
			vac.size = Size;
			iden.id = ID;

			front.sprite = Icon;
			back.sprite = Icon;
		}
	}
}
