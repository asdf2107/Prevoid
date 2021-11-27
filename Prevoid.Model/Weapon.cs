namespace Prevoid.Model
{
    public abstract class Weapon
    {
        public virtual string Name { get; }
        public int AttackRange { get; private set; }
        public float Damage { get; private set; }
        public int RoundsPerTurn { get; private set; }
        public int Rounds { get; private set; }
        public DamageType DamageType { get; private set; }

        public Weapon(int attackRange, float damage, int roundsPerTurn, DamageType damageType)
        {
            AttackRange = attackRange;
            Damage = damage;
            RoundsPerTurn = roundsPerTurn;
            DamageType = damageType;

            RefillRounds();
        }

        public void RemoveRound()
        {
            Rounds--;
        }

        public void RefillRounds()
        {
            Rounds = RoundsPerTurn;
        }
    }
}
