using Guu.Utils;

using UnityEngine;

namespace SRML.Debug
{
	/// <summary>
	/// A marker for gadgets
	/// </summary>
	public class GadgetMarker : Marker, IMarkerTarget<Gadget>
	{
		/// <summary>The target for the marker</summary>
		public Gadget Target { get; private set; }

		/// <summary>Sets up this marker</summary>
		public override void Setup()
		{
			Target = gameObject.GetComponent<Gadget>();
			if (Target == null)
				Target = gameObject.GetComponentInChildren<Gadget>();
		}

		/// <summary>The icon for the marker</summary>
		public override Texture2D GetIcon()
		{
			return ExceptionUtils.IgnoreErrors(() => GameContext.Instance.LookupDirector.GetGadgetDefinition(Target.id).icon.texture, MarkerController.MissingImage);;
		}

		/// <summary>Checks if the marker is enabled</summary>
		public override bool IsEnabled()
		{
			return !SceneContext.Instance.PlayerState.InGadgetMode;
		}
	}
}
