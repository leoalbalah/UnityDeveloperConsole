using System.Linq;
using UnityEngine;
using DevConsole.Core;

namespace DevConsole.Commands
{
    /// <summary>  
    /// Echo Developer Console Command
    /// </summary>
    public class CommandEcho : ConsoleCommandBase
    {
        #region Properties

        public sealed override string Name { get; protected set; }
        public sealed override string Command { get; protected set; }
        public sealed override string Description { get; protected set; }
        public sealed override string Help { get; protected set; }

        #endregion

        private CommandEcho()
        {
            Name = "Echo";
            Command = "echo";
            Description = "Prints a message in the console.";
            Help = "Options: \n" +
                   "echo <message> where <message> is the message to be printed.";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            if (args.Length != 0)
            {
                switch (args.Length)
                {
                    case 1:
                    {
                        var message = CodeUtils.CleanString(args[0]);

                        if (message != "" & message != " ")
                        {
                            Debug.Log(message);
                            return;
                        }

                        break;
                    }
                    case > 1:
                    {
                        var message = args.Aggregate("", (current, arg) => current + (arg + " "));

                        Debug.Log(message);
                        return;
                    }
                }
            }

            Debug.LogWarning("Please pass a message");
        }

        public static CommandEcho CreateCommand()
        {
            return new CommandEcho();
        }
    }
}