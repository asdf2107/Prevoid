using System;
using System.Collections.Generic;

namespace Prevoid.ViewModel
{
    public class Overlay
    {
        public event Action<Overlay> Changed;

        public OverlayType Type { get; private set; }
        public SpriteType SpriteType { get; private set; }
        private List<(int, int)> _Fields { get; set; } = new List<(int, int)>();

        public Overlay(OverlayType type, SpriteType spriteType)
        {
            Type = type;
            SpriteType = spriteType;
        }

        public void Add(IEnumerable<(int, int)> coords)
        {
            _Fields.AddRange(coords);
            Changed?.Invoke(this);
        }

        public void Add(int x, int y)
        {
            _Fields.Add((x, y));
            Changed?.Invoke(this);
        }

        public void Remove(int x, int y)
        {
            bool removed = _Fields.Remove((x, y));
            if (removed) Changed?.Invoke(this);
        }

        public IEnumerable<(int, int)> GetFields()
        {
            return _Fields.ToArray();
        }
    }
}
