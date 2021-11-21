using Prevoid.Model;
using Prevoid.Model.Commands;
using Prevoid.Model.EventArgs;
using Prevoid.View.Renderers;
using Prevoid.ViewModel;
using System.Threading.Tasks;

namespace Prevoid.View
{
    public class RenderHandler
    {
        private readonly ScreenDrawer _ScreenDrawer = new ScreenDrawer();
        private readonly OverlayHelper _OverlayHelper;
        private readonly Overlay _MoveAreaOverlay;
        private readonly Overlay _AttackAreaOverlay;

        private readonly MapRenderer _MapRenderer;

        public RenderHandler()
        {
            _MapRenderer = new MapRenderer(_ScreenDrawer, GM.Map);
            _OverlayHelper = new OverlayHelper(GM.Map);

            _MoveAreaOverlay = new Overlay(OverlayType.Move);
            _AttackAreaOverlay = new Overlay(OverlayType.Attack);

            GM.TurnChanged += RenderTurnChange;
            GM.Map.SelectionMoved += RenderSelectionMove;
            GM.SelectedUnitChanged += RenderSelectedUnitChange;

            GM.CommandHandler.NeedMoveCommandRender += RenderMoveCommand;
            GM.CommandHandler.NeedAttackCommandRender += RenderAttackCommand;

            _MoveAreaOverlay.ShownChanged += RenderOverlay;
            _MoveAreaOverlay.Hidden += HideOverlay;
            _AttackAreaOverlay.ShownChanged += RenderOverlay;
            _AttackAreaOverlay.Hidden += HideOverlay;
        }

        public async Task StartRenderingAsync()
        {
            await Task.Run(_ScreenDrawer.DrawLoop);
        }

        private void RenderTurnChange()
        {
            _MapRenderer.RenderMap();
        }

        private void RenderSelectionMove(SelectionMovedEventArgs eventArgs)
        {
            _MapRenderer.RenderFields(new[] { (eventArgs.FromX, eventArgs.FromY), (eventArgs.ToX, eventArgs.ToY) });
            _OverlayHelper.UpdateMoveAreaOverlay(_MoveAreaOverlay);
            _OverlayHelper.UpdateAttackAreaOverlay(_AttackAreaOverlay);
        }

        private void RenderSelectedUnitChange(Unit unit)
        {
            _OverlayHelper.UpdateMoveAreaOverlay(_MoveAreaOverlay);
            _OverlayHelper.UpdateAttackAreaOverlay(_AttackAreaOverlay);
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
