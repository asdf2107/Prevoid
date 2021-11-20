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
        public static Player CurrentPlayer;
        public static GameState GameState;
        public static readonly Random Random = new Random();
        public static readonly CommandHandler CommandHandler = new CommandHandler();

        static GM()
        {
            Map = new Map(30, 30);
            Player1 = new Player(1, ConsoleColor.Blue);
            Player2 = new Player(2, ConsoleColor.Red);
            CurrentPlayer = Player1;
        }

        public static void NextTurn()
        {
            if (CurrentPlayer == Player1)
            {
                CurrentPlayer = Player2;
                GameState = GameState == GameState.Movement ? GameState.Movement : GameState.Attack;
            }
            else
            {
                CurrentPlayer = Player1;
                GameState = GameState == GameState.Attack ? GameState.Movement : GameState.Attack;
            }

            TurnChanged?.Invoke();
        }

        /// <summary>
        /// Handles user input. If returns false, the program should end execution.
        /// </summary>
        /// <param name="keyInfo">User input</param>
        /// <returns></returns>
        public static bool HandleInput(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    Map.TryMoveSelection(Direction.North);
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    Map.TryMoveSelection(Direction.South);
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    Map.TryMoveSelection(Direction.West);
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    Map.TryMoveSelection(Direction.East);
                    break;
            }

            return true;
        }
    }
}
