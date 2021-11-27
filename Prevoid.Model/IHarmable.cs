using System;

namespace Prevoid.Model
{
    public interface IHarmable
    {
        float Hp { get; }
        char HpChar
        {
            get
            {
                double value = Math.Truncate(Hp);
                return value >= 10 ? '+' : value.ToString()[0];
            }
        }
        Player Player { get; }
        void Harm(float damage);
        void Destroy();
    }
}
