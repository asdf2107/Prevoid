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
                char res = Math.Round(Hp).ToString()[0];
                return res == '0' ? '1' : res;
            }
        }
        Player Player { get; }
        void Harm(float damage);
        void Destroy();
    }
}
