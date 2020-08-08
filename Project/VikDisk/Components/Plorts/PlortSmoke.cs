using UnityEngine;

namespace VikDisk.Components
{
	/// <summary>
	/// Controls the smoke of the plort with this component
	/// </summary>
	public class PlortSmoke : SRBehaviour
	{
		private float maxCooldown;
		private float cooldown = -1;
		private ParticleSystem parts;

		public void SetMaxCooldown(float max)
		{
			maxCooldown = max;
		}

		private void Start()
		{
			parts = gameObject.FindChild("SmokePart").GetComponent<ParticleSystem>();
		}

		private void Update()
		{
			if (Time.time < cooldown && cooldown != -1)
				return;

			parts.Play();
			cooldown = Time.time + maxCooldown;
		}
	}
}
