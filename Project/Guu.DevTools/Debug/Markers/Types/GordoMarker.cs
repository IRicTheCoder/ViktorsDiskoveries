using UnityEngine;
using SRML.Registries;

namespace SRML.Debug
{
	/// <summary>
	/// A marker for gordos
	/// </summary>
	public class GordoMarker : Marker, IMarkerTarget<GordoIdentifiable>
	{
		/// <summary>The target for the marker</summary>
		public GordoIdentifiable Target { get; private set; }

		/// <summary>Sets up this marker</summary>
		public override void Setup()
		{
			Target = gameObject.GetComponent<GordoIdentifiable>();
			if (Target == null)
				Target = gameObject.GetComponentInChildren<GordoIdentifiable>();
		}

		/// <summary>The icon for the marker</summary>
		public override Texture2D GetIcon()
		{
			return GordoRegistry.GetGordoIcon(Target.id)?.texture;
		}
	}
}
