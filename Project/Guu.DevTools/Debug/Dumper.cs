using UnityEngine;
using System.IO;
using System.Reflection;
using System.Collections;

namespace SRML.Debug
{
	/// <summary>
	/// The dumper is a class that can dump files
	/// straight from debug commands
	/// </summary>
	public static class Dumper
	{
		// The Dump Directory
		private static readonly DirectoryInfo DUMP_DIR = new DirectoryInfo(Application.dataPath + "/../Dumps");

		/// <summary>
		/// Dumps an Unity Object
		/// </summary>
		/// <typeparam name="T">Type of Object</typeparam>
		/// <param name="name">Name of the object to dump</param>
		public static void DumpObject<T>(string name) where T : Object
		{
			T obj = SRObjects.Get<T>(name);

			if (obj == null)
			{
				foreach (T newObj in Object.FindObjectsOfType<T>())
				{
					if (newObj.name.Equals(name))
					{
						obj = newObj;
						break;
					}
				}
			}

			DumpObject(obj);
		}

		/// <summary>
		/// Dumps an Unity Object
		/// </summary>
		/// <typeparam name="T">Type of Object</typeparam>
		/// <param name="obj">The object to dump</param>
		/// <param name="subDir">A sub directory to store them</param>
		public static void DumpObject<T>(T obj, string subDir = null) where T : Object
		{
			if (obj == null)
				return;

			if (!DUMP_DIR.Exists)
				DUMP_DIR.Create();

			FileInfo file = new FileInfo(Path.Combine(DUMP_DIR.FullName, $"{typeof(T).Name}{(subDir != null ? "/" + subDir : string.Empty)}/{obj.name}.txt"));

			if (!file.Directory.Exists)
				file.Directory.Create();

			if (!file.Exists)
				file.Create().Close();

			using (StreamWriter writer = new StreamWriter(file.FullName))
			{
				if (obj is GameObject go)
				{
					writer.WriteLine($"GAME OBJECT: {go.name} - {LayerMask.LayerToName(go.layer)}");
					writer.WriteLine("");

					foreach (Component comp in go.GetComponents<Component>())
					{
						TypeDump(comp.GetType(), writer, comp);
					}

					if (go.transform.childCount > 0)
						ChildDump(go.transform, writer, 5, 0, "  ");
				}
				else if (obj is Shader sha)
				{
					ShaderDump(sha, writer);
				}
				else
					TypeDump(typeof(T), writer, obj);
			}
		}

		/// <summary>
		/// Dumps an Unity Object
		/// </summary>
		/// <param name="name">Name of the object to dump</param>
		/// <param name="type">Type of Object (Needs to be a subclass of Object)</param>
		public static void DumpObject(string name, System.Type type)
		{
			if (!type?.IsSubclassOf(typeof(Object)) ?? false)
				return;

			Object obj = SRObjects.Get(name, type);

			if (obj == null)
			{
				foreach (Object newObj in Object.FindObjectsOfType<Object>())
				{
					if (newObj.name.Equals(name))
					{
						obj = newObj;
						break;
					}
				}
			}

			DumpObject(name, obj);
		}

		/// <summary>
		/// Dumps an Unity Object
		/// </summary>
		/// <param name="name">Name of the object to dump</param>
		/// <param name="obj">The object to dump</param>
		/// <param name="subDir">A sub directory to store them</param>
		public static void DumpObject(string name, Object obj, string subDir = null)
		{
			if (obj == null)
				return;

			if (!DUMP_DIR.Exists)
				DUMP_DIR.Create();

			FileInfo file = new FileInfo(Path.Combine(DUMP_DIR.FullName, $"{obj.GetType().Name}{(subDir != null ? "/" + subDir : string.Empty)}/{name}.txt"));

			if (!file.Directory.Exists)
				file.Directory.Create();

			if (!file.Exists)
				file.Create().Close();

			using (StreamWriter writer = new StreamWriter(file.FullName))
			{
				if (obj is GameObject go)
				{
					writer.WriteLine($"GAME OBJECT: {go.name} - {LayerMask.LayerToName(go.layer)}");
					writer.WriteLine("");

					foreach (Component comp in go.GetComponents<Component>())
					{
						TypeDump(comp.GetType(), writer, comp);
					}

					if (go.transform.childCount > 0)
						ChildDump(go.transform, writer, 5, 0, "  ");
				}
				else if (obj is Shader sha)
				{
					ShaderDump(sha, writer);
				}
				else
					TypeDump(obj.GetType(), writer, obj);
			}
		}

