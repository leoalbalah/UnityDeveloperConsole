using System.Collections.Generic;
using System.Linq;
using DevConsole.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DevConsole.Terminal
{
    /// <summary>
    /// Session-wide command history for a console input field.
    /// Use Arrow Up / Arrow Down to cycle through previously entered commands.
    /// No persistence — history is cleared when the application ends.
    /// </summary>
    public class ConsoleInputHistory : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The input field to attach history navigation to.")]
        private TMP_InputField _inputField;

        [SerializeField]
        [Tooltip("Optional: parent canvas or panel. History navigation only works when this is active.")]
        private GameObject _consoleRoot;

        private readonly List<string> _history = new List<string>();
        private int _historyIndex = -1;
        private string _currentDraft = string.Empty;

        private void Awake()
        {
            if (_inputField == null)
            {
                _inputField = GetComponent<TMP_InputField>();
            }
        }

        private void Update()
        {
            if (_inputField == null)
            {
                return;
            }

            if (_consoleRoot != null && !_consoleRoot.activeInHierarchy)
            {
                return;
            }

            var selected = EventSystem.current != null ? EventSystem.current.currentSelectedGameObject : null;
            if (selected != _inputField.gameObject)
            {
                return;
            }

            var keyboard = Keyboard.current;
            if (keyboard == null)
            {
                return;
            }

            if (keyboard.upArrowKey.wasPressedThisFrame)
            {
                NavigateHistory(up: true);
                return;
            }

            if (keyboard.downArrowKey.wasPressedThisFrame)
            {
                NavigateHistory(up: false);
                return;
            }

            if (keyboard.tabKey.wasPressedThisFrame)
            {
                TryCompleteCommand();
            }
        }

        private void TryCompleteCommand()
        {
            var text = _inputField.text ?? string.Empty;
            var firstSpace = text.IndexOf(' ');
            var prefix = firstSpace >= 0 ? text.Substring(0, firstSpace) : text;
            var rest = firstSpace >= 0 ? text.Substring(firstSpace) : string.Empty;

            if (string.IsNullOrEmpty(prefix))
            {
                return;
            }

            var commands = DeveloperConsole.GetCommands();
            if (commands == null || commands.Count == 0)
            {
                return;
            }

            var prefixLower = prefix.ToLowerInvariant();
            var matches = commands
                .Where(c => c != null && !string.IsNullOrEmpty(c.Command) &&
                           c.Command.ToLowerInvariant().StartsWith(prefixLower))
                .Select(c => c.Command)
                .Distinct()
                .ToList();

            if (matches.Count == 0)
            {
                return;
            }

            string completion;
            if (matches.Count == 1)
            {
                completion = matches[0];
            }
            else
            {
                completion = GetLongestCommonPrefix(matches);
                if (completion.Length <= prefix.Length)
                {
                    return;
                }
            }

            _inputField.text = completion + rest;
            _inputField.caretPosition = completion.Length;
            _inputField.stringPosition = completion.Length;
        }

        private static string GetLongestCommonPrefix(List<string> strings)
        {
            if (strings == null || strings.Count == 0) return string.Empty;
            if (strings.Count == 1) return strings[0];

            var first = strings[0];
            var len = first.Length;
            foreach (var s in strings)
            {
                len = Mathf.Min(len, s.Length);
                for (var i = 0; i < len; i++)
                {
                    if (char.ToLowerInvariant(first[i]) != char.ToLowerInvariant(s[i]))
                    {
                        len = i;
                        break;
                    }
                }
            }
            return first.Substring(0, len);
        }

        private void NavigateHistory(bool up)
        {
            if (_history.Count == 0)
            {
                return;
            }

            if (_historyIndex < 0)
            {
                _currentDraft = _inputField.text;
                if (up)
                {
                    _historyIndex = _history.Count - 1;
                    SetInputText(_history[_historyIndex]);
                }
                else
                {
                    _historyIndex = _history.Count;
                }
                return;
            }

            if (up)
            {
                if (_historyIndex > 0)
                {
                    _historyIndex--;
                    SetInputText(_history[_historyIndex]);
                }
            }
            else
            {
                if (_historyIndex < _history.Count - 1)
                {
                    _historyIndex++;
                    SetInputText(_history[_historyIndex]);
                }
                else if (_historyIndex == _history.Count - 1)
                {
                    _historyIndex = -1;
                    SetInputText(_currentDraft);
                }
            }
        }

        private void SetInputText(string text)
        {
            if (_inputField == null) return;

            _inputField.text = text ?? string.Empty;
            _inputField.caretPosition = _inputField.text.Length;
            _inputField.stringPosition = _inputField.text.Length;
        }

        /// <summary>
        /// Call this when a command is submitted to add it to history.
        /// Skips empty or duplicate consecutive entries.
        /// </summary>
        public void AddToHistory(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                return;
            }

            var trimmed = command.Trim();
            if (_history.Count > 0 && _history[_history.Count - 1] == trimmed)
            {
                return;
            }

            _history.Add(trimmed);
            _historyIndex = -1;
            _currentDraft = string.Empty;
        }

        /// <summary>
        /// Resets navigation state when the input field loses focus or is cleared.
        /// Call this when the input is cleared (e.g. after submitting a command).
        /// </summary>
        public void ResetNavigation()
        {
            _historyIndex = -1;
            _currentDraft = string.Empty;
        }

        /// <summary>
        /// Clears all history. Useful when starting a new session or for testing.
        /// </summary>
        public void ClearHistory()
        {
            _history.Clear();
            _historyIndex = -1;
            _currentDraft = string.Empty;
        }
    }
}
