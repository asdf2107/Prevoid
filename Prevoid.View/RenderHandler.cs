using Prevoid.Model;
using Prevoid.Model.Commands;
using Prevoid.View.Renderers;
using Prevoid.ViewModel;
using System.Threading.Tasks;

namespace Prevoid.View
{
    public class RenderHandler
    {
        private readonly ScreenDrawer _ScreenDrawer = new ScreenDrawer();
        private readonly MapRenderer _MapRenderer;

        public RenderHandler()
        {
            _MapRenderer = new MapRenderer(_ScreenDrawer, GM.Map);

            GM.TurnChanged += RenderTurnChange;

            GM.CommandHandler.NeedMoveCommandRender += RenderMoveCommand;
            GM.CommandHandler.NeedAttackCommandRender += RenderAttackCommand;

            GM.MoveAreaOverlay.Changed += RenderOverlay;
            GM.MoveAreaOverlay.Hidden += HideOverlay;
            GM.AttackAreaOverlay.Changed += RenderOverlay;
            GM.AttackAreaOverlay.Hidden += HideOverlay;
        }

        public async Task StartRenderingAsync()
        {
            await Task.Run(_ScreenDrawer.DrawLoop);
        }

        private void RenderTurnChange()
        {
            _MapRenderer.RenderMap();
        }

        private void RenderMoveCommand(MoveCommand command)
        {
            _MapRenderer.RenderFields(new[] { (command.FromX, command.FromY), (command.ToX, command.ToY) });
        }

        private void RenderAttackCommand(AttackCommand command)
        {
            _MapRenderer.RenderField(command.AtX, command.AtY);
        }

        private void RenderOverlay(Overlay overlay)
        {
            _MapRenderer.RenderOverlay(overlay);
        }

        private void HideOverlay(Overlay overlay)
        {
            _MapRenderer.HideOverlay(overlay);
        }
    }
}
