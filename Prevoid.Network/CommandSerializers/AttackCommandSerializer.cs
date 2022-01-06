using Prevoid.Model;
using Prevoid.Model.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prevoid.Network.CommandSerializers
{
    public class AttackCommandSerializer : CommandSerializer
    {
        public AttackCommandSerializer() : base(typeof(AttackCommand), 'a') { }

        public override void Serialize(StringBuilder sb, Command command)
        {
            var attackCommand = (AttackCommand)command;

            Add(sb, Prefix);
            Add(sb, attackCommand.Unit.Id);
            Add(sb, attackCommand.AtX);
            Add(sb, attackCommand.AtY);
            Add(sb, attackCommand.Damage);
            Add(sb, (int)attackCommand.DamageType);
            AddEnd(sb);
        }

        protected override Command Deserialize(string[] parts)
        {
            var unit = GM.Map.GetUnitById(int.Parse(parts[1]));
            var command = new AttackCommand(unit, int.Parse(parts[2]), int.Parse(parts[3]), float.Parse(parts[4]), (DamageType)int.Parse(parts[5]));

            return command;
        }
    }
}
