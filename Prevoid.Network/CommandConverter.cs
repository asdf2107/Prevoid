using Prevoid.Model;
using System.Text;
using System.Collections.Generic;
using Prevoid.Model.Commands;
using System;

namespace Prevoid.Network
{
    // TODO: Add adequate architecture for command serialization/deserialization
    public static class CommandConverter
    {
        private const char DelimitingChar = ':';
        private const char EndingChar = '@';

        private const char MoveCommandPrefix = 'm';
        private const char AttackCommandPrefix = 'a';
        private const char GenMapCommandPrefix = 'g';

        public static string Serialize(IEnumerable<Command> commands)
        {
            StringBuilder sb = new(EndingChar.ToString());

            foreach (var command in commands)
            {
                if (command is MoveCommand moveCommand)
                {
                    SerializeMoveCommand(sb, moveCommand);
                }
                else if (command is AttackCommand attackCommand)
                {
                    SerializeAttackCommand(sb, attackCommand);
                }
                else if (command is GenMapCommand genMapCommand)
                {
                    SerializeGenMapCommand(sb, genMapCommand);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            return sb.ToString();
        }

        public static List<Command> Deserialize(string allCommandsString)
        {
            List<Command> commands = new();
            string[] commandStrings = allCommandsString.Split(EndingChar);

            foreach (string commandString in commandStrings)
            {
                if (!string.IsNullOrEmpty(commandString))
                {
                    if (commandString[0] == MoveCommandPrefix)
                    {
                        commands.Add(DeserializeMoveCommand(commandString));
                    }
                    else if(commandString[0] == AttackCommandPrefix)
                    {
                        commands.Add(DeserializeAttackCommand(commandString));
                    }
                    else if (commandString[0] == GenMapCommandPrefix)
                    {
                        commands.Add(DeserializeGenMapCommand(commandString));
                    }
                    else if (commandString[0] != '\0')
                    {
                        throw new NotImplementedException();
                    }
                }
            }

            foreach (var command in commands)
            {
                command.NeedRender = false;
            }

            return commands;
        }

        #region Serialization and deserialization of Commands

        static void SerializeMoveCommand(StringBuilder sb, MoveCommand command)
        {
            sb.Add(MoveCommandPrefix);
            sb.Add(command.Unit.Id);
            sb.Add(command.ToX);
            sb.Add(command.ToY);
            sb.AddEnd();
        }

        static MoveCommand DeserializeMoveCommand(string commandString)
        {
            string[] parts = commandString.Split(DelimitingChar);

            var unit = GM.Map.GetUnitById(int.Parse(parts[1]));
            var command = new MoveCommand(unit, int.Parse(parts[2]), int.Parse(parts[3]));

            return command;
        }

        static void SerializeAttackCommand(StringBuilder sb, AttackCommand command)
        {
            sb.Add(AttackCommandPrefix);
            sb.Add(command.Unit.Id);
            sb.Add(command.AtX);
            sb.Add(command.AtY);
            sb.Add(command.Damage);
            sb.Add((int)command.DamageType);
            sb.AddEnd();
        }

        static AttackCommand DeserializeAttackCommand(string commandString)
        {
            string[] parts = commandString.Split(DelimitingChar);

            var unit = GM.Map.GetUnitById(int.Parse(parts[1]));
            var command = new AttackCommand(unit, int.Parse(parts[2]), int.Parse(parts[3]), float.Parse(parts[4]), (DamageType)int.Parse(parts[5]));

            return command;
        }

        static void SerializeGenMapCommand(StringBuilder sb, GenMapCommand command)
        {
            sb.Add(GenMapCommandPrefix);
            sb.Add(command.Seed);
            sb.AddEnd();
        }

        static GenMapCommand DeserializeGenMapCommand(string commandString)
        {
            string[] parts = commandString.Split(DelimitingChar);

            var command = new GenMapCommand(int.Parse(parts[1]));

            return command;
        }

        #endregion

        static void Add(this StringBuilder sb, object value)
        {
            string s = value.ToString();
            if (s.Contains(DelimitingChar) || s.Contains(EndingChar))
                throw new ArgumentException($"Value '{s}' contains one of prohibited values ('{DelimitingChar}', '{EndingChar}')");
            sb.Append($"{s}{DelimitingChar}");
        }

        static void AddEnd(this StringBuilder sb)
        {
            sb.Append(EndingChar);
        }
    }
}
