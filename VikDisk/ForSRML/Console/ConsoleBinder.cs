using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Text.RegularExpressions;
using SRML.ConsoleSystem;

namespace SRML.ConsoleSystem
{
	/// <summary>
	/// Binds user commands to the console command menu
	/// </summary>
	public static class ConsoleBinder
	{
		// BINDING FILE
		internal static string bindFile = Path.Combine(Application.persistentDataPath, "SRML/userbinds.bindings");

		/// <summary>
		/// Reads all bindings
		/// </summary>
		internal static void ReadBinds()
		{
			if (!File.Exists(bindFile))
				return;

			foreach (string line in File.ReadAllLines(bindFile))
			{
				if (!line.Contains(":"))
					continue;

				Console.RegisterButton(new ConsoleButton("U: " + line.Substring(0, line.LastIndexOf(":")), line.Substring(line.LastIndexOf(":") + 1)));
			}
		}

		/// <summary>
		/// Registers a new bind
		/// </summary>
		/// <param name="text">The text to show on the button</param>
		/// <param name="command">The command to execute</param>
		public static void RegisterBind(string text, string command)
		{
			Console.RegisterButton(new ConsoleButton("U: " + text, command));
			File.AppendAllText(bindFile, $"{text}:{command}\n");
		}

		/// <summary>
		/// Removes a bind
		/// </summary>
		/// <param name="text">The text of the bind to remove</param>
		/// <returns>True if the bind got removed, false otherwise</returns>
		public static bool RemoveBind(string text)
		{
			int index = -1;
			for (int i = 0; i < Console.cmdButtons.Count ; i++)
			{
				if (Console.cmdButtons[i].Text.Equals("U: " + text))
				{
					index = i;
					break;
				}
			}

			if (index == -1)
				return false;
				
			Console.cmdButtons.RemoveAt(index);
			File.WriteAllText(bindFile, Regex.Replace(File.ReadAllText(bindFile), $@"{text}:.+\n", ""));
			return true;
		}

		/// <summary>
		/// Removes all binds
		/// </summary>
		public static void RemoveAll()
		{
			string[] lines = File.ReadAllLines(bindFile);
			foreach (string line in lines)
			{
				if (!line.Contains(":"))
					continue;

				RemoveBind(line.Substring(0, line.LastIndexOf(":")));
			}
		}

		/// <summary>
		/// Gets all the binds registered
		/// </summary>
		public static List<string> GetAllBinds()
		{
			List<string> result = new List<string>();

			foreach (string line in File.ReadAllLines(bindFile))
			{
				if (!line.Contains(":"))
					continue;

				result.Add(line.Substring(0, line.LastIndexOf(":")));
			}

			return result;
		}
	}
}
