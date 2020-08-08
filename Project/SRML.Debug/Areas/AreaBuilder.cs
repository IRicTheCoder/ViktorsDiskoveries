using System.Collections.Generic;
using UnityEngine;
using SRML.Debug;

namespace SRML.Areas
{
	/// <summary>
	/// Controls the Area Builder. A tool that
	/// allows you to create things in the map
	/// </summary>
	public class AreaBuilder : MonoBehaviour
	{
		// TODO: Stop the ability to manipulate original objects

		/// <summary>A easy to get value for the player's target</summary>
		public static GameObject Target => SceneContext.Instance.PlayerState.Targeting;

		/// <summary>The vac pack component</summary>
		public static WeaponVacuum VacPack { get; private set; }

		/// <summary>The UI for the builder</summary>
		public static BuilderUI UI { get; private set; }

		/// <summary>Is the builder on?</summary>
		public static bool IsBuilderOn { get; private set; } = false;

		/// <summary>The selected object</summary>
		public static GameObject SelectedObject { get; private set; }

		// A list of all objects turned off
		private static readonly List<UndoAction> actions = new List<UndoAction>();

		// The imprint from the current selected object
		private static TransformImprint? imprint;

		// Is it currently copying stuff?
		private static bool isCopying = false;

		// Is control pressed?
		private static bool control = false;

		// Is shift pressed?
		private static bool shift = false;

		// The mode for the manipulation command
		private static BuilderMode mode = BuilderMode.MOVE;

		// Builds the extra UI content for debugging
		internal static void Build()
		{
			GameObject slots = AmmoSlotUI.Instance.gameObject;
			slots.SetActive(false);

			GameObject builderUI = Instantiate(slots, slots.transform.parent, false);
			slots.SetActive(true);

			builderUI.name = "AreaBuilderUI";
			builderUI.AddComponent<AreaBuilder>();

			DestroyImmediate(builderUI.GetComponent<AmmoSlotUI>());
			builderUI.SetActive(true);

			UI = builderUI.AddComponent<BuilderUI>();
			UI.enabled = true;

			Setup();
		}

		// Sets up the Debug Handler
		internal static void Setup()
		{
			VacPack = SceneContext.Instance.Player.GetComponent<WeaponVacuum>();
			if (VacPack == null)
				VacPack = SceneContext.Instance.Player.GetComponentInChildren<WeaponVacuum>();

			if (VacPack == null)
				VacPack = SceneContext.Instance.Player.GetComponentInParent<WeaponVacuum>();
		}

		// Check if the Target is from builder
		private static bool IsTargetFromBuilder()
		{
			AddedByBuilder comp = Target.GetComponent<AddedByBuilder>();
			if (comp != null)
				return true;

			comp = Target.GetComponentInParent<AddedByBuilder>();
			return comp != null;
		}

		// Gets the Builder Target
		private static GameObject FromBuilder()
		{
			AddedByBuilder comp = Target.GetComponent<AddedByBuilder>();
			if (comp != null)
				return comp.gameObject;

			comp = Target.GetComponentInParent<AddedByBuilder>();
			return comp?.gameObject;
		}

		// The behaviour update function
		private void Update()
		{
			if (!DebugHandler.IsDebugging)
			{
				if (!AmmoSlotUI.Instance.gameObject.activeSelf)
				{
					ObjectExtensions.SetPrivateField(AmmoSlotUI.Instance, "lastSelectedAmmoIndex", -1);
					AmmoSlotUI.Instance.gameObject.SetActive(true);
				}

				if (UI.IsVisible)
					UI.SetVisibility(false);

				if (SelectedObject != null)
					SelectedObject = null;

				return;
			}

			// Key Controls for UI
			if (Input.GetKeyDown(KeyCode.F8))
				IsBuilderOn = !IsBuilderOn;
			
			// Mode Activator
			if (IsBuilderOn)
			{
				// Controls objects presents
				if (AmmoSlotUI.Instance.gameObject.activeSelf)
					AmmoSlotUI.Instance.gameObject.SetActive(false);

				if (!UI.IsVisible)
				{
					UI.lastSelectedIndex = -1;
					UI.SetVisibility(true);
				}

				// Secondary key control
				control = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)
					|| Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand);

				shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

				// Select the UI
				if (Input.GetKeyDown(KeyCode.Alpha1))
					UI.SelectedIndex = 0;

				if (Input.GetKeyDown(KeyCode.Alpha2))
					UI.SelectedIndex = 1;

				if (Input.GetKeyDown(KeyCode.Alpha3))
					UI.SelectedIndex = 2;

				if (Input.GetKeyDown(KeyCode.Alpha4))
					UI.SelectedIndex = 3;

				if (Input.GetKeyDown(KeyCode.Alpha5))
					UI.SelectedIndex = 4;

				if (UI.lastSelectedIndex != UI.SelectedIndex)
				{
					if (isCopying)
						actions.Add(new UndoAction(SelectedObject, true));

					SelectedObject = null;
					isCopying = false;
				}

				// Activate the functions
				if (Input.GetKeyDown(KeyCode.E))
				{
					switch (UI.SelectedIndex)
					{
						case 0: // Manipulate
							ManipulateObject();
							break;
						case 1: // Add Object
							break;
						case 2: // Remove Object
							RemoveObject();
							break;
						case 3: // Dump Target
							if (Target != null)
								Dumper.DumpObject(Target, "FromBuilder");
							break;
						case 4: // Copy Target
							CopyTarget();
							break;
					}
				}

				// Discard action
				if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
				{
					if (isCopying)
					{
						Destroy(SelectedObject, 0.1f);
						SelectedObject = null;
						isCopying = false;
					}
					else
					{
						imprint?.SetTransform(SelectedObject.transform);

						if (SelectedObject.GetComponents<Collider>().Length > 0)
						{
							foreach (Collider col in SelectedObject.GetComponents<Collider>())
								col.enabled = true;
						}

						foreach (Collider col in SelectedObject.GetComponentsInChildren<Collider>())
							col.enabled = true;

						SelectedObject = null;
						imprint = null;
						isCopying = false;
					}
				}

				// Undo Action
				if (control && Input.GetKeyDown(KeyCode.Z))
				{
					if (actions.Count > 0)
					{
						actions[actions.Count - 1].ApplyUndo();
						actions.RemoveAt(actions.Count - 1);
					}
				}

