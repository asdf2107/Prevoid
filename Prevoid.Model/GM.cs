using Prevoid.ViewModel;
using System;

namespace Prevoid.Model
{
    public static class GM
    {
        public static event Action TurnChanged;

        public static Map Map;
        public static Player Player1;
        public static Player Player2;
        public static readonly Random Random = new Random();
        public static readonly CommandHandler CommandHandler = new CommandHandler();
        public static readonly Overlay MoveAreaOverlay;
        public static readonly Overlay AttackAreaOverlay;

        static GM()
        {
            Map = new Map(30, 30);
            Player1 = new Player(1, ConsoleColor.Blue);
            Player2 = new Player(2, ConsoleColor.Red);

            var translucentSprite = new Sprite
            {
                Type = SpriteType.Translucent,
            };
            MoveAreaOverlay = new Overlay(OverlayType.Move, translucentSprite);
            AttackAreaOverlay = new Overlay(OverlayType.Attack, translucentSprite);
        }

        public static void NextTurn()
        {
            TurnChanged?.Invoke();
        }
    }
}
