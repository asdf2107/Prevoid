using System;
using System.Collections.Generic;

namespace Prevoid.Model
{
    public class Map
    {
        public Unit[,] Fields { get; private set; }
        public Structure[,] Structures { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            Fields = new Unit[Width, Height];
            Structures = new Structure[Width, Height];
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
            }

            if (range >= 4) throw new NotImplementedException();

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
    }
}
