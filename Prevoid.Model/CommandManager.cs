using Prevoid.Model.CommandHandlers;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Model
{
    public static class CommandManager
    {
        private static readonly CommandHandler[] _CommandHandlers = new CommandHandler[]
        {
            new MoveCommandHandler(),
            new AttackCommandHandler(),
            new GenMapCommandHandler(),
            new SetUnitCommandHandler(),
        };

        public static List<Command> TurnCommands { get; private set; } = new();

        static CommandManager()
        {
            GM.TurnChanged += () => TurnCommands.Clear();
        }

        public static void HandleCommand(Command command)
        {
            _CommandHandlers.Single(ch => ch.CommandType == command.GetType())
                .Handle(command);

            TurnCommands.Add(command);
        }
    }
}
