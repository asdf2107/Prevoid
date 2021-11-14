using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.View
{
    public static class Extensions
    {
        public static List<LocatedSymbol> MergeConsecutive(this IEnumerable<LocatedSymbol> locatedSymbols, int mapWidth)
        {
            var ordered = locatedSymbols.OrderBy(locSymb => locSymb.Y * mapWidth + locSymb.X).ToList();
            var res = new List<LocatedSymbol>();

            for (int i = 0; i < ordered.Count; i++)
            {
                for (int j = i + 1; j < ordered.Count - i; j++)
                {
                    if (ordered[i].Symbol.IsSameForDrawing(ordered[j].Symbol))
                    {
                        ordered[i] = ordered[i].MergeWith(ordered[j]);
                    }
                    else
                    {
                        res.Add(ordered[i]);
                        break;
                    }
                }
            }

            return res;
        }
    }
}
