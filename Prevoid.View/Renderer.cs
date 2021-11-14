using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.View
{
    public class Renderer
    {
        protected readonly ScreenDrawer _Drawer;

        public Renderer(ScreenDrawer drawer)
        {
            _Drawer = drawer;
        }
    }
}
