using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Model.Commands
{
    public class SetUnitCommand : Command
    {
        public Unit Unit { get; private set; }
        public int AtX { get; private set; }
        public int AtY { get; private set; }

        public SetUnitCommand(Unit unit, int atX, int atY)
        {
            Unit = unit;
            AtX = atX;
            AtY = atY;
        }
    }
}
