namespace Prevoid.Model.Commands
{
    public class AttackCommand : Command
    {
        public Unit Unit { get; private set; }
        public int AtX { get; private set; }
        public int AtY { get; private set; }
        public float Damage { get; private set; }
        public DamageType DamageType { get; private set; }

        public AttackCommand(Unit unit, int atX, int atY, float damage, DamageType damageType)
        {
            Unit = unit;
            AtX = atX;
            AtY = atY;
            Damage = damage;
            DamageType = damageType;
        }
    }
}
