using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Model.Units
{
    public class Tank : Unit
    {
        public Tank(Player player) : base(player, 3, 5, ViewModel.SpriteType.Tank) { }
    }
}
