using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

namespace SRML.ConsoleSystem
{
	/// <summary>
	/// Draws the window for the in-game console
	/// </summary>
	public class ConsoleWindow : MonoBehaviour
	{
		// CONTROL VARIABLES
		internal static bool updateDisplay = false;
		private static bool showWindow = false;
		private static bool focus = false;
		private static bool autoComplete = false;

		// TEXT VARIABLES
		private static readonly string cmdName = "cmdLine";
		private static readonly string openUnityLog = "Unity Log";
		private static readonly string openSRMLLog = "SRML Log";
		private static readonly string commands = "Command Menu";
		private static readonly string acTitle = "Auto Complete";

		// SCROLL VIEWS
		private static Vector2 consoleScroll = Vector2.zero;
		private static Vector2 commandScroll = Vector2.zero;
		private static Vector2 aCompleteScroll = Vector2.zero;

		// TEXTS TO DISPLAY
		internal static string cmdsText = string.Empty;
		internal static string modsText = string.Empty;
		internal static string fullText = string.Empty;
		private static string cmdText = string.Empty;

		// STYLE CONTROL
		private static GUIStyle textArea;
		private static GUIStyle window;
		private static readonly Font consoleFont = Font.CreateDynamicFontFromOSFont(new string[] { "Lucida Console", "Monaco" }, 13);

		// DESIGN VARIABLES
		private static Vector2 oldRes = new Vector2(Screen.width, Screen.height);
		private static Vector2 windowSize = new Vector2(Screen.width + 20, Screen.height / 2);
		private static Vector2 windowPosition = new Vector2(-10, -20);
		private static Rect windowRect = new Rect(windowPosition, windowSize);

		private static Rect leftGroup = new Rect(15, 25, windowRect.width - 190, windowRect.height - 30);
		private static Rect rightGroupA = new Rect(leftGroup.width + 25, leftGroup.y, 150, 70);
		private static Rect rightGroupB = new Rect(leftGroup.width + 25, rightGroupA.y + rightGroupA.height + 5, 150, leftGroup.height - rightGroupA.height - 10);

		private static float cmdLineY = leftGroup.height - 25;
		private static Rect cmdRect = new Rect(0, cmdLineY, leftGroup.width, 20);

		private static GUIContent FullContent => new GUIContent(fullText.ToString());
		private static Vector2 TextSize => textArea?.CalcSize(FullContent) ?? Vector2.zero;

		private static Rect oRect = new Rect(0, 0, leftGroup.width, cmdLineY - 5);
		private static Rect sRect = new Rect(5, 7, oRect.width - 15, cmdLineY - 20);
		private static Rect tRect = new Rect(0, 0, 0, 0);

		private static readonly Rect ulRect = new Rect(10, 7, 130, 25);
		private static readonly Rect slRect = new Rect(10, 37, 130, 25);
		private static readonly Rect cmRect = new Rect(10, 7, 130, 25);

		private static Rect csRect = new Rect(10, 37, rightGroupB.width - 15, rightGroupB.height - 45);
		private static Rect caRect = new Rect(0, 0, csRect.width - 20, (30 * Console.cmdButtons.Count) + 5);

		private static Rect btnRect = new Rect(0, 5, caRect.width - 5, 22);

		// UI RAYCASTERS (TO DISABLE UI INTEREACTION)
		private static GraphicRaycaster[] cachedCasters;

		// HISTORY INDEX
		internal static int currHistory = -1;

		// AUTO COMPLETE - CONTROL VARIABLES
		private static bool justActivated = false;
		private static bool moveCursor = false;
		private static int completeIndex = 0;
		private static string selectComplete = string.Empty;

		private static Rect completeRect = new Rect(3, windowSize.y - 25, 200, 300);

		private static Rect acsRect = new Rect(10, 25, completeRect.width - 15, completeRect.height - 30);
		private static Rect intRect = new Rect(5, 0, acsRect.width - 20, 0);
		private static Rect cBtnRect = new Rect(0, 0, intRect.width - 5, 20);

		private static List<string> cachedAC = new List<string>();
		private static string oldCmdText = null;

		/// <summary>
		/// Attachs the window to a scene
		/// </summary>
		public static void AttachWindow(Scene oldScene, Scene newScene)
		{
			GameObject window = new GameObject("_ConsoleWindow", typeof(ConsoleWindow));
			DontDestroyOnLoad(window);

			SceneManager.activeSceneChanged -= AttachWindow;
			Console.Log("Attached Console Window successfully!");
		}

