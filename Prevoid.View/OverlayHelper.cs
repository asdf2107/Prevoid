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

            var unit = GM.SelectedUnit ?? Map.Fields[Map.Selection.Item1, Map.Selection.Item2];
            overlay.SetOverlayType(GM.SelectedUnit is null ? OverlayType.Move : OverlayType.Select);

            if (unit is not null && unit.Player == GM.CurrentPlayer)
            {
                overlay.Add(unit.GetMoveArea());
            }
        }
    }
}
