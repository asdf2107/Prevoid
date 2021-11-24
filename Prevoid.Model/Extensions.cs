using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Model
{
    public static class Extensions
    {
        public static float NextFloat(this Random random, float minimum, float maximum)
        {
            return (float)(random.NextDouble() * (maximum - minimum) + minimum);
        }

        public static IEnumerable<(int, int)> GetCoords(this IEnumerable<(int, int, int)> coordsWithDist)
        {
            return coordsWithDist.Select(c => (c.Item1, c.Item2));
        }
    }
}
