using Prevoid.Model;
using System.Text;
using System.Collections.Generic;
using Prevoid.Network.CommandSerializers;
using System.Linq;

namespace Prevoid.Network
{
    public static class CommandConverter
    {
        private static readonly CommandSerializer[] _CommandSerializers = new CommandSerializer[]
        {
            new AttackCommandSerializer(),
            new MoveCommandSerializer(),
            new GenMapCommandSerializer(),
        };

        public static string Serialize(IEnumerable<Command> commands)
        {
            StringBuilder sb = new(CommandSerializer.EndingChar.ToString());

            foreach (var command in commands)
            {
                _CommandSerializers.Single(cs => cs.CommandType == command.GetType())
                    .Serialize(sb, command);
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
                    commands.Add(_CommandSerializers.Single(cs => cs.Prefix == commandString[0])
                        .Deserialize(commandString));
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
