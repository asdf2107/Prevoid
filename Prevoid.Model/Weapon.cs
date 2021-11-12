using System;
using System.Collections.Generic;
using System.Text;

namespace Prevoid.Model
{
    public class Weapon
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
