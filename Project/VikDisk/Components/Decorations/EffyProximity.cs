using UnityEngine;

namespace VikDisk.Components
{
	/// <summary>
	/// Deactivates an Effy renderer if the player isn't close
	/// </summary>
	public class EffyProximity : SRBehaviour
	{
		private const float PROXIMITY_FROM_PLAYER = 7.5f;

		private MeshRenderer render;

		private void Update()
		{
			if (render == null)
				render = gameObject.FindChild("model").GetComponent<MeshRenderer>();

			if (Vector3.Distance(SceneContext.Instance.Player.transform.position, transform.position) > PROXIMITY_FROM_PLAYER && render.enabled)
			{
				render.enabled = false;
				return;
			}

			if (Vector3.Distance(SceneContext.Instance.Player.transform.position, transform.position) <= PROXIMITY_FROM_PLAYER && !render.enabled)
			{
				render.enabled = true;
				return;
			}
		}
	}
}
