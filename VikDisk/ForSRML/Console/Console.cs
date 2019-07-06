using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Text.RegularExpressions;
using SRML.ConsoleSystem;

namespace SRML
{
	/// <summary>
	/// Controls the in-game console
	/// </summary>
	public class Console : ILogHandler
	{
		// CONFIGURE SOME NUMBERS
		public const int MAX_ENTRIES = 200; // MAX ENTRIES TO SHOW ON CONSOLE
		public const int HISTORY = 10; // NUMBER OF COMMANDS TO KEEP ON HISTORY

		// LOG STUFF
		internal static string unityLogFile = Path.Combine(Application.persistentDataPath, "output_log.txt");
		internal static string srmlLogFile = Path.Combine(Application.persistentDataPath, "SRML/srml.log");
		private static readonly ILogHandler unityHandler = Debug.unityLogger.logHandler;
		private static readonly Console console = new Console();

		// COMMAND STUFF
		internal static Dictionary<string, ConsoleCommand> commands = new Dictionary<string, ConsoleCommand>();
		internal static List<ConsoleButton> cmdButtons = new List<ConsoleButton>();

		// LINE COUNTER
		private static int lines = 0;

		// COMMAND HISTORY
		internal static List<string> history = new List<string>(HISTORY);

		// RELOAD EVENT (THIS IS CALLED WHEN THE COMMAND RELOAD IS CALLED, USED TO RUN A RELOAD METHOD FOR A MOD, IF THE AUTHOR WISHES TO CREATE ONE)
		public delegate void ReloadAction(); // Creates the delegate here to prevent 'TypeNotFound' exceptions
		public static event ReloadAction Reload;

		// DUMP ACTIONS
		// KEY = Dump Command Argument; VALUE = The method to run
		public delegate void DumpAction(StreamWriter writer);
		internal static Dictionary<string, DumpAction> dumpActions = new Dictionary<string, DumpAction>();

		// COMMAND CATCHER
		public delegate bool CommandCatcher(string cmd, string[] args);
		internal static List<CommandCatcher> catchers = new List<CommandCatcher>();

		/// <summary>
		/// Initializes the console
		/// </summary>
		public static void Init()
		{
			Debug.unityLogger.logHandler = console;

			if (!Directory.Exists(srmlLogFile.Substring(0, srmlLogFile.LastIndexOf('/'))))
				Directory.CreateDirectory(srmlLogFile.Substring(0, srmlLogFile.LastIndexOf('/')));

			if (File.Exists(srmlLogFile))
				File.Delete(srmlLogFile);

			File.Create(srmlLogFile).Close();

			Log("CONSOLE INITIALIZED!");
			Log("Patching SceneManager to attach window");

			RegisterCommand(new Commands.ClearCommand());
			RegisterCommand(new Commands.HelpCommand());
			RegisterCommand(new Commands.ReloadCommand());
			RegisterCommand(new Commands.ModsCommand());
			RegisterCommand(new Commands.DumpCommand());
			RegisterCommand(new Commands.AddButtonCommand());
			RegisterCommand(new Commands.RemoveButtonCommand());
			RegisterCommand(new Commands.EditButtonCommand());

			RegisterButton(new ConsoleButton("Clear", "clear"));
			RegisterButton(new ConsoleButton("Help", "help"));
			RegisterButton(new ConsoleButton("Mods", "mods"));
			RegisterButton(new ConsoleButton("Reload", "reload"));
			RegisterButton(new ConsoleButton("Dump All", "dump all"));

			ConsoleBinder.ReadBinds();
			SceneManager.activeSceneChanged += ConsoleWindow.AttachWindow;
		}

		/// <summary>
		/// Registers a new command into the console
		/// </summary>
		/// <param name="cmd">Command to register</param>
		public static void RegisterCommand(ConsoleCommand cmd)
		{
			if (commands.ContainsKey(cmd.ID.ToLowerInvariant()))
			{
				LogWarning($"Trying to register command with id '{cmd.ID.ToLowerInvariant()}' but the ID is already registered!");
				return;
			}

			commands.Add(cmd.ID.ToLowerInvariant(), cmd);
			ConsoleWindow.cmdsText += $"{(ConsoleWindow.cmdsText.Equals(string.Empty) ? "" : "\n")}<color=#8ab7ff>{cmd.Usage}</color> - {cmd.Description}";
		}

		/// <summary>
		/// Registers a new console button
		/// </summary>
		/// <param name="button">Button to register</param>
		public static void RegisterButton(ConsoleButton button)
		{
			cmdButtons.Add(button);
		}

		/// <summary>
		/// Registers a new dump action for the dump command
		/// </summary>
		/// <param name="id">The id to use for the dump command argument</param>
		/// <param name="action">The dump action to run</param>
		public static void RegisterDumpAction(string id, DumpAction action)
		{
			dumpActions.Add(id.ToLowerInvariant(), action);
		}

		/// <summary>
		/// Registers a command catcher which allows commands to be processed and their execution controlled by outside methods
		/// </summary>
		/// <param name="catcher">The method to catch the commands</param>
		public static void RegisterCommandCatcher(CommandCatcher catcher)
		{
			catchers.Add(catcher);
		}

