using Prevoid.Model;
using Prevoid.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.View
{
    public class OverlayHelper
    {
        public Map Map { get; private set; }

        public OverlayHelper(Map map)
        {
            Map = map;
        }

        public void UpdateMoveAreaOverlay(Overlay overlay)
        {
            overlay.Clear();

            var unit = Map.Fields[Map.Selection.Item1, Map.Selection.Item2];
            if (unit != null && unit.Player == GM.CurrentPlayer)
            {
                overlay.Add(Map.GetArea(Map.Selection.Item1, Map.Selection.Item2, unit.MoveRange));
            }
        }
    }
}
