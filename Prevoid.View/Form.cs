using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prevoid.View
{
    public abstract class Form
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public BoxCharSet BoxCharSet { get; set; }
        public List<List<Symbol>> InnerText { get; protected set; } = EmptyText;
        protected Symbol _CachedUpperBound;
        protected Symbol _CachedLowerBound;
        public static readonly List<List<Symbol>> EmptyText = new();

        public Form(int x, int y, int width, int height, BoxCharSet boxCharSet)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            BoxCharSet = boxCharSet;

            Recache();
        }

        public abstract void SetInnerText();

        public virtual IEnumerable<LocatedSymbol> GetRenderView()
        {
            List<LocatedSymbol> result = new();

            result.Add(new LocatedSymbol
            {
                ScreenX = X,
                ScreenY = Y,
                Symbol = _CachedUpperBound,
            });

            for (int i = 0; i < InnerText.Count; i++)
            {
                result.Add(new LocatedSymbol
                {
                    ScreenX = X,
                    ScreenY = Y + i + 1,
                    Symbol = InnerText[i][0], // !!!
                });
            }

            result.Add(new LocatedSymbol
            {
                ScreenX = X,
                ScreenY = Y + Height,
                Symbol = _CachedLowerBound,
            });

            return result;
        }

        public virtual void Clear()
        {
            InnerText.Clear();
        }

        protected virtual void Recache()
        {
            StringBuilder sb = new();
            for (int i = 0; i < Width - 2; i++)
            {
                sb.Append(BoxCharSet.Hor);
            }

            string horBar = sb.ToString();

            _CachedUpperBound = Symbol.FromText(BoxCharSet.SE + horBar + BoxCharSet.SW);
            _CachedLowerBound = Symbol.FromText(BoxCharSet.NE + horBar + BoxCharSet.NW);
        }
    }
}
