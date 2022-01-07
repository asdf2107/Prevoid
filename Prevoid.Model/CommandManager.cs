using Prevoid.Model.CommandHandlers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Model
{
    public static class CommandManager
    {
        private static readonly Dictionary<Type, CommandHandler> _CommandHandlers = new();
        public static readonly List<Command> TurnCommands = new();

        static CommandManager()
        {
            AddCommandHandler(new MoveCommandHandler());
            AddCommandHandler(new AttackCommandHandler());
            AddCommandHandler(new GenMapCommandHandler());
            AddCommandHandler(new SetUnitCommandHandler());
            
            GM.TurnChanged += () => TurnCommands.Clear();
        }

        private static void AddCommandHandler(CommandHandler commandHandler)
        {
            if (_CommandHandlers.ContainsKey(commandHandler.CommandType))
                throw new InvalidOperationException($"There already exists a handler for key '{commandHandler.CommandType.FullName}'");
            _CommandHandlers.Add(commandHandler.CommandType, commandHandler);
        }

        public static void HandleCommand(Command command)
        {
            _CommandHandlers[command.GetType()].Handle(command);

            TurnCommands.Add(command);
        }
    }
}
