using UnityEngine;

namespace VikDisk.SRML.Debug
{
	/// <summary>
	/// A marker for toys
	/// </summary>
	public class ToyMarker : Marker, IMarkerTarget<Identifiable>
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
			return GameContext.Instance.LookupDirector.GetToyDefinition(Target.id).Icon.texture;
		}
	}
}
