using UnityEngine;
using DevConsole.Core;

namespace DevConsole.Commands
{
    /// <summary>  
    /// Quit Developer Console Command
    /// </summary>
    public class CommandQuit : ConsoleCommandBase
    {
        #region Properties

        public sealed override string Name { get; protected set; }
        public sealed override string Command { get; protected set; }
        public sealed override string Description { get; protected set; }
        public sealed override string Help { get; protected set; }

        #endregion

        private CommandQuit()
        {
            Name = "Quit";
            Command = "quit";
            Description = "Quits the application";
            Help = "Use this command with no arguments to force the application to exit!";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            if (Application.isEditor)
            {
#if UNITY_EDITOR

                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
            else
            {
                Application.Quit();
            }
        }

        public static CommandQuit CreateCommand()
        {
            return new CommandQuit();
        }
    }
}