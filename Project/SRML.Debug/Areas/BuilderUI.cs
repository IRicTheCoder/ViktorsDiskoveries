using SRML.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SRML.Areas
{
	/// <summary>
	/// The UI for the Area Builder
	/// </summary>
	public class BuilderUI : SRBehaviour
	{
		/// <summary>Current selected index</summary>
		public int SelectedIndex { get; set; } = 0;

		/// <summary>All slots available</summary>
		public Slot[] slots = new Slot[]
		{
			new Slot("Manipulate", SRObjects.Get<Sprite>("iconSSUtilities")),
			new Slot("Add Object", SRObjects.Get<Sprite>("iconSSBlueprints")),
			new Slot("Remove Object", SRObjects.Get<Sprite>("iconSSWarpTech")),
			new Slot("Dump Target", SRObjects.Get<Sprite>("iconSSDLC")),
			new Slot("Copy Target", SRObjects.Get<Sprite>("iconSSDecorations"))
		};

		// The Lookup Director
		private LookupDirector lookupDir;

		// The ID for the animation of being selected
		private int animSelectedId;

		// The last selected index
		internal int lastSelectedIndex = -1;

		// The prefab for a slot
		private GameObject slotPrefab;

		/// <summary>The visibility of this UI</summary>
		public bool IsVisible { get; private set; } = false;

		// Awakes the script
		private void Awake()
		{
			lookupDir = GameContext.Instance.LookupDirector;
			animSelectedId = Animator.StringToHash("selected");

			// Clears the slots
			foreach (Transform child in transform)
			{
				if (child.name.Equals("Ammo Slot 1"))
				{
					slotPrefab = PrefabUtils.CopyPrefab(child.gameObject);
					slotPrefab.name = "Builder Slot";

					GameObject internSlot = slotPrefab.FindChild("Ammo Slot");
					Destroy(internSlot.GetComponent<StatusBar>());
					Destroy(internSlot.FindChild("Status"));
					Destroy(internSlot.FindChild("ValueText"));
				}

				Destroy(child.gameObject);
			}
		}

		// Starts the script
		private void Start()
		{
			//selected = Instantiate(AmmoSlotUI.Instance.selectedPrefab);

			// Builds the slots
			for (int i = 0; i < slots.Length; i++)
			{
				Slot slot = slots[i];

				CreateSlot(slot, i + 1);
			}
		}

		// Updates the script
		private void Update()
		{
			if (SelectedIndex != lastSelectedIndex)
			{
				if (lastSelectedIndex > -1)
					slots[lastSelectedIndex].anim.SetBool(animSelectedId, false);

				slots[SelectedIndex].anim.SetBool(animSelectedId, true);

				lastSelectedIndex = SelectedIndex;
			}
		}

		/// <summary>
		/// Sets the visibility of the UI
		/// </summary>
		/// <param name="visible">The visible state</param>
		public void SetVisibility(bool visible)
		{
			IsVisible = visible;
			foreach (Slot slot in slots)
				slot.main.SetActive(IsVisible);
		}

		// Creates a new slot
		private void CreateSlot(Slot slot, int slotNumber)
		{
			slot.main = Instantiate(slotPrefab, transform, true);
			slot.main.name = $"Builder Slot {slotNumber}";

			slot.anim = slot.main.GetComponent<Animator>();

			GameObject internSlot = slot.main.FindChild("Ammo Slot");

			slot.back = internSlot.FindChild("Behind").GetComponent<Image>();
			slot.back.sprite = AmmoSlotUI.Instance.backFilled;
			slot.back.color = Color.green;

			slot.front = internSlot.FindChild("Frame").GetComponent<Image>();
			slot.front.sprite = AmmoSlotUI.Instance.frontFilled;
			slot.front.color = new Color(0.65f, 1f, 0.5f, 1f);

			slot.label = slot.main.FindChild("Label").GetComponent<TMP_Text>();
			slot.label.text = slot.slotName;

			slot.icon = slot.main.FindChild("Icon").GetComponent<Image>();
			slot.icon.sprite = slot.slotIcon;
			slot.icon.enabled = true;

			slot.keyBinding = slot.main.FindChild("Keybinding");
			slot.keyBinding.FindChild("Text").GetComponent<TMP_Text>().text = slotNumber.ToString();
			slot.keyBinding.GetComponent<Image>().color = new Color(0.65f, 1f, 0.5f, 1f);

			slot.main.SetActive(IsVisible);
		}		

		/// <summary>
		/// The Slot for this UI
		/// </summary>
		[System.Serializable]
		public class Slot
		{
			/// <summary>The main object of this slot</summary>
			public string slotName;

			/// <summary>The icon for this slot</summary>
			public Sprite slotIcon;

			/// <summary>The main object of this slot</summary>
			public GameObject main;

			/// <summary>The image that contains the icon</summary>
			public Image icon;

			/// <summary>The animator for this slot</summary>
			public Animator anim;

			/// <summary>The back image</summary>
			public Image back;

			/// <summary>The front image</summary>
			public Image front;

			/// <summary>The label</summary>
			public TMP_Text label;

			/// <summary>The key binding</summary>
			public GameObject keyBinding;

			/// <summary>
			/// Creates a new slot with the given name
			/// </summary>
			/// <param name="name">Name to add to the slot</param>
			/// <param name="icon">The icon to display</param>
			public Slot(string name, Sprite icon)
			{
				slotName = name;
				slotIcon = icon;
			}
		}
	}
}