		/// <summary>
		/// Dumps a System Type
		/// </summary>
		/// <param name="name">Name of the object to dump</param>
		/// <param name="obj">The object to dump</param>
		/// <param name="subDir">A sub directory to store them</param>
		public static void DumpType<T>(string name, T obj, string subDir = null)
		{
			if (obj == null)
				return;

			if (!DUMP_DIR.Exists)
				DUMP_DIR.Create();

			FileInfo file = new FileInfo(Path.Combine(DUMP_DIR.FullName, $"{obj.GetType().Name}{(subDir != null ? "/" + subDir : string.Empty)}/{name}.txt"));

			if (!file.Directory.Exists)
				file.Directory.Create();

			if (!file.Exists)
				file.Create().Close();

			using (StreamWriter writer = new StreamWriter(file.FullName))
			{
				if (obj is GameObject go)
				{
					writer.WriteLine($"GAME OBJECT: {go.name} - {LayerMask.LayerToName(go.layer)}");
					writer.WriteLine("");

					foreach (Component comp in go.GetComponents<Component>())
					{
						TypeDump(comp.GetType(), writer, comp);
					}

					if (go.transform.childCount > 0)
						ChildDump(go.transform, writer, 5, 0, "  ");
				}
				else if (obj is Shader sha)
				{
					ShaderDump(sha, writer);
				}
				else
					TypeDump(obj.GetType(), writer, obj);
			}
		}

		// Dumps the children of a Game Object into the stream
		private static void ChildDump(Transform transform, StreamWriter writer, int limit, int count, string sep)
		{
			if (count >= limit || transform == null || writer == null || sep == null)
				return;

			foreach (Transform child in transform)
			{
				writer.WriteLine($"{sep}GAME OBJECT: {child.name} - {LayerMask.LayerToName(child.gameObject.layer)}");
				writer.WriteLine("");

				foreach (Component comp in child.GetComponents<Component>() ?? new Component[0])
				{
					TypeDump(comp.GetType(), writer, comp, sep);
				}

				if (child.childCount > 0)
					ChildDump(child, writer, limit, count++, sep + "  ");
			}
		}

		// Dumps a shader into a stream
		private static void ShaderDump(Shader sha, StreamWriter writer)
		{
			writer.WriteLine($"SHADER: {sha.name}");
			Material mat = new Material(sha);

			foreach (string prop in mat.GetTexturePropertyNames())
			{
				writer.WriteLine($"{prop}");
			}
		}