		// TO ENSURE IT GOT CREATED
		private void Start()
		{
			Console.Log("Console Window running.");
			Console.Log("Use command 'help' for a list of all commands");
			Console.Log("Use command 'mods' for a list of all mods loaded");
			Console.Log("You can also check the menu on the right");

			foreach (SRModInfo info in SRModLoader.LoadedMods)
			{
				modsText += $"{(modsText.Equals(string.Empty) ? "" : "\n")}<color=#8ab7ff>{info.Name}</color> [<color=#8ab7ff>Author:</color> {info.Author} | <color=#8ab7ff>ID:</color> {info.Id} | <color=#8ab7ff>Version:</color> {info.Version}]";
			}
		}

		// TO CHECK FOR RESOLUTON CHANGES
		private void Update()
		{
			if (oldRes.x != Screen.width || oldRes.y != Screen.height)
			{
				oldRes = new Vector2(Screen.width, Screen.height);
				windowSize = new Vector2(Screen.width + 20, Screen.height / 2);
				windowRect = new Rect(windowPosition, windowSize);

				leftGroup = new Rect(15, 25, windowRect.width - 190, windowRect.height - 30);
				rightGroupA = new Rect(leftGroup.width + 25, leftGroup.y, 150, 70);
				rightGroupB = new Rect(leftGroup.width + 25, rightGroupA.y + rightGroupA.height + 5, 150, leftGroup.height - rightGroupA.height - 10);

				cmdLineY = leftGroup.height - 25;
				cmdRect = new Rect(0, cmdLineY, leftGroup.width, 20);

				oRect = new Rect(0, 0, leftGroup.width, cmdLineY - 5);
				sRect = new Rect(5, 7, oRect.width - 15, cmdLineY - 20);

				csRect = new Rect(10, 37, rightGroupB.width - 15, rightGroupB.height - 45);
				caRect = new Rect(0, 0, csRect.width - 20, (30 * Console.cmdButtons.Count) + 5);
			}
		}

		// DRAWS THE WINDOW
		private void OnGUI()
		{
			// UNITY PREVENTS "GUI" STUFF FROM BEING CALLED OUTSIDE "OnGUI"
			if (textArea == null) textArea = new GUIStyle(GUI.skin.label);
			if (window == null) window = new GUIStyle(GUI.skin.window);

			window.active.background = window.normal.background;
			window.hover.background = window.normal.background;
			window.focused.background = window.normal.background;
			window.onActive.background = window.normal.background;
			window.onHover.background = window.normal.background;
			window.onFocused.background = window.normal.background;

			// LISTENS TO MAIN INPUT
			if (Event.current.isKey && Event.current.type == EventType.KeyDown)
			{
				// TOGGLES THE WINDOW
				if ((Event.current.modifiers == EventModifiers.Control || Event.current.modifiers == EventModifiers.Command) && Event.current.keyCode == KeyCode.Tab)
				{
					ToggleWindow();
				}
			}

			if (showWindow)
			{
				Font defFont = GUI.skin.font;
				GUI.skin.font = consoleFont;

				GUI.Window(1234567890, windowRect, DrawWindow, string.Empty, window);
				GUI.BringWindowToFront(1234567890);

				if (autoComplete)
				{
					completeRect.x = 3 + (3 * cmdText.Length);
					GUI.Window(1234567891, completeRect, DrawACWindow, acTitle, window);
					GUI.BringWindowToFront(1234567891);
				}

				GUI.skin.font = defFont;
			}
		}

