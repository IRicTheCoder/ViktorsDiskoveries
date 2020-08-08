using System.Text;
using MonomiPark.SlimeRancher.Regions;
using TMPro;
using UnityEngine;
using SRML.Areas;

namespace SRML.Debug
{
	/// <summary>
	/// This class binds the Debug system to the
	/// game
	/// </summary>
	public class DebugHandler : MonoBehaviour
	{
		/// <summary>Is the debug mode turned on?</summary>
		public static bool IsDebugging { get; internal set; } = true;

		// This is the last target found by the system
		private static GameObject lastTarget;

		// The Raycast Hit
		private static RaycastHit mainHit;

		/// <summary>The TextMeshPro object that contains the debug text</summary>
		public static TMP_Text DebugText { get; private set; }

		/// <summary>A easy to get value for the player's target</summary>
		public static GameObject Target => SRSingleton<SceneContext>.Instance.PlayerState.Targeting;

		// Check text invisibility
		private static bool IsInvisible => !TargetingUI.Instance.infoText.enabled;

		// Builds the extra UI content for debugging
		public static void Build()
		{
			// Build the Debug Handler
			GameObject info = TargetingUI.Instance.infoText.gameObject;
			GameObject debug = Instantiate(info, info.transform.parent);
			debug.name = "Debug";
			debug.AddComponent<DebugHandler>();

			DebugText = debug.GetComponent<TMP_Text>();
			DebugText.richText = true;
			DebugText.text = string.Empty;

			// Build all required debug things
			AreaBuilder.Build();

			// Setup things
			Setup();
		}

		// Sets up the Debug Handler
		internal static void Setup()
		{
			MarkerController.Setup();
		}

		// The behaviour update function
		private void Update()
		{
			DebugText.enabled = IsDebugging;
			if (!IsDebugging || Target == null)
				return;

			if (lastTarget != Target)
			{
				StringBuilder builder = new StringBuilder($"<size=+10>{Target.name}</size>\nPosition: {Target.transform.position}\n");
				if (!IsInvisible) builder.Insert(0, "\n");

				// Info requiring parent objects
				if (Target.transform.parent != null)
				{
					Region region = Target.GetComponentInParent<Region>();
					if (region != null)
						builder.AppendLine($"<b>Region:</b> {region.GetZoneId()}\n");
				}

				DebugText.text = builder.ToString();
				lastTarget = Target;
			}

			/*DebugMarker marker = Target.GetComponent<DebugMarker>();
			if (marker != null && !IsInvisible)
				marker.SetHover();*/

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Physics.Raycast(ray, out mainHit, 10000f);
		}

		// The legacy GUI to display generic debug info
		private void OnGUI()
		{
			if (!IsDebugging)
				return;

			GUILayout.BeginArea(new Rect(15, 200, 450, Screen.height - 400));

			// Title
			GUILayout.Label("<b>DEBUG MODE ACTIVE</b>");
			GUILayout.Space(5);

			// Vars
			ZoneDirector.Zone zone = SceneContext.Instance.PlayerZoneTracker.GetCurrentZone();

			// Player Info
			GUILayout.Label($"<b>Position: </b>{SceneContext.Instance.Player.transform.position}");
			GUILayout.Label($"<b>Zone: </b>{zone}");
			GUILayout.Label($"<b>Map Unlocked: </b>{SceneContext.Instance.PlayerState.HasUnlockedMap(zone)}");
			GUILayout.Label($"<b>EndGame Time: </b>{SceneContext.Instance.PlayerState.GetEndGameTime()}");
			GUILayout.Label($"<b>Ammo Mode: </b>{SceneContext.Instance.PlayerState.GetAmmoMode()}");
			GUILayout.Space(5);

			// View Raycast
			GUILayout.Label($"<b>Look At: </b>{mainHit.point}");

			GUILayout.EndArea();
		}
	}
}
