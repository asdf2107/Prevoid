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
        public int InnerWidth { get { return Width - SideInterval * 2; } }
        public int Height { get; private set; }
        public int InnerHeight { get { return Height - VerticalInterval * 2; } }
        public int SideInterval { get; protected set; } = 2;
        public int VerticalInterval { get; protected set; } = 1;
        public BoxCharSet BoxCharSet { get; set; }
        public List<List<Symbol>> InnerText { get; protected set; } = EmptyText;
        protected Symbol _CachedUpperBound;
        protected Symbol _CachedLowerBound;
        protected Symbol _CachedVerticalStart;
        protected Symbol _CachedVerticalEnd;
        protected Symbol _CachedBlankLine;
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
                    ScreenY = Y + VerticalInterval + i,
                    Symbol = _CachedVerticalStart,
                });

                int lineLength = 0;

                for (int j = 0; j < InnerText[i].Count; j++)
                {
                    result.Add(new LocatedSymbol
                    {
                        ScreenX = X + SideInterval + lineLength,
                        ScreenY = Y + VerticalInterval + i,
                        Symbol = InnerText[i][j],
                    });

                    lineLength += InnerText[i][j].Text.Length;
                }

                int leftTillLineEnd = Width - lineLength - SideInterval * 2;

                if (leftTillLineEnd > 0)
                {
                    result.Add(new LocatedSymbol
                    {
                        ScreenX = X + SideInterval + lineLength,
                        ScreenY = Y + VerticalInterval + i,
                        Symbol = Symbol.FromText(new string(' ', leftTillLineEnd)),
                    });
                }

                result.Add(new LocatedSymbol
                {
                    ScreenX = X + Width - 1,
                    ScreenY = Y + VerticalInterval + i,
                    Symbol = _CachedVerticalEnd,
                });
            }

            //Add the blank lines
            for (int i = InnerText.Count + 1; i < Height; i++)
            {
                result.Add(new LocatedSymbol
                {
                    ScreenX = X,
                    ScreenY = Y + i,
                    Symbol = _CachedBlankLine,
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

        protected List<List<Symbol>> GetClearText()
        {
            var symbol = Symbol.FromText(new string(' ', InnerWidth));
            List<List<Symbol>> result = new();

            for (int i = 0; i < Height; i++)
            {
                result.Add(new() { symbol });
            }

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
            _CachedVerticalStart = Symbol.FromText(BoxCharSet.Ver.ToString() + " ");
            _CachedVerticalEnd = Symbol.FromText(" " + BoxCharSet.Ver.ToString());
            _CachedBlankLine = Symbol.FromText(BoxCharSet.Ver.ToString() + new string(' ', Width - 2) + BoxCharSet.Ver.ToString());
        }
    }
}