		private void DrawWindow(int id)
		{
			// LISTENING TO THE KEY EVENTS
			if (Event.current.isKey && Event.current.type == EventType.KeyDown)
			{
				// SUBMITS THE TEXTFIELD
				if (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter)
				{
					Console.ProcessInput(cmdText.TrimEnd(' ').ToLowerInvariant());
					cmdText = string.Empty;

					autoComplete = false;

					Event.current.Use();
					return;
				}

				// AUTO COMPLETE TOGGLE
				if ((Event.current.modifiers == EventModifiers.Control || Event.current.modifiers == EventModifiers.Command) && Event.current.keyCode == KeyCode.Space)
				{
					autoComplete = !autoComplete;
					justActivated = true;

					Event.current.Use();
					return;
				}

				// TAB AUTO COMPLETE
				if (Event.current.keyCode == KeyCode.Tab && Event.current.modifiers == EventModifiers.None && autoComplete)
				{
					if (!selectComplete.Equals(string.Empty))
					{
						cmdText += selectComplete;
						selectComplete = string.Empty;
						moveCursor = true;

						autoComplete = false;
					}

					Event.current.Use();
					return;
				}

				// CYCLES HISTORY UP
				if (Event.current.keyCode == KeyCode.UpArrow && !autoComplete)
				{
					if (currHistory == -1)
					{
						cmdText = Console.history[Console.history.Count - 1];
						currHistory = Console.history.Count - 1;
					}
					else
					{
						if (currHistory > 0)
						{
							cmdText = Console.history[currHistory - 1];
							currHistory -= 1;
						}
					}

					Event.current.Use();
					return;
				}
				else if (Event.current.keyCode == KeyCode.UpArrow && autoComplete)
				{
					completeIndex--;

					if (completeIndex < 0)
						completeIndex = cachedAC.Count - 1;

					Event.current.Use();
					return;
				}

				// CYCLES HISTORY DOWN
				if (Event.current.keyCode == KeyCode.DownArrow && !autoComplete)
				{
					if (currHistory != -1)
					{
						if (currHistory < (Console.history.Count - 1))
						{
							cmdText = Console.history[currHistory + 1];
							currHistory += 1;
						}
						else
						{
							cmdText = string.Empty;
							currHistory = -1;
						}
					}

					Event.current.Use();
					return;
				}
				else if (Event.current.keyCode == KeyCode.DownArrow && autoComplete)
				{
					completeIndex++;

					if (completeIndex > cachedAC.Count - 1)
						completeIndex = 0;

					Event.current.Use();
					return;
				}

				// TRIGGER AUTO COMPLETE
				if (Event.current.keyCode == KeyCode.None && !cmdText.Equals(oldCmdText) && Event.current.modifiers == EventModifiers.None)
				{
					autoComplete = true;
					Event.current.Use();
				}
			}

			if (cmdText.EndsWith(" ") && justActivated)
			{
				cmdText = cmdText.TrimEnd(' ');
				oldCmdText = null;
				justActivated = false;
			}

			// CONSOLE AREA
			GUI.BeginGroup(leftGroup);

			GUI.SetNextControlName(cmdName);
			cmdText = GUI.TextField(cmdRect, cmdText);

			if (!focus)
			{
				focus = true;
				GUI.FocusControl(cmdName);
			}

			textArea.wordWrap = true;
			textArea.clipping = TextClipping.Clip;
			textArea.richText = true;
			textArea.padding = new RectOffset(5, 5, 5, 5);

			tRect.width = TextSize.x;
			tRect.height = TextSize.y;

			GUI.BeginGroup(oRect, GUI.skin.textArea);
			consoleScroll = GUI.BeginScrollView(sRect, consoleScroll, tRect, false, true);
			GUI.Label(tRect, FullContent, textArea);
			GUI.EndScrollView();
			GUI.EndGroup();

			GUI.EndGroup();

			// MENU AREA
			GUI.BeginGroup(rightGroupA, GUI.skin.textArea);

			if (GUI.Button(ulRect, openUnityLog))
				System.Diagnostics.Process.Start(Console.unityLogFile);

			if (GUI.Button(slRect, openSRMLLog))
				System.Diagnostics.Process.Start(Console.srmlLogFile);

			GUI.EndGroup();

			GUI.BeginGroup(rightGroupB, GUI.skin.textArea);

			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.Label(cmRect, commands);
			GUI.skin.label.alignment = TextAnchor.MiddleLeft;

			caRect.height = (30 * Console.cmdButtons.Count) + 5;
			commandScroll = GUI.BeginScrollView(csRect, commandScroll, caRect, false, true);
			GUI.BeginGroup(caRect);

			int y = 5;
			foreach (ConsoleButton button in Console.cmdButtons)
			{
				btnRect.y = y;
				if (GUI.Button(btnRect, button.Text))
					Console.ProcessInput(button.Command.TrimEnd(' ').ToLowerInvariant(), true);

				y += 30;
			}

			GUI.EndGroup();
			GUI.EndScrollView();

			GUI.EndGroup();

			// UPDATES THE SCROLL POSITION FOR THE CONSOLE TO SHOW LATEST MESSAGES
			if (updateDisplay)
			{
				consoleScroll.y = textArea.CalcSize(new GUIContent(fullText.ToString())).y;
				updateDisplay = false;
			}

			// MOVES THE CURSOR AFTER AUTO COMPLETE
			if (moveCursor)
			{
				TextEditor txt = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
				txt.MoveCursorToPosition(Vector2.one * 50000);

				autoComplete = false;
				moveCursor = false;
			}
		}

