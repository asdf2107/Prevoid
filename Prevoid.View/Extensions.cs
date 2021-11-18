using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.View
{
    public static class Extensions
    {
        public static List<LocatedSymbol> MergeConsecutive(this IEnumerable<LocatedSymbol> locatedSymbols)
        {
            var ordered = locatedSymbols.OrderBy(locSymb => locSymb.ScreenX).ToList();
            var res = new List<LocatedSymbol>();

            int currentHead = 0;
            bool headMerged = false;

            for (int i = 1; i < ordered.Count; i++)
            {
                if (ordered[currentHead].Symbol.IsSameForDrawing(ordered[i].Symbol))
                {
                    ordered[currentHead] = ordered[currentHead].MergeWith(ordered[i]);
                    headMerged = true;
                }
                else
                {
                    res.Add(ordered[currentHead]);
                    currentHead = i++;
                    headMerged = false;
                }
            }

            if (!headMerged) res.Add(ordered[currentHead]);

            return res;
        }
    }
}
