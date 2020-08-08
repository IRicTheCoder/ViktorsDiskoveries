using System;
using System.Collections.Generic;

namespace SRML.Modules
{
	/// <summary>
	/// This attribute can be used to identify modules to be loaded by the module manager
	/// </summary>
	public class ModuleEntry : Attribute
	{
		/// <summary>The id for the mod that loads this module</summary>
		internal string modID;

		/// <summary>The module name to load</summary>
		public string moduleName;

		/// <summary>The list of dependencies</summary>
		public Dictionary<string, string> dependencies = new Dictionary<string, string>();

		/// <summary>The id for the mod that loads this module</summary>
		public string ModID => modID;

		/// <summary>
		/// Identifies a new module entry
		/// </summary>
		/// <param name="moduleName">The module name to be loaded</param>
		/// <param name="dependencies">The list of dependencies</param>
		public ModuleEntry(string moduleName, params Tuple<string, string>[] dependencies)
		{
			this.moduleName = moduleName;

			foreach (Tuple<string, string> dependency in dependencies)
				this.dependencies.Add(dependency.Item1, dependency.Item2);
		}
	}
}
