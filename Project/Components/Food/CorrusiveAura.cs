using System.Collections.Generic;
using UnityEngine;

namespace VikDisk.Components
{
	/// <summary>
	/// Creates a corrusive aura that has a change of rotting objects
	/// </summary>
	public class CorrusiveAura : SRBehaviour
	{
		private const float COOLDOWN = 15f;
		private const float RADIUS = 3f;
		private const int ROTTEN_CHANCE = 30;

		private static readonly List<Identifiable.Id> exceptions = new List<Identifiable.Id>()
		{
			Enums.Identifiables.DARK_GINGER_VEGGIE,
			Identifiable.Id.GINGER_VEGGIE,
			Identifiable.Id.SPICY_TOFU
		};

		private float cooldown = -1;
		private ParticleSystem parts;

		private void Start()
		{
			parts = gameObject.FindChild("CurrPart").GetComponent<ParticleSystem>();
		}

		private void Update()
		{
			if (Time.time < cooldown && cooldown != -1)
				return;

			parts.Play();

			if (Random.Range(0, 100) <= ROTTEN_CHANCE)
			{
				foreach (Collider col in Physics.OverlapSphere(transform.position, RADIUS))
				{
					if (col.gameObject.GetComponent<ResourceCycle>() != null)
					{
						if (exceptions.Contains(col.gameObject.GetComponent<Identifiable>().id))
							continue;

						col.gameObject.GetComponent<ResourceCycle>().ImmediatelyRot();
					}
				}
			}

			cooldown = Time.time + COOLDOWN;
		}
	}
}
