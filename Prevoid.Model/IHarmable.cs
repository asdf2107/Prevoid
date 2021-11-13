using System;
using System.Collections.Generic;
using System.Text;

namespace Prevoid.Model
{
    public interface IHarmable
    {
        float Hp { get; }
        void Harm(float damage);
        void Destroy();
    }
}
