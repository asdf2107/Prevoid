using Prevoid.Model.Weapons;

namespace Prevoid.Model.Units
{
    public class Tank : Unit
    {
        public Tank(Player player) : base(player, 3, 5, ViewModel.SpriteType.Tank, 4, new MiddleTankGun()) { }
    }
}