				// Move selected object
				if (SelectedObject != null)
				{
					ControlObject();
				}
				else
				{
					if (!VacPack.enabled)
						VacPack.enabled = true;
				}
			}
			else
			{
				// Controls objects presents
				if (!AmmoSlotUI.Instance.gameObject.activeSelf)
				{
					ObjectExtensions.SetPrivateField(AmmoSlotUI.Instance, "lastSelectedAmmoIndex", -1);
					AmmoSlotUI.Instance.gameObject.SetActive(true);
				}

				if (UI.IsVisible)
					UI.SetVisibility(false);

				SelectedObject = null;
			}
		}

		// Controls the object when selected
		private void ControlObject()
		{
			if (VacPack.enabled)
			{
				SceneContext.Instance.PlayerState.Targeting = null;
				VacPack.enabled = false;
			}

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit[] hits = Physics.RaycastAll(ray, 15f);
			if (hits.Length > 0)
			{
				foreach (RaycastHit hit in hits)
				{
					if (hit.collider.gameObject == SelectedObject)
						continue;

					SelectedObject.transform.position = hit.point;
					break;
				}
			}
			else
				SelectedObject.transform.position = ray.GetPoint(15f);

			float amount = 0.1f;
			if (shift)
				amount = 1f;
			if (control)
				amount = 0.5f;

			if (Input.GetKeyDown(KeyCode.KeypadPlus))
			{
				actions.Add(new UndoAction(SelectedObject.transform));

				Vector3 scale = SelectedObject.transform.localScale;
				scale.x += (mode == BuilderMode.MOVE || mode == BuilderMode.CONTROL_X) ? amount : 0;
				scale.y += (mode == BuilderMode.MOVE || mode == BuilderMode.CONTROL_Y) ? amount : 0;
				scale.z += (mode == BuilderMode.MOVE || mode == BuilderMode.CONTROL_Z) ? amount : 0;
				SelectedObject.transform.localScale = scale;
			}

			if (Input.GetKeyDown(KeyCode.KeypadMinus))
			{
				actions.Add(new UndoAction(SelectedObject.transform));

				Vector3 scale = SelectedObject.transform.localScale;
				scale.x -= (mode == BuilderMode.MOVE || mode == BuilderMode.CONTROL_X) ? amount : 0;
				scale.y -= (mode == BuilderMode.MOVE || mode == BuilderMode.CONTROL_Y) ? amount : 0;
				scale.z -= (mode == BuilderMode.MOVE || mode == BuilderMode.CONTROL_Z) ? amount : 0;
				SelectedObject.transform.localScale = scale;
			}

			if (mode != BuilderMode.MOVE)
			{
				if (Input.GetKey(KeyCode.R))
				{
					float newAmount = amount * 0.1f * Time.deltaTime;

					Vector3 angles = SelectedObject.transform.eulerAngles;
					angles.x += (mode == BuilderMode.CONTROL_X) ? amount : 0;
					angles.y += (mode == BuilderMode.CONTROL_Y) ? amount : 0;
					angles.z += (mode == BuilderMode.CONTROL_Z) ? amount : 0;
					SelectedObject.transform.eulerAngles = angles;
				}
			}

			if (Input.GetKeyDown(KeyCode.Tab))
			{
				int iMode = (int)mode;
				iMode++;

				if (iMode > 3)
					iMode = 0;

				mode = (BuilderMode)iMode;
			}
		}

		// Runs the manipulate object command
		private void ManipulateObject()
		{
			if (SelectedObject == null)
			{
				if (Target == null)
					return;

				imprint = new TransformImprint(Target.transform);
				actions.Add(new UndoAction(Target.transform));
				SelectedObject = Target;

				if (SelectedObject.GetComponents<Collider>().Length > 0)
				{
					foreach (Collider col in SelectedObject.GetComponents<Collider>())
						col.enabled = false;
				}

				foreach (Collider col in SelectedObject.GetComponentsInChildren<Collider>())
					col.enabled = false;				
			}
			else
			{
				if (SelectedObject.GetComponents<Collider>().Length > 0)
				{
					foreach (Collider col in SelectedObject.GetComponents<Collider>())
						col.enabled = true;
				}

				foreach (Collider col in SelectedObject.GetComponentsInChildren<Collider>())
					col.enabled = true;

				SelectedObject = null;
			}
		}

		// Runs the remove object command
		private void RemoveObject()
		{
			if (Target == null)
				return;

			if (IsTargetFromBuilder())
				Destroy(FromBuilder());
			else
			{
				actions.Add(new UndoAction(Target, false));
				Target.SetActive(false);
			}
		}

		// Runs the copy target command
		private void CopyTarget()
		{
			if (SelectedObject == null && !isCopying)
			{
				if (Target == null)
					return;

				SelectedObject = Instantiate(Target, Target.transform.parent);
				SelectedObject.name = SelectedObject.name.Replace("(Clone)", "");
				SelectedObject.AddComponent<AddedByBuilder>();

				if (SelectedObject.GetComponents<Collider>().Length > 0)
				{
					foreach (Collider col in SelectedObject.GetComponents<Collider>())
						col.enabled = false;
				}

				foreach (Collider col in SelectedObject.GetComponentsInChildren<Collider>())
					col.enabled = false;

				isCopying = true;
			}
			else
			{
				actions.Add(new UndoAction(SelectedObject, true));

				if (SelectedObject.GetComponents<Collider>().Length > 0)
				{
					foreach (Collider col in SelectedObject.GetComponents<Collider>())
						col.enabled = true;
				}

				foreach (Collider col in SelectedObject.GetComponentsInChildren<Collider>())
					col.enabled = true;

				SelectedObject = null;

				isCopying = false;
			}
		}

		// The legacy GUI to display generic debug info
		private void OnGUI()
		{
			GUIStyle style = new GUIStyle(GUI.skin.label)
			{
				alignment = TextAnchor.MiddleCenter,
				richText = true
			};

			if (SelectedObject != null)
			{
				GUI.Label(new Rect(0, 20, Screen.width, 30), "<b>Manipulation Mode: </b>" + mode.ToString().Replace("_", ""), style);

				float amount = 0.1f;

				if (shift)
					amount = 1f;

				if (control)
					amount = 0.5f;

				GUI.Label(new Rect(0, 50, Screen.width, 30), "<b>Scale/Rotation Increase: </b>" + amount.ToString() + "(Shift: " + shift.ToString() + "; Control:" + control.ToString() + ")", style);
				GUI.Label(new Rect(0, 80, Screen.width, 30), "Press <b>TAB</b> to change mode", style);
				GUI.Label(new Rect(0, 110, Screen.width, 30), "Press Numpad <b>+ or -</b> to scale up or down", style);

				if (mode != BuilderMode.MOVE)
					GUI.Label(new Rect(0, 140, Screen.width, 30), "Press <b>R</b> to rotate", style);
			}

			GUI.Label(new Rect(0, Screen.height - 210, Screen.width, 30), "Press <b>CTRL + Z</b> to undo actions. Some actions aren't undoable!", style);
			GUI.Label(new Rect(0, Screen.height - 180, Screen.width, 30), "Press <b>Q</b> to discard current action", style);
			switch (UI.SelectedIndex)
			{
				case 0: // Manipulate
					GUI.Label(new Rect(0, Screen.height - 150, Screen.width, 30), "Press <b>E</b> to select targeted object", style);
					break;
				case 1: // Add Object
					break;
				case 2: // Remove Object
					GUI.Label(new Rect(0, Screen.height - 150, Screen.width, 30), "Press <b>E</b> to remove targeted object", style);
					break;
				case 3: // Dump Target
					GUI.Label(new Rect(0, Screen.height - 150, Screen.width, 30), "Press <b>E</b> to dump targeted object", style);
					break;
				case 4: // Copy Target
					if (isCopying)
						GUI.Label(new Rect(0, Screen.height - 150, Screen.width, 30), "Press <b>E</b> to place copied object", style);
					else
						GUI.Label(new Rect(0, Screen.height - 150, Screen.width, 30), "Press <b>E</b> to copy targeted object", style);
					break;
			}
		}

		/// <summary>Tags a object as added by the builder</summary>
		public class AddedByBuilder : MonoBehaviour { }

		/// <summary>The mode for the manipulation command for the builder</summary>
		public enum BuilderMode
		{
			MOVE = 0,
			CONTROL_X = 1,
			CONTROL_Y = 2,
			CONTROL_Z = 3
		}

		/// <summary>An imprint of a Transform Component</summary>
		public struct TransformImprint
		{
			public Vector3 position;
			public Vector3 rotation;
			public Vector3 scale;

			public TransformImprint(Transform trans)
			{
				position = trans.position;
				rotation = trans.eulerAngles;
				scale = trans.localScale;
			}

			public void SetTransform(Transform trans)
			{
				trans.position = position;
				trans.eulerAngles = rotation;
				trans.localScale = scale;
			}
		}

		/// <summary>An undo action for the builder</summary>
		public struct UndoAction
		{
			public GameObject obj;
			public bool builder;
			public TransformImprint? imprint;

			public UndoAction(GameObject obj, bool builder)
			{
				this.obj = obj;
				this.builder = builder;
				imprint = null;
			}

			public UndoAction(Transform trans)
			{
				obj = trans.gameObject;
				builder = false;
				imprint = new TransformImprint(trans);
			}

			public void ApplyUndo()
			{
				if (obj == null)
					return;

				if (imprint == null)
				{
					if (builder)
						Destroy(obj);
					else
						obj.SetActive(true);
				}
				else
				{
					imprint?.SetTransform(obj.transform);
				}
			}
		}
	}
}
