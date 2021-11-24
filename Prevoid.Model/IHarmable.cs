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
                return Math.Truncate(Hp).ToString()[0];
            }
        }
        Player Player { get; }
        void Harm(float damage);
        void Destroy();
    }
}
