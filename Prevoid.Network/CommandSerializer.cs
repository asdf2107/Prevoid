using Prevoid.Model;
using System;
using System.Text;

namespace Prevoid.Network
{
    public abstract class CommandSerializer
    {
        public const char DelimitingChar = ':';
        public const char EndingChar = '@';

        public Type CommandType { get; set; }
        public char Prefix { get; private set; }

        public CommandSerializer(Type commandType, char prefix)
        {
            if (!typeof(Command).IsAssignableFrom(commandType))
                throw new ArgumentException($"Type {commandType.FullName} can not be used here");
            
            CommandType = commandType;
            Prefix = prefix;
        }

        public abstract void Serialize(StringBuilder sb, Command command);
        public Command Deserialize(string commandString)
        {
            return Deserialize(commandString.Split(DelimitingChar));
        }
        protected abstract Command Deserialize(string[] parts);

        protected static void Add(StringBuilder sb, object value)
        {
            string s = value.ToString();
            if (s.Contains(DelimitingChar) || s.Contains(EndingChar))
                throw new ArgumentException($"Value '{s}' contains one of prohibited values ('{DelimitingChar}', '{EndingChar}')");
            sb.Append($"{s}{DelimitingChar}");
        }

        protected static void AddEnd(StringBuilder sb)
        {
            sb.Append(EndingChar);
        }
    }
}
