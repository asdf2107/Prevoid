using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.View
{
    public class ScreenDrawer
    {
        private readonly Queue<LocatedSymbol> _DrawQueue = new Queue<LocatedSymbol>();

        public void Draw(LocatedSymbol locatedSymbol)
        {
            _DrawQueue.Enqueue(locatedSymbol);
        }

        public void Draw(IEnumerable<LocatedSymbol> locatedSymbols)
        {
            foreach (var locatedSymbol in locatedSymbols)
            {
                _DrawQueue.Enqueue(locatedSymbol);
            }
        }

        public void DrawLoop()
        {
            while (true)
            {
                if (_DrawQueue.Count > 0) 
                    PerformDraw(_DrawQueue.Dequeue());
            }
        }

        private void PerformDraw(LocatedSymbol located)
        {
            if (Console.ForegroundColor != located.Symbol.ForeColor && !string.IsNullOrEmpty(located.Symbol.Text))
                Console.ForegroundColor = located.Symbol.ForeColor;

            if (Console.BackgroundColor != located.Symbol.BackColor)
                Console.BackgroundColor = located.Symbol.BackColor;

            if (Console.CursorLeft != located.X - 1 || Console.CursorTop != located.Y)
                Console.SetCursorPosition(located.X, located.Y);

            Console.Write(located.Symbol.Text);
        }
    }
}
