using System;

namespace Prevoid.Model
{
    public abstract class CommandHandler
    {
        public Type CommandType { get; private set; }

        public CommandHandler(Type commandType)
        {
            if (!typeof(Command).IsAssignableFrom(commandType))
                throw new ArgumentException($"Type {commandType.FullName} can not be used here");
            
            CommandType = commandType;
        }

        public abstract void Handle(Command command);
    }
}
