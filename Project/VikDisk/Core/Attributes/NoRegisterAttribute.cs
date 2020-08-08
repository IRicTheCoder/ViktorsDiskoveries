using System;

namespace VikDisk
{
	/// <summary>
	/// Tags a registry item not to be registered by
	/// my auto registry system
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class NoRegisterAttribute : Attribute { }
}
