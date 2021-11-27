using Prevoid.Model;
using Prevoid.Model.Commands;
using Prevoid.Model.EventArgs;
using Prevoid.View.Forms;
using Prevoid.View.Renderers;
using Prevoid.ViewModel;
using System.Threading.Tasks;

namespace Prevoid.View
{
    public class RenderHandler
    {
        private readonly ScreenDrawer _ScreenDrawer = new();

        private readonly Overlay _MoveAreaOverlay;
        private readonly Overlay _AttackAreaOverlay;

        private readonly HelpForm _HelpForm;
        private readonly UnitInfoForm _PointedUnitInfoForm;
        private readonly UnitInfoForm _SelectedUnitInfoForm;

        private readonly OverlayHelper _OverlayHelper;
        private readonly MapRenderer _MapRenderer;
        private readonly FormRenderer _FormRenderer;

        public RenderHandler()
        {
            _OverlayHelper = new OverlayHelper(GM.Map);
            _MapRenderer = new MapRenderer(_ScreenDrawer, GM.Map);
            _FormRenderer = new FormRenderer(_ScreenDrawer);

            _MoveAreaOverlay = new Overlay(OverlayType.Move);
            _AttackAreaOverlay = new Overlay(OverlayType.Attack);

            _HelpForm = new HelpForm(62, 24, 56, 6);
            _PointedUnitInfoForm = new PointedUnitInfoForm(62, 0, 20, 22);
            _SelectedUnitInfoForm = new SelectedUnitInfoForm(83, 0, 20, 22);

            GM.TurnEnded += RenderTurnEnd;
            GM.TurnChanged += RenderTurnChange;
            GM.Map.SelectionMoved += RenderSelectionMove;
            GM.SelectedUnitChanged += RenderSelectedUnitChange;

            GM.CommandHandler.NeedMoveCommandRender += RenderMoveCommand;
            GM.CommandHandler.NeedAttackCommandRender += RenderAttackCommand;
            GM.CommandHandler.NeedSetUnitCommandRender += RenderSetUnitCommand;

            _MoveAreaOverlay.ShownChanged += RenderOverlay;
            _MoveAreaOverlay.Hidden += HideOverlay;
            _AttackAreaOverlay.ShownChanged += RenderOverlay;
            _AttackAreaOverlay.Hidden += HideOverlay;
        }

        public async Task StartRenderingAsync()
        {
            await Task.Run(_ScreenDrawer.DrawLoop);
        }

        private void RenderTurnEnd()
        {
            GM.FieldOfView.Clear();
            _MapRenderer.RenderMap();
            _FormRenderer.Render(_HelpForm);
            _FormRenderer.Render(_PointedUnitInfoForm);
            _FormRenderer.Render(_SelectedUnitInfoForm);
        }

        private void RenderTurnChange()
        {
            _MapRenderer.RecacheAndRedrawFieldOfView();
            _MapRenderer.RenderMap();
            _OverlayHelper.UpdateMoveAreaOverlay(_MoveAreaOverlay);
            _OverlayHelper.UpdateAttackAreaOverlay(_AttackAreaOverlay);
            _FormRenderer.Render(_HelpForm);
            _FormRenderer.Render(_PointedUnitInfoForm);
            _FormRenderer.Render(_SelectedUnitInfoForm);
        }

        private void RenderSelectionMove(SelectionMovedEventArgs eventArgs)
        {
            _MapRenderer.RenderFields(new[] { (eventArgs.FromX, eventArgs.FromY), (eventArgs.ToX, eventArgs.ToY) });           
            _OverlayHelper.UpdateMoveAreaOverlay(_MoveAreaOverlay);
            _OverlayHelper.UpdateAttackAreaOverlay(_AttackAreaOverlay);
            _FormRenderer.Render(_PointedUnitInfoForm); // TODO: Rerender only if PointedUnit changes
        }

        private void RenderSelectedUnitChange(Unit unit)
        {
            _OverlayHelper.UpdateMoveAreaOverlay(_MoveAreaOverlay);
            _OverlayHelper.UpdateAttackAreaOverlay(_AttackAreaOverlay);
            _FormRenderer.Render(_SelectedUnitInfoForm);
        }

        private void RenderMoveCommand(MoveCommand command)
        {
            _MapRenderer.RecacheAndRedrawFieldOfView();
            _MapRenderer.RenderFields(new[] { (command.FromX, command.FromY), (command.ToX, command.ToY) });
            _FormRenderer.Render(_PointedUnitInfoForm);
        }

        private void RenderAttackCommand(AttackCommand command)
        {
            _MapRenderer.RenderField(command.AtX, command.AtY);
            _FormRenderer.Render(_PointedUnitInfoForm);
            _FormRenderer.Render(_SelectedUnitInfoForm);
        }

        private void RenderSetUnitCommand(SetUnitCommand command)
        {
            _MapRenderer.RenderField(command.AtX, command.AtY);
            _FormRenderer.Render(_HelpForm);
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
