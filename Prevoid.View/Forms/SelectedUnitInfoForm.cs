using Prevoid.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.View.Forms
{
    public class SelectedUnitInfoForm : UnitInfoForm
    {
        public SelectedUnitInfoForm(int x, int y, int width, int height) : base(x, y, width, height) { }

        protected override void SetUnit()
        {
            if (GM.SelectedUnit is not null)
            {
                Unit = GM.SelectedUnit;
            }
            else
            {
                Unit = null;
            }
        }
    }
}
