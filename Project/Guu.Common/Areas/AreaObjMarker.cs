using UnityEngine;

namespace SRML.Areas
{
	/// <summary>
	/// An asset pack that contains all assets in a bundle
	/// </summary>
	public class AreaObjMarker : MonoBehaviour
	{
		public enum MarkerType
		{
			Box,
			Sphere,
			Gadget
		}

		public string objName;

		public MarkerType type;

		[Tooltip("Useful for Sphere and Gadget type")]
		public float radius;

		[Tooltip("Useful for Box type")]
		public Vector3 size;

		public Color markerColor;

		public bool runSpawnAction;

		private void OnDrawGizmos()
		{
			Gizmos.color = markerColor;
			switch (type)
			{
				case MarkerType.Gadget:
					Gizmos.DrawWireCube(transform.position, new Vector3(radius, 0.1f, radius));
					Gizmos.color = markerColor * 0.5f;
					Gizmos.DrawCube(transform.position, new Vector3(radius, 0.1f, radius));
					break;
				case MarkerType.Sphere:
					Gizmos.DrawWireSphere(transform.position, radius);
					Gizmos.color = markerColor * 0.5f;
					Gizmos.DrawSphere(transform.position, radius);
					break;
				case MarkerType.Box:
				default:
					Gizmos.DrawWireCube(transform.position, size);
					Gizmos.color = markerColor * 0.5f;
					Gizmos.DrawCube(transform.position, size);
					break;
			}
		}
	}
}
