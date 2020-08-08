using UnityEngine;

namespace VikDisk.SRML.Debug
{
	/// <summary>
	/// A marker for fashion pods
	/// </summary>
	public class FashionPodMarker : Marker, IMarkerTarget<FashionPod>
	{
		/// <summary>The target for the marker</summary>
		public FashionPod Target { get; private set; }

		/// <summary>Sets up this marker</summary>
		public override void Setup()
		{
			Target = gameObject.GetComponent<FashionPod>();
			if (Target == null)
				Target = gameObject.GetComponentInChildren<FashionPod>();
		}

		/// <summary>The icon for the marker</summary>
		public override Texture2D GetIcon()
		{
			return ExceptionUtils.IgnoreErrors(() => GameContext.Instance.LookupDirector.GetIcon(Target.fashionId).texture, MarkerController.MissingImage);;
		}

		/// <summary>Checks if the marker is enabled</summary>
		public override bool IsEnabled()
		{
			return !SceneContext.Instance.PlayerState.InGadgetMode;
		}
	}
}
