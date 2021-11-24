using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Unit> GetUnits()
        {
            return GM.Map.Fields.Cast<Unit>().Where(u => u?.Player.Id == Id);
        }

        public IEnumerable<(int, int)> GetFieldOfView()
        {
            List<(int, int)> result = new();
            var units = GetUnits();
            foreach (var unit in units)
            {
                result.AddRange(unit.GetFieldOfView());
            }

            return result.Distinct();
        }
    }
}
