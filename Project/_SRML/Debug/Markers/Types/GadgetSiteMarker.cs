using UnityEngine;

namespace VikDisk.SRML.Debug
{
	/// <summary>
	/// A marker for gadget sites
	/// </summary>
	public class GadgetSiteMarker : Marker, IMarkerTarget<GadgetSite>
	{
		/// <summary>The target for the marker</summary>
		public GadgetSite Target { get; private set; }

		/// <summary>Sets up this marker</summary>
		public override void Setup()
		{
			Target = gameObject.GetComponent<GadgetSite>();
			if (Target == null)
				Target = gameObject.GetComponentInChildren<GadgetSite>();
		}

		/// <summary>Draws the Gizmo itself</summary>
		public override void DrawGizmo(Vector2 position)
		{
			if (Target.HasAttached())
			{
				Texture2D secIcon = ExceptionUtils.IgnoreErrors(() => GameContext.Instance.LookupDirector.GetGadgetDefinition(Target.GetAttachedId())?.icon.texture, MarkerController.MissingImage);

				position += (ICON_SIZE / 2);
				Rect rect = new Rect(position, ICON_SIZE / 1.5f);
				GUI.DrawTexture(rect, secIcon);
			}
		}

		/// <summary>The icon for the marker</summary>
		public override Texture2D GetIcon()
		{
			return MarkerController.GadgetSite;
		}

		/// <summary>Checks if the marker is enabled</summary>
		public override bool IsEnabled()
		{
			return SceneContext.Instance.PlayerState.InGadgetMode;
		}
	}
}
