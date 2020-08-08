using System.Reflection;
using UnityEngine;
using VikDisk.Components;
using SRML;
using VikDisk.Utils;

namespace VikDisk.Core
{
	/// <summary>
	/// This class runs on the mod's start and when the world
	/// is generated so it can fix certain things in the game.
	/// </summary>
	internal static class GameFixer
	{
		// A list of all the directors for easy access
		private static LookupDirector LookupDirector => GameContext.Instance.LookupDirector;

		// Method that runs when the game starts so it can fix things then
		internal static void FixAtGameLoad()
		{
			// Fixes to the scales
			if (QOL.FixScales)
			{
				// Gilded Ginger Fix
				ScaleFix(Identifiable.Id.GINGER_VEGGIE, Vector3.one * 1.2f, "model_parsnip");
				ObjectFix(Identifiable.Id.GINGER_VEGGIE, (obj) =>
				{
					// TODO: Fix this later
					float perc = 1.2f / 2.1f;

					CapsuleCollider col = obj.GetComponent<CapsuleCollider>();
					SphereCollider sCol = obj.GetComponent<SphereCollider>();

					GameObject launch = obj.FindChild("DelaunchTrigger");
					launch.transform.localScale = launch.transform.localScale * perc;

					col.radius *= perc;
					col.height *= perc;

					sCol.radius *= perc;
				});
			}

			if (QOL.ImpCollisions)
			{
				ObjectFix(SRObjects.Get<GameObject>("ranchPorch"), (obj) =>
				{
					ModLogger.Log("Test");
					foreach (MeshCollider col in obj.GetComponentsInChildren<MeshCollider>())
					{
						col.sharedMesh = col.gameObject.GetComponent<MeshFilter>().sharedMesh;
					}
				});
			}
		}

		// Method that runs when the world is generated so it can fix world things
		internal static void FixAtWorldGen()
		{
			// Fixes to the collisions
			
		}

		// UTILITY METHODS

		// FIXER METHODS
		private static void ScaleFix<T>(T item, Vector3 scale, string name = null)
		{
			if (item is GameObject obj)
				(name == null ? obj.transform : obj.transform.Find(name)).localScale = scale;
			else if (item is Identifiable.Id id)
				(name == null ? LookupDirector.GetPrefab(id).transform : LookupDirector.GetPrefab(id).transform.Find(name)).localScale = scale;
		}

		private static void ComponentFix<T, E>(T item, System.Action<E> action, string name = null)
		{
			if (item is GameObject obj)
				action.Invoke((name == null ? obj : obj.FindChild(name)).GetComponent<E>());
			else if (item is Identifiable.Id id)
				action.Invoke((name == null ? LookupDirector.GetPrefab(id) : LookupDirector.GetPrefab(id).FindChild(name)).GetComponent<E>());
		}

		private static void ObjectFix<T>(T item, System.Action<GameObject> action, string name = null)
		{
			if (item is GameObject obj)
				action.Invoke(name == null ? obj : obj.FindChild(name));
			else if (item is Identifiable.Id id)
				action.Invoke(name == null ? LookupDirector.GetPrefab(id) : LookupDirector.GetPrefab(id).FindChild(name));
		}
	}
}
