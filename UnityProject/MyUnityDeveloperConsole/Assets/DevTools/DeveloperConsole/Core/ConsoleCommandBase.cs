namespace DevConsole.Core
{
    /// <summary>  
    /// Abstract Base Developer Console Command Class
    /// </summary>
    public abstract class ConsoleCommandBase
    {
        #region Properties

        public abstract string Name { get; protected set; }
        public abstract string Command { get; protected set; }
        public abstract string Description { get; protected set; }
        public abstract string Help { get; protected set; }

        #endregion

        /// <summary>  
        /// Subscribe the command to the console.
        /// </summary>
        protected void AddCommandToConsole()
        {
            DeveloperConsole.AddCommandsToConsole(this);
        }

        /// <summary>  
        /// Runs the command.
        /// </summary>
        public abstract void RunCommand(string[] args);
    }
}