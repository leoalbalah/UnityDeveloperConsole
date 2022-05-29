using UnityEngine;
using DevConsole.Core;

namespace DevConsole.Commands
{
    /// <summary>  
    /// FPS Developer Console Command
    /// </summary>
    public class CommandFPS : ConsoleCommandBase
    {
        #region Properties

        public sealed override string Name { get; protected set; }
        public sealed override string Command { get; protected set; }
        public sealed override string Description { get; protected set; }
        public sealed override string Help { get; protected set; }

        #endregion

        private CommandFPS()
        {
            Name = "FPS";
            Command = "fps";
            Description = "Display a fps counter on screen.";
            Help = "Options: \n" +
                   "fps <state> where <state> is the bool that controls if the counter most be shown.";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            if (args.Length != 0)
            {
                var arg = CodeUtils.CleanString(args[0]);

                if (arg != "" & arg != " ")
                {
                    var counter = GameObject.FindWithTag("FPSCounter").GetComponent<FPSCounter.FPSCounter>();

                    switch (arg)
                    {
                        case "true":
                        case "1":
                            counter.Enable();
                            break;
                        case "false":
                        case "0":
                            counter.Disable();
                            break;
                        default:
                            Debug.LogWarning("Please pass a correct state");
                            break;
                    }

                    return;
                }
            }

            Debug.LogWarning("Please pass a state");
        }

        public static CommandFPS CreateCommand()
        {
            return new CommandFPS();
        }
    }
}