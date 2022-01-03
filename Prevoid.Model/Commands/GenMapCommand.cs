using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Model.Commands
{
    public class GenMapCommand : Command
    {
        public int Seed { get; set; }

        public GenMapCommand(int seed)
        {
            Seed = seed;
        }
    }
}
