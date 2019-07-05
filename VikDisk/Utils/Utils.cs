using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace VikDisk
{
	/// <summary>
	/// Contains all the utility methods required for this mod
	/// </summary>
	public static class Utils
	{
		/// <summary>
		/// Gets the text from embbeded files in the mod's dll
		/// </summary>
		/// <param name="name">Name of the resource</param>
		/// <returns>The text inside the file</returns>
		public static string GetTextFromEmbbededFile(string name)
		{
			string result = "";

			using (Stream stream = Main.execAssembly.GetManifestResourceStream("VikDisk.Resources." + name))
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					result = reader.ReadToEnd();
				}
			}

			return result;
		}

		/// <summary>
		/// Processes a file that as rules to fix stuff
		/// </summary>
		/// <param name="name">Name of the file</param>
		/// <param name="action">Action to run for each line</param>
		/// <typeparam name="K">The type for the key</typeparam>
		/// <typeparam name="V">The type for the value</typeparam>
		/// <returns>A dictionary with the keys and values processed</returns>
		public static Dictionary<K, V> ProcessFixesFile<K, V>(string name, ModFunc<string, string, KeyValuePair<K, V>?> convert)
		{
			Dictionary<K, V> dict = new Dictionary<K, V>();
			string[] fixesFile = Utils.GetTextFromEmbbededFile($"VikDisk.Resources.Fixes.{name}.fix").Split('\n');

			foreach (string line in fixesFile)
			{
				if (line.Equals(string.Empty) || line.StartsWith("#"))
					continue;

				string[] splited = line.Split('=');
				KeyValuePair<K, V>? pair = convert(splited[0], splited[1]);
				if (pair != null) dict.Add(pair.GetValueOrDefault().Key, pair.GetValueOrDefault().Value);
			}

			return dict;
		}

		/// <summary>
		/// Runs a reflected method
		/// </summary>
		/// <param name="typeName">The name of the type to run in</param>
		/// <param name="methodName">The method to run</param>
		/// <param name="args">The arguments for that method</param>
		/// <returns>True if the method was executed, false otherwise</returns>
		public static bool RunReflectedMethod(string typeName, string methodName, params object[] args)
		{
			Type type = Type.GetType(typeName, false);
			if (type == null)
				return false;

			MethodInfo info;
			Type[] types = new Type[args.Length];
			for (int i = 0; i < args.Length; i++)
				types[i] = args[i].GetType();
			try
			{
				info = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public, null, types, null);
				if (info == null)
				{
					SRML.Console.LogError($"The method 'methodName' couldn't be found");
					return false;
				}

				info?.Invoke(null, args);

				return true;
			}
			catch (Exception e)
			{
				SRML.Console.LogError($"The method 'methodName' generated an exception, found below:");
				UnityEngine.Debug.LogException(e);
				return false;
			}
		}
	}
}
