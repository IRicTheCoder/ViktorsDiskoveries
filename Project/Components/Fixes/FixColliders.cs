using UnityEngine;

namespace VikDisk.Components
{
	/// <summary>
	/// Fixes the colliders of certain objects
	/// </summary>
	public class FixColliders : SRBehaviour
	{
		private bool fixApplied = false;

		public void FixCollision()
		{
			foreach (MeshFilter filter in gameObject.GetComponentsInChildren<MeshFilter>())
			{
				MeshCollider col = filter.gameObject.AddComponent<MeshCollider>();
				col.sharedMesh = filter.sharedMesh;
				col.convex = true;
			}
		}

		private void Update()
		{
			if (fixApplied)
				return;

			FixCollision();
			fixApplied = true;
		}
	}
}
