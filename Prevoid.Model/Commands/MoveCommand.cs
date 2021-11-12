using System;
using System.Collections.Generic;
using System.Text;

namespace Prevoid.Model.Commands
{
    public class MoveCommand : Command
    {
        public Unit Unit { get; set; }
        public int ToX { get; set; }
        public int ToY { get; set; }
    }
}
