using Prevoid.Model.Weapons;

namespace Prevoid.Model.Units
{
    public class Tank : Unit
    {
        public Tank(Player player, string nickname = null) 
            : base(player, string.IsNullOrEmpty(nickname) ? "TANK" : $"TANK {nickname}",
                  3, 5, ViewModel.SpriteType.Tank, 4, new MiddleTankGun()) { }
    }
}
