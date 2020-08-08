using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static VikDisk.SRML.Debug.MarkerMesh;

namespace VikDisk.SRML.Debug
{
	/// <summary>
	/// The base marker class
	/// <para>Markers are runtime gizmos</para>
	/// </summary>
	public abstract class Marker : MonoBehaviour
	{
		/// <summary>The limit distance to render the marker</summary>
		public static readonly float LIMIT_DISTANCE = 150f;

		/// <summary>The size of the Marker's Icon</summary>
		public static readonly Vector2 ICON_SIZE = new Vector2(48, 48);

		/// <summary>The size of the Marker's Label</summary>
		public static readonly Vector2 LABEL_SIZE = new Vector2(300, 20);

		// Is the Marker set up?
		private bool setupDone = false;

		/// <summary>The GUI Style for the Marker label</summary>
		public static GUIStyle LabelStyle { get; private set; }

		/// <summary>The material for this marker</summary>
		public virtual MarkerMaterial Material { get; } = MarkerMaterial.Generic;

		/// <summary>The extra height to move the icon</summary>
		public virtual float ExtraHeight { get; } = 0;

		/// <summary>Is the marker visible?</summary>
		public bool IsVisible { get; private set; } = false;

		/// <summary>The distance of the marker</summary>
		public float Distance { get; private set; } = 0f;



		/// <summary>Sets up this marker</summary>
		public abstract void Setup();

		/// <summary>Draws the Gizmo itself</summary>
		public virtual void DrawGizmo(Vector2 position) { }

		/// <summary>The icon for the marker</summary>
		public abstract Texture2D GetIcon();

		/// <summary>Checks if the marker is enabled</summary>
		public virtual bool IsEnabled() { return true; }

		// Updates the distance
		private void Update()
		{
			if (!IsEnabled())
				return;

			Distance = Vector3.Distance(transform.position, Camera.main.transform.position);
			CheckVisibility();
		}

		// Uses the Legacy GUI to draw gizmo info
		private void OnGUI()
		{
			if (!IsEnabled())
				return;

			if (LabelStyle == null)
			{
				LabelStyle = new GUIStyle(GUI.skin.label)
				{
					alignment = TextAnchor.UpperCenter,
					richText = true
				};
			}

			if (!IsVisible || !DebugHandler.IsDebugging || !MarkerController.ShowMarkers || SceneContext.Instance.TimeDirector.HasPauser())
				return;

			if (!setupDone)
			{
				Setup();
				setupDone = true;
			}

			Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
			pos.y = (Screen.height - pos.y) + ExtraHeight;
			pos -= (ICON_SIZE / 2);

			Rect rect = new Rect(pos, ICON_SIZE);
			GUI.DrawTexture(rect, GetIcon() ?? MarkerController.MissingImage);

			DrawGizmo(pos);
		}

		// Checks the visibility of the marker
		private void CheckVisibility()
		{
			Vector3 view = Camera.main.WorldToViewportPoint(transform.position);
			if (view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1 || view.z <= 0 || Distance > LIMIT_DISTANCE)
			{
				IsVisible = false;
				return;
			}

			IsVisible = true;
		}

		/*GameObject mark = GameObject.CreatePrimitive(PrimitiveType.Cube);

		mark.name = "Point Marker";
		mark.transform.parent = markerComp.transform;
		mark.transform.localScale = Vector3.one * 3f;
		MarkerMesh mesh = mark.AddComponent<MarkerMesh>();
		mesh.Setup(Material);*/
	}
}
