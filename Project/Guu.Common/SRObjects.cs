using System.Collections.Generic;
using SRML.Utils;
using UnityEngine;

namespace SRML
{
	// TODO: This replaces the BaseObjects class and all its sub classes
	/// <summary>
	/// A class that can access all objects within the game
	/// </summary>
	public static class SRObjects
	{
		/// <summary>The missing icon sprite</summary>
		public static Sprite MissingIcon { get; private set; }

		/// <summary>The missing image sprite</summary>
		public static Sprite MissingImg { get; private set; }
		
		/// <summary>The base largo object</summary>
		public static GameObject BaseLargo { get; internal set; }

		// Static Constructors to load the base objects
		static SRObjects()
		{
			MissingIcon = Get<Sprite>("unknownSmall");
			MissingImg = Get<Sprite>("unknownLarge");

			BaseLargo = PrefabUtils.CopyPrefab(Get<GameObject>("slimePinkTabby"));

			Object.Destroy(BaseLargo.GetComponent<PinkSlimeFoodTypeTracker>());
			Object.Destroy(BaseLargo.GetComponent<StalkConsumable>());
			Object.Destroy(BaseLargo.GetComponent<GatherIdentifiableItems>());
			Object.Destroy(BaseLargo.GetComponent<AttackPlayer>());
		}

		/// <summary>
		/// Gets an object of a type by its name
		/// </summary>
		/// <typeparam name="T">Type to search</typeparam>
		/// <param name="name">Name to search for</param>
		/// <returns>Object found or null if nothing is found</returns>
		public static T Get<T>(string name) where T : Object
		{
			foreach (T found in Resources.FindObjectsOfTypeAll<T>())
			{
				if (found.name.Equals(name))
					return found;
			}

			return null;
		}

		/// <summary>
		/// Gets an object of a type by its name
		/// </summary>
		/// <param name="name">Name to search for</param>
		/// <param name="type">Type to search</param>
		/// <returns>Object found or null if nothing is found</returns>
		public static Object Get(string name, System.Type type)
		{
			foreach (Object found in Resources.FindObjectsOfTypeAll(type))
			{
				if (found.name.Equals(name))
					return found;
			}

			return null;
		}

		/// <summary>
		/// Gets all objects of a type
		/// </summary>
		/// <typeparam name="T">Type to search</typeparam>
		public static List<T> GetAll<T>() where T : Object
		{
			return new List<T>(Resources.FindObjectsOfTypeAll<T>());
		}

		/// <summary>
		/// Gets an instance of an object of a type by its name
		/// </summary>
		/// <typeparam name="T">Type to search</typeparam>
		/// <param name="name">Name to search for</param>
		/// <returns>Instance of object found or null if nothing is found</returns>
		public static T GetInst<T>(string name) where T : Object
		{
			foreach (T found in Resources.FindObjectsOfTypeAll<T>())
			{
				if (found.name.Equals(name))
					return Object.Instantiate(found);
			}

			return null;
		}

		/// <summary>
		/// Gets an instance of an object of a type by its name
		/// </summary>
		/// <param name="name">Name to search for</param>
		/// <param name="type">Type to search</param>
		/// <returns>Instance of object found or null if nothing is found</returns>
		public static Object GetInst(string name, System.Type type)
		{
			foreach (Object found in Resources.FindObjectsOfTypeAll(type))
			{
				if (found.name.Equals(name))
					return Object.Instantiate(found);
			}

			return null;
		}

		/// <summary>
		/// Gets an object of a type by its name in the world
		/// </summary>
		/// <typeparam name="T">Type to search</typeparam>
		/// <param name="name">Name to search for</param>
		/// <returns>Object found or null if nothing is found</returns>
		public static T GetWorld<T>(string name) where T : Object
		{
			foreach (T found in Object.FindObjectsOfType<T>())
			{
				if (found.name.Equals(name))
					return found;
			}

			return null;
		}

		/// <summary>
		/// Gets an object of a type by its name in the world
		/// </summary>
		/// <param name="name">Name to search for</param>
		/// <param name="type">Type to search</param>
		/// <returns>Object found or null if nothing is found</returns>
		public static Object GetWorld(string name, System.Type type)
		{
			foreach (Object found in Object.FindObjectsOfType(type))
			{
				if (found.name.Equals(name))
					return found;
			}

			return null;
		}

		/// <summary>
		/// Gets all objects of a type in the world
		/// </summary>
		/// <typeparam name="T">Type to search</typeparam>
		public static List<T> GetAllWorld<T>() where T : Object
		{
			return new List<T>(Object.FindObjectsOfType<T>());
		}
	}
}
