using UnityEngine;

namespace SRML.Debug
{
	/// <summary>
	/// A marker for crates
	/// </summary>
	public class CrateMarker : Marker, IMarkerTarget<Identifiable>
	{
		/// <summary>The target for the marker</summary>
		public Identifiable Target { get; private set; }

		/// <summary>Sets up this marker</summary>
		public override void Setup()
		{
			Target = gameObject.GetComponent<Identifiable>();
			if (Target == null)
				Target = gameObject.GetComponentInChildren<Identifiable>();
		}

		/// <summary>Draws the Gizmo itself</summary>
		public override void DrawGizmo(Vector2 position)
		{
			Texture2D secIcon = GameContext.Instance.LookupDirector.GetIcon(Identifiable.Id.STAR_ORNAMENT).texture;

			position += (ICON_SIZE / 2);
			Rect rect = new Rect(position, ICON_SIZE / 1.5f);
			GUI.DrawTexture(rect, secIcon);

			secIcon = GameContext.Instance.LookupDirector.GetIcon(Identifiable.Id.HUNTER_ORNAMENT).texture;

			position += new Vector2(-ICON_SIZE.x / 2, 0);
			rect = new Rect(position, ICON_SIZE / 1.5f);
			GUI.DrawTexture(rect, secIcon);
		}

		/// <summary>The icon for the marker</summary>
		public override Texture2D GetIcon()
		{
			return GameContext.Instance.LookupDirector.GetIcon(Identifiable.Id.PINK_ORNAMENT).texture;
		}
	}
}
