using DevConsole.Core;
using UnityEngine;

namespace DevConsole.Commands
{
    /// <summary>  
    /// Commands Developer Console Command
    /// </summary>
    public class CommandCommands : ConsoleCommandBase
    {
        #region Properties

        public sealed override string Name { get; protected set; }
        public sealed override string Command { get; protected set; }
        public sealed override string Description { get; protected set; }
        public sealed override string Help { get; protected set; }

        #endregion

        private CommandCommands()
        {
            Name = "Commands";
            Command = "commands";
            Description = "Prints all available commands in the console.";
            Help = "Takes no arguments and prints all commands registered in the console";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            var devConsole = GameObject.FindWithTag("DeveloperConsole").GetComponent<DeveloperConsole>();
            foreach (var command in DeveloperConsole.GetCommands())
            {
                devConsole.AddMessageToConsole(command.Command + " ----- " + command.Description);
            }
        }

        public static CommandCommands CreateCommand()
        {
            return new CommandCommands();
        }
    }
}