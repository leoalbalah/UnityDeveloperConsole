using System;
using System.Collections.Generic;
using System.Linq;
using DevConsole.Commands;
using DevConsole.Terminal;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DevConsole.Core
{
    /// <summary>  
    /// Handles the console, commands & UI
    /// </summary>
    public class DeveloperConsole : MonoBehaviour
    {
        #region Properties

        private KeyBindings _keyBindings;

        public static DeveloperConsole Instance { get; private set; }
        private static List<ConsoleCommandBase> Commands { get; set; }

        [Header("UI Components")] 
        [SerializeField] private Canvas consoleCanvas;
        [SerializeField] private TextMeshProUGUI consoleText;
        [SerializeField] private TextMeshProUGUI inputText;
        [SerializeField] private TMP_InputField consoleInput;
        [SerializeField] private ConsoleInputHistory consoleInputHistory;

        #endregion

        #region LifeCycle

        private void OnEnable()
        {
            _keyBindings.Enable();
            Application.logMessageReceived += HandleLog;
        }

        private void OnDisable()
        {
            _keyBindings.Disable();
            Application.logMessageReceived -= HandleLog;
        }

        private void Awake()
        {
            _keyBindings = new KeyBindings();

            if (Instance != null)
            {
                return;
            }

            Instance = this;
            Commands = new List<ConsoleCommandBase>();
        }

        private void Start()
        {
            if (consoleInputHistory == null)
            {
                consoleInputHistory = GetComponentInChildren<ConsoleInputHistory>(true);
            }

            consoleCanvas.gameObject.SetActive(false);
            _keyBindings.Default.ToggleDebug.performed += _ => ToggleConsole();
            _keyBindings.Default.EnterCommand.performed += _ => CommandEnter();

            consoleText.text = $"Starting Developer Console ... \n" +
                               $"{Application.productName} v{Application.version}\n" +
                               "--------------------------------------------------------------\n" +
                               "Type <color=orange>commands</color> for list of available commands. \n" +
                               "Type <color=orange><command> -help</color> or <color=orange><command> -h</color> for command details. \n \n \n";
            CreateCommands();
        }

        #endregion

        #region Public Methods

        public void AddMessageToConsole(string msg)
        {
            consoleText.text += msg + "\n";
        }

        public static void AddCommandsToConsole(ConsoleCommandBase command)
        {
            var found = false;
            foreach (var cmnd in Commands)
            {
                if (cmnd.Name.Equals(command.Name)) found = true;
            }

            if (!found)
                Commands.Add(command);
        }

        public static List<ConsoleCommandBase> GetCommands()
        {
            return Commands;
        }

        /// <summary>
        /// Fired when the console is opened or closed. Parameter is true when opening, false when closing.
        /// Subscribe to block game input, pause, etc. when the console is open.
        /// </summary>
        public static event Action<bool> ConsoleVisibilityChanged;

        #endregion

        #region Private Methods

        private void ToggleConsole()
        {
            var isOpening = !consoleCanvas.gameObject.activeInHierarchy;
            consoleCanvas.gameObject.SetActive(isOpening);
            ConsoleVisibilityChanged?.Invoke(isOpening);
            FocusConsole();
        }

        private void FocusConsole()
        {
            EventSystem.current.SetSelectedGameObject(consoleInput.gameObject, null);
            consoleInput.OnPointerClick(new PointerEventData(EventSystem.current));
        }

        private void CommandEnter()
        {
            if (!consoleCanvas.gameObject.activeInHierarchy) return;
            if (inputText.text != "")
            {
                HandleInput(inputText.text);
                consoleInputHistory?.AddToHistory(inputText.text);
            }

            consoleInput.text = "";
            consoleInputHistory?.ResetNavigation();
            FocusConsole();
        }

        public void ClearConsole()
        {
            consoleText.SetText("");
        }

        private void HandleLog(string logMessage, string stackTrace, LogType type)
        {
            var message = type switch
            {
                LogType.Log => $"<color=white>{logMessage}</color>",
                LogType.Error => $"<color=red>{logMessage}</color>",
                LogType.Warning => $"<color=orange>{logMessage}</color>",
                _ => $"<color=grey>{logMessage}</color>"
            };

            AddMessageToConsole(message);
        }

        private void HandleInput(string input)
        {
            var parameters = input.Split(' ');

            if (parameters.Length == 0)
            {
                return;
            }

            foreach (var cmnd in Commands)
            {
                if (cmnd.Command != CodeUtils.CleanString(parameters[0])) continue;

                var args = parameters.ToList();
                args.RemoveAt(0);

                if (args.Count != 0)
                {
                    if (CodeUtils.CleanString(args[0]) == "-help" || CodeUtils.CleanString(args[0]) == "-h")
                    {
                        AddMessageToConsole("=========================");
                        AddMessageToConsole(cmnd.Description);
                        AddMessageToConsole("-------------------------");
                        AddMessageToConsole(cmnd.Help);
                        AddMessageToConsole("=========================");
                        return;
                    }
                }

                cmnd.RunCommand(args.ToArray());
                return;
            }

            AddMessageToConsole($"<color=orange>Command not recognized</color>");
        }

        private static void CreateCommands()
        {
            CommandQuit.CreateCommand();
            CommandEcho.CreateCommand();
            CommandClear.CreateCommand();
            CommandFPS.CreateCommand();
            CommandCommands.CreateCommand();
        }

        #endregion
    }
}