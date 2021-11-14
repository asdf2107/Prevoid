using Prevoid.Model;
using Prevoid.Model.Commands;
using Prevoid.View.Renderers;
using System.Threading.Tasks;

namespace Prevoid.View
{
    public static class RenderHandler
    {
        private static readonly ScreenDrawer _ScreenDrawer = new ScreenDrawer();
        private static readonly MapRenderer _MapRenderer = new MapRenderer(_ScreenDrawer, GM.Map);

        static RenderHandler()
        {
            Task.Run(_ScreenDrawer.DrawLoop);
            // TODO: Add CommandHandler and GM Overlays event handlers

            GM.CommandHandler.NeedMoveCommandRender += RenderMoveCommand;
            GM.CommandHandler.NeedAttackCommandRender += RenderAttackCommand;
        }

        private static void RenderMoveCommand(MoveCommand command)
        {
            _MapRenderer.RenderFields(new[] { (command.FromX, command.FromY), (command.ToX, command.ToY) });
        }

        private static void RenderAttackCommand(AttackCommand command)
        {
            _MapRenderer.RenderField(command.AtX, command.AtY);
        }
    }
}
