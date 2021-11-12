using System;
using System.Collections.Generic;

namespace Prevoid.ViewModel
{
    public class Overlay
    {
        public ConsoleColor Color { get; set; }
        public SpriteType SpriteType { get; set; }
        private List<(int, int)> _Fields { get; set; } = new List<(int, int)>();

        public void Add(int x, int y)
        {
            _Fields.Add((x, y));
        }

        public void Remove(int x, int y)
        {
            _Fields.Remove((x, y)); // !
        }

        public IEnumerable<(int, int)> GetFields()
        {
            return _Fields.ToArray();
        }
    }
}
