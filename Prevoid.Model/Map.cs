using Prevoid.Model.EventArgs;
using System;
using System.Collections.Generic;

namespace Prevoid.Model
{
    public class Map
    {
        public event Action<SelectionMovedEventArgs> SelectionMoved;

        public Unit[,] Fields { get; private set; }
        public Structure[,] Structures { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public (int, int) Selection { get; private set; }

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            Fields = new Unit[Width, Height];
            Structures = new Structure[Width, Height];
            Selection = (Width / 2, Height / 2);
        }

        public void SetStructure(Structure structure, int x, int y)
        {
            structure.X = x;
            structure.Y = y;
            Structures[x, y] = structure;
        }

        public List<(int, int)> GetArea(int x, int y, int range)
        {
            List<(int, int)> result = new List<(int, int)>();

            if (range >= 1)
            {
                Add(x + 1, y);
                Add(x - 1, y);
                Add(x, y + 1);
                Add(x, y - 1);
            }

            if (range >= 2)
            {
                Add(x + 2, y);
                Add(x - 2, y);
                Add(x, y + 2);
                Add(x, y - 2);

                Add(x + 1, y + 1);
                Add(x - 1, y - 1);
                Add(x - 1, y + 1);
                Add(x + 1, y - 1);
            }

            if (range >= 3)
            {
                Add(x + 3, y);
                Add(x - 3, y);
                Add(x, y + 3);
                Add(x, y - 3);

                Add(x + 2, y + 2);
                Add(x - 2, y - 2);
                Add(x - 2, y + 2);
                Add(x + 2, y - 2);

                Add(x + 2, y + 1);
                Add(x - 2, y - 1);
                Add(x - 2, y + 1);
                Add(x + 2, y - 1);

                Add(x + 1, y + 2);
                Add(x - 1, y - 2);
                Add(x - 1, y + 2);
                Add(x + 1, y - 2);
            }

            if (range >= 4)
            {
                Add(x + 4, y);
                Add(x - 4, y);
                Add(x, y + 4);
                Add(x, y - 4);

                Add(x + 4, y - 1);
                Add(x - 4, y - 1);
                Add(x - 1, y + 4);
                Add(x - 1, y - 4);

                Add(x + 4, y + 1);
                Add(x - 4, y + 1);
                Add(x + 1, y + 4);
                Add(x + 1, y - 4);

                Add(x + 3, y - 1);
                Add(x - 3, y - 1);
                Add(x - 1, y + 3);
                Add(x - 1, y - 3);

                Add(x + 3, y + 1);
                Add(x - 3, y + 1);
                Add(x + 1, y + 3);
                Add(x + 1, y - 3);

                Add(x + 3, y - 2);
                Add(x - 3, y - 2);
                Add(x - 2, y + 3);
                Add(x - 2, y - 3);

                Add(x + 3, y + 2);
                Add(x - 3, y + 2);
                Add(x + 2, y + 3);
                Add(x + 2, y - 3);
            }

            if (range >= 5) throw new NotImplementedException();

            return result;

            void Add(int x, int y)
            {
                if (InBounds(x, y)) result.Add((x, y));
            }
        }

        public bool InBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }

        public bool TryMoveSelection(Direction direction)
        {
            int dx = (direction == Direction.West) ? -1 : (direction == Direction.East) ? 1 : 0;
            int dy = (direction == Direction.North) ? -1 : (direction == Direction.South) ? 1 : 0;
            return TrySetSelection(Selection.Item1 + dx, Selection.Item2 + dy);
        }

        private bool TrySetSelection(int x, int y)
        {
            if (InBounds(x, y))
            {
                var eventArgs = new SelectionMovedEventArgs(Selection.Item1, Selection.Item2, x, y);
                Selection = (x, y);
                SelectionMoved?.Invoke(eventArgs);
                return true;
            }

            return false;
        }
    }
}
