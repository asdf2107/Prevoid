using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Model.Weapons
{
    public class Minigun : Weapon
    {
        public override string Name => "Minigun";

        public Minigun() : base(4, 0.4f, 3, DamageType.Point) { }
    }
}
