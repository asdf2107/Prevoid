using Prevoid.View.Forms;

namespace Prevoid.View.Renderers
{
    public class FormRenderer : Renderer
    {
        public FormRenderer(ScreenDrawer screenDrawer) : base(screenDrawer) { }

        public void Render(Form form)
        {
            _Drawer.Draw(form.GetRenderView());
        }
    }
}
