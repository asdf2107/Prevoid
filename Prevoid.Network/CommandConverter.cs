using Prevoid.Model;
using System.Text;
using System.Collections.Generic;
using Prevoid.Network.CommandSerializers;
using System.Linq;
using System;

namespace Prevoid.Network
{
    public static class CommandConverter
    {
        private static readonly Dictionary<Type, CommandSerializer> _CommandSerializersByType = new();
        private static readonly Dictionary<char, CommandSerializer> _CommandSerializersByPrefix = new();

        static CommandConverter()
        {
            AddCommandSerializer(new AttackCommandSerializer());
            AddCommandSerializer(new MoveCommandSerializer());
            AddCommandSerializer(new GenMapCommandSerializer());
        }

        private static void AddCommandSerializer(CommandSerializer commandSerializer)
        {
            if (_CommandSerializersByType.ContainsKey(commandSerializer.CommandType))
                throw new InvalidOperationException($"There already exists a serializer for key '{commandSerializer.CommandType.FullName}'");
            if (_CommandSerializersByPrefix.ContainsKey(commandSerializer.Prefix))
                throw new InvalidOperationException($"There already exists a serializer for key '{commandSerializer.Prefix}'");

            _CommandSerializersByType.Add(commandSerializer.CommandType, commandSerializer);
            _CommandSerializersByPrefix.Add(commandSerializer.Prefix, commandSerializer);
        }

        public static string Serialize(IEnumerable<Command> commands)
        {
            StringBuilder sb = new(CommandSerializer.EndingChar.ToString());

            foreach (var command in commands)
            {
                _CommandSerializersByType[command.GetType()].Serialize(sb, command);
            }

            return sb.ToString();
        }

        public static List<Command> Deserialize(string allCommandsString)
        {
            List<Command> commands = new();
            string[] commandStrings = allCommandsString.Split(CommandSerializer.EndingChar);

            foreach (string commandString in commandStrings)
            {
                if (!string.IsNullOrEmpty(commandString) && commandString[0] != '\0')
                {
                    commands.Add(_CommandSerializersByPrefix[commandString[0]].Deserialize(commandString));
                }
            }

            foreach (var command in commands)
            {
                command.NeedRender = false;
            }

            return commands;
        }
    }
}
