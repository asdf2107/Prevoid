using Prevoid.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.View
{
    public struct LocatedSymbol
    {
        public Symbol Symbol { get; set; }
        public int ScreenX { get; set; }
        public int ScreenY { get; set; }

        public LocatedSymbol MergeWith(LocatedSymbol next)
        {
            return new LocatedSymbol
            {
                ScreenX = this.ScreenX,
                ScreenY = this.ScreenY,
                Symbol = this.Symbol.MergeWith(next.Symbol),
            };
        }
    }
}
