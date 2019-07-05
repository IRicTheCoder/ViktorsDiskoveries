﻿namespace SRML.ConsoleSystem
{
	/// <summary>
	/// A button for the console menu
	/// </summary>
	public struct ConsoleButton
	{
		/// <summary>
		/// Text to display on the button
		/// </summary>
		public string Text { get; private set; }

		/// <summary>
		/// Command to execute when the button is pressed
		/// </summary>
		public string Command { get; private set; }

		/// <summary>
		/// Creates a new console button button
		/// </summary>
		/// <param name="text">Text to display on the button</param>
		/// <param name="command">Command to execute when the button is pressed</param>
		public ConsoleButton(string text, string command)
		{
			Text = text;
			Command = command;
		}
	}
}
