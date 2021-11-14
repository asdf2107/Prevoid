namespace Prevoid.Model
{
    public abstract class Weapon
    {
        public int AttackRange { get; protected set; }
        public float Damage { get; protected set; }
        public DamageType DamageType { get; protected set; }

        public Weapon(int attackRange, float damage, DamageType damageType)
        {
            AttackRange = attackRange;
            Damage = damage;
            DamageType = damageType;
        }
    }
}
