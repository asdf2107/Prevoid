using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.View
{
    public struct Symbol
    {
        public ConsoleColor ForeColor { get; set; }
        public ConsoleColor BackColor { get; set; }
        public string Text { get; set; }

        public static Symbol FromText(string text, ConsoleColor foreColor = Constants.BrightTextColor)
        {
            return new Symbol
            {
                ForeColor = foreColor,
                BackColor = Constants.BackgroundColor,
                Text = text,
            };
        }

        public bool IsSameForDrawing(Symbol other)
        {
            return ForeColor == other.ForeColor && BackColor == other.BackColor;
        }

        public Symbol MergeWith(Symbol next)
        {
            return new Symbol
            {
                ForeColor = next.ForeColor,
                BackColor = next.BackColor,
                Text = this.Text + next.Text,
            };
        }

        public Symbol GetTranslucent(ConsoleColor color)
        {
            return new Symbol
            {
                ForeColor = ForeColor,
                BackColor = color,
                Text = Text,
            };
        }
    }
}