		/// <summary>
		/// Logs a info message
		/// </summary>
		/// <param name="message">Message to log</param>
		/// <param name="logToFile">Should log to file?</param>
		public static void Log(string message, bool logToFile = true)
		{
			unityHandler.LogFormat(LogType.Log, null, Regex.Replace(message, @"\<[a-z=]+\>|\<\/[a-z]+\>", ""), string.Empty);
			console.LogEntry(LogType.Log, message, logToFile);
		}

		/// <summary>
		/// Logs a warning message
		/// </summary>
		/// <param name="message">Message to log</param>
		/// <param name="logToFile">Should log to file?</param>
		public static void LogWarning(string message, bool logToFile = true)
		{
			unityHandler.LogFormat(LogType.Warning, null, Regex.Replace(message, @"\<[a-z=]+\>|\<\/[a-z]+\>", ""), string.Empty);
			console.LogEntry(LogType.Warning, message, logToFile);
		}

		/// <summary>
		/// Logs an error message
		/// </summary>
		/// <param name="message">Message to log</param>
		/// <param name="logToFile">Should log to file?</param>
		public static void LogError(string message, bool logToFile = true)
		{
			unityHandler.LogFormat(LogType.Error, null, Regex.Replace(message, @"\<[a-z=]+\>|\<\/[a-z]+\>", ""), string.Empty);
			console.LogEntry(LogType.Error, message, logToFile);
		}

		// PROCESSES THE TEXT FROM THE CONSOLE INPUT
		internal static void ProcessInput(string command, bool forced = false)
		{
			if (command.Equals(string.Empty))
				return;

			if (!forced)
			{
				if (history.Count == HISTORY)
					history.RemoveAt(0);

				history.Add(command);
			}

			try
			{
				Log("<color=cyan>CMD: </color>" + command);

				bool spaces = command.Contains(" ");
				string cmd = spaces ? command.Substring(0, command.IndexOf(' ')) : command;

				if (commands.ContainsKey(cmd))
				{
					bool keepExecution = true;
					string[] args = spaces ? StripArgs(command) : null;

					foreach (CommandCatcher catcher in catchers)
					{
						keepExecution = catcher.Invoke(cmd, args);

						if (!keepExecution)
							break;
					}

					if (keepExecution)
						commands[cmd].Execute(args);
				}
				else
				{
					LogError("Unknown command. Please use 'help' for available commands or check the menu on the right");
				}
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
		}

		private static string[] StripArgs(string command)
		{
			MatchCollection result = Regex.Matches(command.Substring(command.IndexOf(' ')+1), "\\w+|\'[^']+\'|\"[^\"]+\"");
			List<string> args = new List<string>(result.Count);

			foreach (Match match in result)
				args.Add(Regex.Replace(match.Value, "'|\"", "").ToLowerInvariant());

			return args.ToArray();
		}

		// CONVERTS LOG TYPE TO A SMALLER MORE READABLE TYPE
		private string TypeToText(LogType logType)
		{
			if (logType == LogType.Error || logType == LogType.Exception)
				return "ERRO";

			return logType == LogType.Warning ? "WARN" : "INFO";
		}

		// LOGS A NEW ENTRY
		private void LogEntry(LogType logType, string message, bool logToFile)
		{
			string type = TypeToText(logType);
			string color = "white";
			if (type.Equals("ERRO")) color = "#ff7070";
			if (type.Equals("WARN")) color = "yellow";

			if (lines == MAX_ENTRIES)
				ConsoleWindow.fullText = ConsoleWindow.fullText.Substring(ConsoleWindow.fullText.IndexOf('\n'));
			else
				lines++;

			ConsoleWindow.fullText += $"{(ConsoleWindow.fullText.Equals(string.Empty) ? "" : "\n")}<color=cyan>[{DateTime.Now.ToString("HH:mm:ss")}]</color><color={color}>[{type}] {Regex.Replace(message, @"<material[^>]*>|<\/material>|<size[^>]*>|<\/size>|<quad[^>]*>|<b>|</b>", "")}</color>";

			if (logToFile)
				FileLogger.LogEntry(logType, message);

			ConsoleWindow.updateDisplay = true;
		}

		// RUNS THE RELOAD COMMAND
		internal static void ReloadMods()
		{
			Reload?.Invoke();
		}

		// THE TWO FOLLOWING METHODS CAN BE IGNORED, THEY TAP INTO UNITY'S SYSTEM TO LISTEN TO THE LOG
		void ILogHandler.LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
		{
			unityHandler.LogFormat(logType, context, Regex.Replace(format, @"\<[a-z=]+\>|\<\/[a-z]+\>", ""), args);
			LogEntry(logType, Regex.Replace(string.Format(format, args), @"\[INFO]\s|\[ERROR]\s|\[WARNING]\s", ""), true);
		}

		void ILogHandler.LogException(Exception exception, UnityEngine.Object context)
		{
			System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(exception, true);

			unityHandler.LogException(exception, context);
			LogEntry(LogType.Exception, exception.Message + "\n" + trace.ToString(), true);
		}
	}
}
