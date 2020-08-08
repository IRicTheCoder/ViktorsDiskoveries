using UnityEngine;

namespace VikDisk.Components
{
	/// <summary>
	/// The controller for the glitch plort
	/// </summary>
	public class GlitchedPlort : SRBehaviour
	{
		private const float FADE_OUT_TIME = 10f;
		private const int PERC_TO_GLITCH = 10;

		private bool canGlitch = false;
		private bool hasGlitched = false;

		private float timeToDisappear = 0;

		public static Material GlitchMat { get; private set; }

		private void Start()
		{
			if (GetComponent<PlortInvulnerability>().activateObj.activeInHierarchy)
				canGlitch = true;
		}

		private void Update()
		{
			if (canGlitch && timeToDisappear > -1)
			{
				if (!hasGlitched)
				{
					hasGlitched = true;
					if (Random.Range(1, 100) <= PERC_TO_GLITCH)
					{
						timeToDisappear = Time.time + FADE_OUT_TIME;
						Destroy(GetComponent<Vacuumable>());

						if (GlitchMat == null)
						{
							GlitchMat = GetComponent<MeshRenderer>().sharedMaterial.Copy();
							GlitchMat.SetFloat("_CycleGlitchRatio", 0.1f);
						}

						GetComponent<MeshRenderer>().sharedMaterial = GlitchMat;
					}
				}

				if (timeToDisappear > 0 && Time.time >= timeToDisappear)
				{
					timeToDisappear = -1;
					Destroy(gameObject);
				}
			}
		}
	}
}