		// Dumps a type into a stream
		private static void TypeDump(System.Type type, StreamWriter writer, object obj = null, string indent = null)
		{
			if (type == null || writer == null || obj == null)
				return;

			writer.WriteLine($"{indent ?? string.Empty}[{type.Name}]");
			writer.WriteLine("");

			bool hasFields = false;

			foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
			{
				hasFields = true;

				if (field.GetValue(obj) == null)
				{
					writer.WriteLine($"{indent ?? string.Empty}f - {field.Name} - null [{field.FieldType}]");
					continue;
				}

				if (field.FieldType.IsAssignableFrom(typeof(IEnumerable)))
				{
					writer.WriteLine($"{indent ?? string.Empty}f - {field.Name} - {field.GetValue(obj).ToString().Replace("\n", " | ")} [{field.FieldType}]");

					IEnumerable num = field.GetValue(obj) as IEnumerable;
					int i = 0;
					foreach (object child in num)
					{
						writer.WriteLine($"{indent ?? string.Empty}    {i}:{child}");
						i++;
					}
				}
				else if (field.FieldType.IsArray)
				{
					writer.WriteLine($"{indent ?? string.Empty}f - {field.Name} - {field.GetValue(obj).ToString().Replace("\n", " | ")} [{field.FieldType}]");

					System.Array num = field.GetValue(obj) as System.Array;
					int i = 0;
					foreach (object child in num)
					{
						writer.WriteLine($"{indent ?? string.Empty}    {i}:{child}");
						i++;
					}
				}
				else if (field.FieldType.Equals(typeof(Material)))
				{
					Material mat = field.GetValue(obj) as Material;
					writer.WriteLine($"{indent ?? string.Empty}f - {field.Name} - {field.GetValue(obj).ToString().Replace("\n", " | ")} | {mat.shader.name} [{field.FieldType}]");
				}
				else
					writer.WriteLine($"{indent ?? string.Empty}f - {field.Name} - {field.GetValue(obj).ToString().Replace("\n", " | ")} [{field.FieldType}]");
			}

			foreach (FieldInfo field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
			{
				hasFields = true;

				if (field.GetValue(obj) == null)
				{
					writer.WriteLine($"{indent ?? string.Empty}f - {field.Name} - null [{field.FieldType}]");
					continue;
				}

				if (field.FieldType.IsAssignableFrom(typeof(IEnumerable)))
				{
					writer.WriteLine($"{indent ?? string.Empty}f - <{field.Name} - {field.GetValue(obj).ToString().Replace("\n", " | ")} [{field.FieldType}]>");

					IEnumerable num = field.GetValue(obj) as IEnumerable;
					int i = 0;
					foreach (object child in num)
					{
						writer.WriteLine($"{indent ?? string.Empty}    {i}:{child}");
						i++;
					}
				}
				else if (field.FieldType.IsArray)
				{
					writer.WriteLine($"{indent ?? string.Empty}f - {field.Name} - {field.GetValue(obj).ToString().Replace("\n", " | ")} [{field.FieldType}]");

					System.Array num = field.GetValue(obj) as System.Array;
					int i = 0;
					foreach (object child in num)
					{
						writer.WriteLine($"{indent ?? string.Empty}    {i}:{child}");
						i++;
					}
				}
				else if (field.FieldType.Equals(typeof(Material)))
				{
					Material mat = field.GetValue(obj) as Material;
					writer.WriteLine($"{indent ?? string.Empty}f - <{field.Name} - {field.GetValue(obj).ToString().Replace("\n", " | ")} | {mat.shader.name} [{field.FieldType}]>");
				}
				else
					writer.WriteLine($"{indent ?? string.Empty}f - <{field.Name} - {field.GetValue(obj).ToString().Replace("\n", " | ")} [{field.FieldType}]>");
			}

			if (hasFields)
				writer.WriteLine("");

			foreach (PropertyInfo field in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
			{
				try
				{
					if (field.GetValue(obj) == null)
					{
						writer.WriteLine($"{indent ?? string.Empty}f - {field.Name} - null [{field.PropertyType}]");
						continue;
					}

					if (field.PropertyType.IsAssignableFrom(typeof(IEnumerable)))
					{
						writer.WriteLine($"{indent ?? string.Empty}p - {field.Name} - {field.GetValue(obj).ToString().Replace("\n", " | ")} [{field.PropertyType}]");

						IEnumerable num = field.GetValue(obj) as IEnumerable;
						int i = 0;
						foreach (object child in num)
						{
							writer.WriteLine($"{indent ?? string.Empty}    {i}:{child}");
							i++;
						}
					}
					else if (field.PropertyType.IsArray)
					{
						writer.WriteLine($"{indent ?? string.Empty}f - {field.Name} - {field.GetValue(obj).ToString().Replace("\n", " | ")} [{field.PropertyType}]");

						System.Array num = field.GetValue(obj) as System.Array;
						int i = 0;
						foreach (object child in num)
						{
							writer.WriteLine($"{indent ?? string.Empty}    {i}:{child}");
							i++;
						}
					}
					else if (field.PropertyType.Equals(typeof(Material)))
					{
						Material mat = field.GetValue(obj) as Material;
						writer.WriteLine($"{indent ?? string.Empty}p - {field.Name} - {field.GetValue(obj).ToString().Replace("\n", " | ")} | {mat.shader.name} [{field.PropertyType}]");
					}
					else
						writer.WriteLine($"{indent ?? string.Empty}p - {field.Name} - {field.GetValue(obj).ToString().Replace("\n", " | ")} [{field.PropertyType}]");
				}
				catch { continue; }
			}

			foreach (PropertyInfo field in type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance))
			{
				try
				{ 
					if (field.GetValue(obj) == null)
					{
						writer.WriteLine($"{indent ?? string.Empty}f - {field.Name} - null [{field.PropertyType}]");
						continue;
					}

					if (field.PropertyType.IsAssignableFrom(typeof(IEnumerable)))
					{
						writer.WriteLine($"{indent ?? string.Empty}p - <{field.Name} - {field.GetValue(obj).ToString().Replace("\n", " | ")} [{field.PropertyType}]>");

						IEnumerable num = field.GetValue(obj) as IEnumerable;
						int i = 0;
						foreach (object child in num)
						{
							writer.WriteLine($"{indent ?? string.Empty}    {i}:{child}");
							i++;
						}
					}
					else if (field.PropertyType.IsArray)
					{
						writer.WriteLine($"{indent ?? string.Empty}f - {field.Name} - {field.GetValue(obj).ToString().Replace("\n", " | ")} [{field.PropertyType}]");

						System.Array num = field.GetValue(obj) as System.Array;
						int i = 0;
						foreach (object child in num)
						{
							writer.WriteLine($"{indent ?? string.Empty}    {i}:{child}");
							i++;
						}
					}
					else if (field.PropertyType.Equals(typeof(Material)))
					{
						Material mat = field.GetValue(obj) as Material;
						writer.WriteLine($"{indent ?? string.Empty}p - <{field.Name} - {field.GetValue(obj).ToString().Replace("\n", " | ")} | {mat.shader.name} [{field.PropertyType}]>");
					}
					else
						writer.WriteLine($"{indent ?? string.Empty}p - <{field.Name} - {field.GetValue(obj).ToString().Replace("\n", " | ")} [{field.PropertyType}]>");
				}
				catch { continue; }
			}

			writer.WriteLine("");
		}
	}
}
