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

            if  (GM.GameState == GameState.Movement)
            {
                var unit = GM.SelectedUnit ?? Map.Fields[Map.Selection.Item1, Map.Selection.Item2];
                overlay.SetOverlayType(GM.SelectedUnit is null ? OverlayType.Move : OverlayType.Select);

                if (unit is not null)
                {
                    if (unit.Player != GM.CurrentPlayer || !unit.CanMove)
                    {
                        overlay.SetOverlayType(OverlayType.Forbidden);
                    }

                    overlay.Add(unit.GetMoveArea());
                }
            }
        }

        public void UpdateAttackAreaOverlay(Overlay overlay)
        {
            overlay.Clear();

            if (GM.GameState == GameState.Attack)
            {
                var chosenUnit = GM.SelectedUnit;
                var unit = Map.Fields[Map.Selection.Item1, Map.Selection.Item2];

                if (chosenUnit is not null)
                {
                    overlay.SetOverlayType(OverlayType.Select);
                    overlay.Add(chosenUnit.GetAttackTargets());
                }
                else if (unit is not null)
                {
                    overlay.SetOverlayType(unit.Player == GM.CurrentPlayer && unit.CanAttack ? 
                        OverlayType.Attack : OverlayType.Forbidden);
                    overlay.Add(unit.GetAttackArea());
                }
            }
        }
    }
}
