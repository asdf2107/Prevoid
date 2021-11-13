using System;

namespace Prevoid.Model.EffectType
{
    class DamageDebuffEffect : Effect
    {
        public float DebuffAmount { get; set; }

        public DamageDebuffEffect(float debuffAmount) : base()
        {
            DebuffAmount = debuffAmount;
        }

        public override void MergeWith(Effect effect)
        {
            if (!(effect is DamageDebuffEffect other)) throw new ArgumentException();

            DebuffAmount = other.DebuffAmount > DebuffAmount ? other.DebuffAmount : DebuffAmount;
            Duration = other.Duration > Duration ? other.Duration : Duration;
        }
    }
}
