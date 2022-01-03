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
        public TerrainType[,] TerrainTypes { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public (int, int) Selection { get; private set; }

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            Fields = new Unit[Width, Height];
            Structures = new Structure[Width, Height];
            TerrainTypes = new TerrainType[Width, Height];
            Selection = (Width / 2, Height / 2);
        }

        public void SetStructure(Structure structure, int x, int y)
        {
            structure.SetCoords(x, y);
            Structures[x, y] = structure;
        }

        public void SetUnit(Unit unit, int x, int y)
        {
            unit.SetCoords(x, y);
            Fields[x, y] = unit;
        }

        public Unit GetUnitAtSelection()
        {
            return Fields[Selection.Item1, Selection.Item2];
        }

        public Unit GetUnitById(int id)
        {
            foreach (var unit in Fields)
            {
                if (unit?.Id == id) return unit;
            }

            throw new InvalidOperationException($"Unit with id '{id}' not found");
        }

        /// <summary>
        /// Get coords corresponding to a circle of given radius with the center in (x, y)
        /// </summary>
        /// <param name="x">X coordinate of circle center</param>
        /// <param name="y">Y coordinate of circle center</param>
        /// <param name="range">Radius of the circle</param>
        /// <returns>Collection of tuples of formst (X, Yance from center)</returns>
        public List<(int, int, int)> GetArea(int x, int y, int range, bool includeCenter = false)
        {
            List<(int, int, int)> result = new List<(int, int, int)>();
            int dist = 0;
            if (includeCenter) Add(x, y);

            if (range >= 1)
            {
                dist = 1;

                Add(x + 1, y);
                Add(x - 1, y);
                Add(x, y + 1);
                Add(x, y - 1);
            }

            if (range >= 2)
            {
                dist = 2;

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
                dist = 3;

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
                dist = 4;

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

            if (range >= 5)
            {
                dist = 5;

                Add(x + 5, y);
                Add(x - 5, y);
                Add(x, y + 5);
                Add(x, y - 5);

                Add(x + 5, y - 1);
                Add(x - 5, y - 1);
                Add(x - 1, y + 5);
                Add(x - 1, y - 5);

                Add(x + 5, y + 1);
                Add(x - 5, y + 1);
                Add(x + 1, y + 5);
                Add(x + 1, y - 5);

                Add(x + 4, y - 2);
                Add(x - 4, y - 2);
                Add(x - 2, y + 4);
                Add(x - 2, y - 4);

                Add(x + 4, y + 2);
                Add(x - 4, y + 2);
                Add(x + 2, y + 4);
                Add(x + 2, y - 4);

                Add(x + 3, y + 3);
                Add(x + 3, y - 3);
                Add(x - 3, y + 3);
                Add(x - 3, y - 3);

                Add(x + 3, y + 4);
                Add(x + 3, y - 4);
                Add(x - 3, y + 4);
                Add(x - 3, y - 4);

                Add(x + 4, y + 3);
                Add(x + 4, y - 3);
                Add(x - 4, y + 3);
                Add(x - 4, y - 3);
            }

            if (range >= 6) throw new NotImplementedException();

            return result;

            void Add(int x, int y)
            {
                if (InBounds(x, y)) result.Add((x, y, dist));
            }
        }

        public bool InBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }

        public bool ContainsUnit(Unit unit)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (Fields[i, j] == unit) return true;
                }
            }

            return false;
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
