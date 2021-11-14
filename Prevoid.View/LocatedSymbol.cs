using Prevoid.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.View
{
    public struct LocatedSymbol
    {
        public Symbol Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public LocatedSymbol MergeWith(LocatedSymbol next)
        {
            return new LocatedSymbol
            {
                X = this.X,
                Y = this.Y,
                Symbol = this.Symbol.MergeWith(next.Symbol),
            };
        }
    }
}
