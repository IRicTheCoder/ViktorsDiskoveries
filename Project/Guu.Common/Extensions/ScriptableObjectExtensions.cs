using System.Reflection;
using UnityEngine;

// TODO: Might be needed
/// <summary>
/// Contains extension methods for Scriptable Objects
/// </summary>
public static class ScriptableObjectExtensions
{
	/// <summary>
	/// Clones the Scriptable Object
	/// </summary>
	public static T Clone<T>(this T obj) where T : ScriptableObject
	{
		T newObj = ScriptableObject.CreateInstance<T>();
		foreach (FieldInfo field in typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance |
		                                                BindingFlags.Public))
		{
			field.SetValue(newObj, field.GetValue(obj));
		}
		
		return newObj;
	}
}