using System;
using System.Reflection;

namespace VikDisk.Utils
{
	/// <summary>
	/// An utility class to help with creating delegates
	/// </summary>
	public static class DelegateUtils
	{
		/// <summary>
		/// Registers a new delegate
		/// </summary>
		/// <typeparam name="T">Type to register in</typeparam>
		/// <typeparam name="E">Type holding the method to delegate</typeparam>
		/// <param name="eventOwner">The owner of the event</param>
		/// <param name="eventName">Name of the event</param>
		/// <param name="methodOwner">The owner of the method to delegate</param>
		/// <param name="methodName">Name of the method to delegate</param>
		public static void Register<T, E>(T eventOwner, string eventName, E methodOwner, string methodName)
		{
			EventInfo ev = eventOwner.GetType().GetEvent(eventName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			MethodInfo addHandler = ev.GetAddMethod();

			Delegate d = Delegate.CreateDelegate(ev.EventHandlerType, methodOwner,
				typeof(E).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance));

			object[] addHandlerArgs = { d };
			addHandler.Invoke(eventOwner, addHandlerArgs);
		}
	}
}
