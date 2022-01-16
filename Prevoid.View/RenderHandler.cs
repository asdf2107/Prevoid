using Prevoid.Model;
using Prevoid.Model.CommandHandlers;
using Prevoid.Model.Commands;
using Prevoid.Model.EventArgs;
using Prevoid.View.Forms;
using Prevoid.View.Renderers;
using Prevoid.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prevoid.View
{
    public static class RenderHandler
    {
        private static readonly ScreenDrawer _ScreenDrawer = new();

        private static readonly Overlay _MoveAreaOverlay;
        private static readonly Overlay _AttackAreaOverlay;

        private static readonly HelpForm _HelpForm;
        private static readonly UnitInfoForm _PointedUnitInfoForm;
        private static readonly UnitInfoForm _SelectedUnitInfoForm;

        private static readonly OverlayHelper _OverlayHelper;
        private static readonly MapRenderer _MapRenderer;
        private static readonly FormRenderer _FormRenderer;

        static RenderHandler()
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
            GM.GameStarted += RenderTurnChange;
            GM.TurnChanged += RenderTurnChange;
            GM.Map.SelectionMoved += RenderSelectionMove;
            GM.SelectedUnitChanged += RenderSelectedUnitChange;

            MoveCommandHandler.NeedMoveCommandRender += RenderMoveCommand;
            AttackCommandHandler.NeedAttackCommandRender += RenderAttackCommand;
            SetUnitCommandHandler.NeedSetUnitCommandRender += RenderSetUnitCommand;
            GenMapCommandHandler.NeedGenMapCommandRender += RenderFullMap;

            _MoveAreaOverlay.ShownChanged += RenderOverlay;
            _MoveAreaOverlay.Hidden += HideOverlay;
            _AttackAreaOverlay.ShownChanged += RenderOverlay;
            _AttackAreaOverlay.Hidden += HideOverlay;
        }

        public static async Task StartRenderingAsync()
        {
            await Task.Run(_ScreenDrawer.DrawLoop);
        }

        internal static void RenderForm(Form form)
        {
            _FormRenderer.Render(form);
        }

        private static void RenderTurnEnd(TurnEndedEventArgs e)
        {
            GM.FieldOfView.Clear();
            _MapRenderer.RenderMap();
            _FormRenderer.Render(_HelpForm);
            _FormRenderer.Render(_PointedUnitInfoForm);
            _FormRenderer.Render(_SelectedUnitInfoForm);
        }

        private static void RenderTurnChange()
        {
            RenderFullMap();
            _FormRenderer.Render(_HelpForm);
            _FormRenderer.Render(_PointedUnitInfoForm);
            _FormRenderer.Render(_SelectedUnitInfoForm);
        }

        private static void RenderFullMap()
        {
            _MapRenderer.RecacheAndRedrawFieldOfView();
            _MapRenderer.RenderMap();
            _OverlayHelper.UpdateMoveAreaOverlay(_MoveAreaOverlay);
            _OverlayHelper.UpdateAttackAreaOverlay(_AttackAreaOverlay);
        }

        private static void RenderSelectionMove(SelectionMovedEventArgs eventArgs)
        {
            _MapRenderer.RenderFields(new[] { (eventArgs.FromX, eventArgs.FromY), (eventArgs.ToX, eventArgs.ToY) });           
            _OverlayHelper.UpdateMoveAreaOverlay(_MoveAreaOverlay);
            _OverlayHelper.UpdateAttackAreaOverlay(_AttackAreaOverlay);
            _FormRenderer.Render(_PointedUnitInfoForm); // TODO: Rerender only if PointedUnit changes
        }

        private static void RenderSelectedUnitChange(Unit unit)
        {
            _OverlayHelper.UpdateMoveAreaOverlay(_MoveAreaOverlay);
            _OverlayHelper.UpdateAttackAreaOverlay(_AttackAreaOverlay);
            _FormRenderer.Render(_SelectedUnitInfoForm);
        }

        private static void RenderMoveCommand(MoveCommand command)
        {
            _MapRenderer.RecacheAndRedrawFieldOfView();
            _MapRenderer.RenderFields(new[] { (command.FromX, command.FromY), (command.ToX, command.ToY) });
            _FormRenderer.Render(_PointedUnitInfoForm);
        }

        private static void RenderAttackCommand(AttackCommand command)
        {
            _MapRenderer.RenderField(command.AtX, command.AtY);
            _FormRenderer.Render(_PointedUnitInfoForm);
            _FormRenderer.Render(_SelectedUnitInfoForm);
        }

        private static void RenderSetUnitCommand(SetUnitCommand command)
        {
            _MapRenderer.RenderField(command.AtX, command.AtY);
            _FormRenderer.Render(_HelpForm);
        }

        private static void RenderOverlay(Overlay overlay)
        {
            _MapRenderer.RenderOverlay(overlay);
        }

        private static void HideOverlay(Overlay overlay)
        {
            _MapRenderer.HideOverlay(overlay);
        }
    }
}
