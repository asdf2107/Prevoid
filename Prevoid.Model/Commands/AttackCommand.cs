using System;
using System.Collections.Generic;
using System.Text;

namespace Prevoid.Model.Commands
{
    public class AttackCommand : Command
    {
        public Unit Unit { get; set; }
        public int AtX { get; set; }
        public int AtY { get; set; }
        public float Damage { get; set; }
        public DamageType DamageType { get; set; }
    }
}
