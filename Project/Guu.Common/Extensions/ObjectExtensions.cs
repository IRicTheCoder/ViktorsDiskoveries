using System.Reflection;
using UnityEngine;

/// <summary>
/// Contains extension methods for Objects
/// </summary>
public static class ObjectExtensions
{
	/// <summary>
	/// Sets a value to a private field
	/// </summary>
	/// <typeparam name="T">Type of object</typeparam>
	/// <param name="obj">The object you are setting the value in</param>
	/// <param name="name">The name of the field</param>
	/// <param name="value">The value to set</param>
	public static T SetPrivateField<T>(this T obj, string name, object value)
	{
		try
		{
			FieldInfo field = obj.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
			field?.SetValue(obj, value);
		}
		catch
		{
			// ignored
		}

		return obj;
	}

	/// <summary>
	/// Sets a value to a private property
	/// </summary>
	/// <typeparam name="T">Type of object</typeparam>
	/// <param name="obj">The object you are setting the value in</param>
	/// <param name="name">The name of the property</param>
	/// <param name="value">The value to set</param>
	public static T SetPrivateProperty<T>(this T obj, string name, object value)
	{
		try
		{
			PropertyInfo field = obj.GetType().GetProperty(name, BindingFlags.NonPublic | BindingFlags.Instance);

			if (field == null) return obj;
			
			if (field.CanWrite)
				field.SetValue(obj, value, null);
			else
				return obj.SetPrivateField($"<{name}>k__BackingField", value);
		}
		catch
		{
			// ignored
		}

		return obj;
	}

	/// <summary>
	/// Gets the value of a private field
	/// </summary>
	/// <typeparam name="E">Type of value</typeparam>
	/// <param name="obj">The object to get the value from</param>
	/// <param name="name">The name of the field</param>
	public static E GetPrivateField<E>(this object obj, string name)
	{
		try
		{
			FieldInfo field = obj.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
			return (E)field?.GetValue(obj);
		}
		catch
		{
			// ignored
		}

		return default;
	}

	/// <summary>
	/// Gets the value of a private property
	/// </summary>
	/// <typeparam name="E">Type of value</typeparam>
	/// <param name="obj">The object to get the value from</param>
	/// <param name="name">The name of the property</param>
	public static E GetPrivateProperty<E>(this object obj, string name)
	{
		try
		{
			PropertyInfo field = obj.GetType().GetProperty(name, BindingFlags.NonPublic | BindingFlags.Instance);
			
			if (field == null) return default;
			
			return field.CanRead ? (E)field.GetValue(obj, null) : obj.GetPrivateField<E>($"<{name}>k__BackingField");
		}
		catch
		{
			// ignored
		}

		return default;
	}
}