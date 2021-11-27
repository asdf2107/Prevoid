namespace Prevoid.Model.Weapons
{
    public class MiddleTankGun : Weapon
    {
        public override string Name => "MTG";

        public MiddleTankGun() : base(4, 1.5f, 1, DamageType.Point) { }
    }
}
