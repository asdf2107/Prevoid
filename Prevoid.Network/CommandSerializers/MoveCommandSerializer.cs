using Prevoid.Model;
using Prevoid.Model.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prevoid.Network.CommandSerializers
{
    public class MoveCommandSerializer : CommandSerializer
    {
        public MoveCommandSerializer() : base(typeof(MoveCommand), 'm') { }

        public override void Serialize(StringBuilder sb, Command command)
        {
            var moveCommand = (MoveCommand)command;

            Add(sb, Prefix);
            Add(sb, moveCommand.Unit.Id);
            Add(sb, moveCommand.ToX);
            Add(sb, moveCommand.ToY);
            AddEnd(sb);
        }

        protected override Command Deserialize(string[] parts)
        {
            var unit = GM.Map.GetUnitById(int.Parse(parts[1]));
            var command = new MoveCommand(unit, int.Parse(parts[2]), int.Parse(parts[3]));

            return command;
        }
    }
}
