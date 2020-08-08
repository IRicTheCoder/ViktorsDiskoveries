using UnityEngine;

namespace SRML.Debug
{
	/// <summary>
	/// A marker for identifiables
	/// </summary>
	public class IdentifiableMarker : Marker, IMarkerTarget<Identifiable>
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

		/// <summary>The icon for the marker</summary>
		public override Texture2D GetIcon()
		{
			return ExceptionUtils.IgnoreErrors(() => GameContext.Instance.LookupDirector.GetIcon(Target.id).texture, MarkerController.MissingImage);
		}
	}
}
