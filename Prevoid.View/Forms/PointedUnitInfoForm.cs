using Prevoid.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.View.Forms
{
    public class PointedUnitInfoForm : UnitInfoForm
    {
        public PointedUnitInfoForm(int x, int y, int width, int height) : base(x, y, width, height) { }

        protected override void SetUnit()
        {
            if (GM.PointedUnit is not null && GM.CanCurrentPlayerSee(GM.PointedUnit.X, GM.PointedUnit.Y))
            {
                Unit = GM.PointedUnit;
            }
            else
            {
                Unit = null;
            }
        }
    }
}
