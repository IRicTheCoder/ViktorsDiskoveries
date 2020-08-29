using System;
using System.Collections.Generic;

namespace Guu.Modules
{
	/// <summary>
	/// This attribute can be used to identify modules to be loaded by the module manager
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class ModuleEntry : Attribute
	{
		/// <summary>The id for the mod that loads this module</summary>
		public readonly string modID;

		/// <summary>The module name to load</summary>
		public readonly string moduleName;

		/// <summary>The list of dependencies</summary>
		public readonly Dictionary<string, string> dependencies = new Dictionary<string, string>();

		/// <summary>
		/// Identifies a new module entry
		/// </summary>
		/// <param name="modID">The id of the mod this module belongs to</param>
		/// <param name="moduleName">The module name to be loaded</param>
		/// <param name="dependencies">The list of dependencies</param>
		public ModuleEntry(string modID, string moduleName, params string[] dependencies)
		{
			this.modID = modID;
			this.moduleName = moduleName;

			foreach (string dependency in dependencies)
			{
				string[] split = dependency.Split(';');
				this.dependencies.Add(split[0], split[1]);
			}
		}
	}
}
