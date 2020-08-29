using System;
using System.Collections.Generic;
using System.Reflection;
using Guu.API;

namespace VikDisk.Utils
{
	/// <summary>
	/// An utility class to help with registering things
	/// </summary>
	public static class RegistryUtils
	{
		// Used to provide extra modules to the registry process
		internal static List<Assembly> extraModules = new List<Assembly> { Main.execAssembly };
		
		/// <summary>
		/// Registers all items of a type and id
		/// </summary>
		/// <typeparam name="K">ID for the item</typeparam>
		/// <typeparam name="V">Type of the item</typeparam>
		/// <param name="priorities">Priorities to check</param>
		/// <param name="register">The register action to use</param>
		public static void RegisterAll<V>(List<Type> priorities, Action<V> register) where V : RegistryItem<V>
		{
			foreach (Type order in priorities)
			{
				foreach (Assembly ass in extraModules)
				{
					foreach (Type type in ass.GetTypes())
					{
						if (type.IsSubclassOf(order))
						{
							if (type.GetCustomAttributes(typeof(NoRegisterAttribute), false).Length > 0) continue;

							V item = Activator.CreateInstance(type) as V;
							register?.Invoke(item);
						}
					}
				}
			}
		}
	}
}
