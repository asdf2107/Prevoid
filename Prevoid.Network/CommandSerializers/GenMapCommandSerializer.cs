using Prevoid.Model;
using Prevoid.Model.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prevoid.Network.CommandSerializers
{
    public class GenMapCommandSerializer : CommandSerializer
    {
        public GenMapCommandSerializer() : base(typeof(GenMapCommand), 'g') { }

        public override void Serialize(StringBuilder sb, Command command)
        {
            var genMapCommand = (GenMapCommand)command;

            Add(sb, Prefix);
            Add(sb, genMapCommand.Seed);
            AddEnd(sb);
        }

        protected override Command Deserialize(string[] parts)
        {
            var command = new GenMapCommand(int.Parse(parts[1]));

            return command;
        }
    }
}
