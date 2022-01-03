using Prevoid.Model.Weapons;

namespace Prevoid.Model.Units
{
    public class Base : Unit
    {
        public Base(Player player)
            : base(player, "BASE",
                0, 10, ViewModel.SpriteType.Base, 4, new Minigun()) { }
    }
}
