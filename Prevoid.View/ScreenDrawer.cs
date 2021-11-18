using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.View
{
    public class ScreenDrawer
    {
        private readonly ConcurrentQueue<LocatedSymbol> _DrawQueue = new ConcurrentQueue<LocatedSymbol>();

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
                {
                    bool success = _DrawQueue.TryDequeue(out LocatedSymbol locatedSymbol);
                    if (success) PerformDraw(locatedSymbol);
                }
            }
        }

        private void PerformDraw(LocatedSymbol located)
        {
            if (Console.ForegroundColor != located.Symbol.ForeColor && !string.IsNullOrEmpty(located.Symbol.Text))
                Console.ForegroundColor = located.Symbol.ForeColor;

            if (Console.BackgroundColor != located.Symbol.BackColor)
                Console.BackgroundColor = located.Symbol.BackColor;

            if (Console.CursorLeft != located.ScreenX - 1 || Console.CursorTop != located.ScreenY)
                Console.SetCursorPosition(located.ScreenX, located.ScreenY);

            Console.Write(located.Symbol.Text);
        }
    }
}
