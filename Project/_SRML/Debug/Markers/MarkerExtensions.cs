using UnityEngine;

namespace VikDisk.SRML.Debug
{
	public static class MarkerExtensions
	{
		/// <summary>
		/// Installs the marker into an object
		/// </summary>
		/// <param name="obj">The obj being marked</param>
		/// <typeparam name="T">The type of marker being added</typeparam>
		public static void InstallMarker<T>(this GameObject obj) where T : Marker
		{
			if (obj.GetComponent<Marker>() == null)
				obj.AddComponent<T>();
		}
	}
}
