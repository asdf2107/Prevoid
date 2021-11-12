using System;
using System.Collections.Generic;
using System.Text;

namespace Prevoid.Model
{
    public static class Extensions
    {
        public static float NextFloat(this Random random, float minimum, float maximum)
        {
            return (float)(random.NextDouble() * (maximum - minimum) + minimum);
        }
    }
}
