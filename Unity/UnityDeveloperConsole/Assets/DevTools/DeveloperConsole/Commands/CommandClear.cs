using DevConsole.Core;

namespace DevConsole.Commands
{
    /// <summary>  
    /// Clear Developer Console Command
    /// </summary>
    public class CommandClear : ConsoleCommandBase
    {
        #region Properties

        public sealed override string Name { get; protected set; }
        public sealed override string Command { get; protected set; }
        public sealed override string Description { get; protected set; }
        public sealed override string Help { get; protected set; }

        #endregion

        private CommandClear()
        {
            Name = "Clear";
            Command = "clear";
            Description = "Clear the console text";
            Help = "Use this command with no arguments to clear the console.";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            DeveloperConsole.Instance.ClearConsole();
        }

        public static CommandClear CreateCommand()
        {
            return new CommandClear();
        }
    }
}