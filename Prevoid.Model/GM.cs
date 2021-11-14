using Prevoid.ViewModel;
using System;

namespace Prevoid.Model
{
    public static class GM
    {
        public static event Action TurnChanged;

        public static Map Map;
        public static readonly Random Random = new Random();
        public static readonly CommandHandler CommandHandler = new CommandHandler();
        public static readonly Overlay MoveAreaOverlay = new Overlay(OverlayType.Move, SpriteType.Translucent);
        public static readonly Overlay AttackAreaOverlay = new Overlay(OverlayType.Attack, SpriteType.Translucent);
    }
}
