using System;
using System.Collections.Generic;
using System.Text;

namespace Prevoid.Model
{
    public class Map
    {
        public Unit[,] Fields { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            Fields = new Unit[Width, Height];
        }
    }
}