		private void DrawACWindow(int id)
		{
			bool spaces = cmdText.Contains(" ");

			intRect.height = (25 * cachedAC.Count) + 5;
			aCompleteScroll = GUI.BeginScrollView(acsRect, aCompleteScroll, intRect, false, true);
			GUI.BeginGroup(intRect);

			Texture2D bg = GUI.skin.textField.normal.background;
			GUI.skin.textField.normal.background = GUI.skin.button.active.background;
			GUI.skin.textField.richText = true;

			if (!spaces)
			{
				if (!cmdText.Equals(oldCmdText))
				{
					cachedAC.RemoveAll(s => true); // .Clear() gets broken sometimes when you dynamically load an Assembly, do the same another way
					foreach (string cmd in Console.commands.Keys)
					{
						if (cmd.StartsWith(cmdText.ToLowerInvariant()))
							cachedAC.Add(cmd);
					}

					oldCmdText = cmdText;
				}

				selectComplete = cachedAC[completeIndex].Substring(cmdText.Length);

				int y = 5;
				for (int i = 0; i < cachedAC.Count; i++)
				{
					GUI.backgroundColor = Color.white;
					if (completeIndex == i)
						GUI.backgroundColor = Color.red;

					cBtnRect.y = y;
					if (GUI.Button(cBtnRect, $"<b>{cmdText.ToLowerInvariant()}</b>{cachedAC[i].Substring(cmdText.Length)}", GUI.skin.textField))
					{
						cmdText += cachedAC[i].Substring(cmdText.Length);
						moveCursor = true;

						autoComplete = false;
					}

					y += 25;
				}
			}
			else
			{
				string[] args = cmdText.ToLowerInvariant().Split(' ');

				int count = args.Length - 1;
				string lastArg = args[count];

				List<string> autoC = Console.commands[args[0]].GetAutoComplete(count - 1, lastArg);

				if (autoC == null)
					autoComplete = false;

				if (!cmdText.Equals(oldCmdText))
				{
					cachedAC.RemoveAll(s => true); // .Clear() gets broken sometimes when you dynamically load an Assembly, do the same another way
					foreach (string cmd in autoC)
					{
						if (cmd.StartsWith(lastArg))
							cachedAC.Add(cmd.ToLowerInvariant());
					}

					oldCmdText = cmdText;
				}

				selectComplete = cachedAC[completeIndex].Substring(lastArg.Length);

				int y = 5;
				for (int i = 0; i < cachedAC.Count; i++)
				{
					GUI.backgroundColor = Color.white;
					if (completeIndex == i)
						GUI.backgroundColor = Color.red;

					cBtnRect.y = y;
					if (GUI.Button(cBtnRect, $"<b>{lastArg}</b>{cachedAC[i].Substring(lastArg.Length)}", GUI.skin.textField))
					{
						cmdText += cachedAC[i].Substring(lastArg.Length);
						moveCursor = true;

						autoComplete = false;
					}

					y += 25;
				}
			}

			GUI.skin.textField.richText = false;
			GUI.skin.textField.normal.background = bg;

			GUI.EndGroup();
			GUI.EndScrollView();
		}

		private void ToggleWindow()
		{
			showWindow = !showWindow;

			if (showWindow)
			{
				focus = false;
				if (SceneManager.GetActiveScene().name.Equals("worldGenerated"))
				{
					if (!SceneContext.Instance.TimeDirector.HasPauser())
						SceneContext.Instance.TimeDirector.Pause(true);
				}

				cachedCasters = FindObjectsOfType<GraphicRaycaster>();
				foreach (GraphicRaycaster caster in cachedCasters)
				{
					caster.enabled = false;
				}
			}
			else
			{
				if (SceneManager.GetActiveScene().name.Equals("worldGenerated"))
				{
					if (SceneContext.Instance.TimeDirector.HasPauser())
						SceneContext.Instance.TimeDirector.Unpause(true);
				}

				foreach (GraphicRaycaster caster in cachedCasters)
				{
					caster.enabled = true;
				}
				cachedCasters = null;
			}
		}
	}
}
