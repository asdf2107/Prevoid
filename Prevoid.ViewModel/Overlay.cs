using System;
using System.Collections.Generic;

namespace Prevoid.ViewModel
{
    public class Overlay
    {
        public event Action<Overlay> ShownChanged;
        public event Action<Overlay> Hidden;

        public OverlayType Type { get; private set; }
        private readonly List<(int, int)> _Fields = new List<(int, int)>();

        public Overlay(OverlayType type)
        {
            Type = type;
        }

        public IEnumerable<(int, int)> GetFields()
        {
            return _Fields.ToArray();
        }

        public void Add(IEnumerable<(int, int)> coords)
        {
            _Fields.AddRange(coords);
            ShownChanged?.Invoke(this);
        }

        public void Add(int x, int y)
        {
            _Fields.Add((x, y));
            ShownChanged?.Invoke(this);
        }

        public void Remove(int x, int y)
        {
            bool removed = _Fields.Remove((x, y));
            if (removed) ShownChanged?.Invoke(this);
        }

        public void Clear()
        {
            Hidden?.Invoke(this);
            _Fields.Clear();
        }
    }
}
