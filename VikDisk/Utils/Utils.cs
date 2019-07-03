using System.IO;
using System;

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
	}
}
