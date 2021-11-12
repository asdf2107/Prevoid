using System;
using System.Collections.Generic;
using System.Text;

namespace Prevoid.Model
{
    public class Player
    {
        public int Id { get; private set; }
        public ConsoleColor Color { get; private set; }

        public Player(int id, ConsoleColor color)
        {
            Id = id;
            Color = color;
        }
    }
}
