using Prevoid.Model.MapGeneration.MapGenStrategies;
using Prevoid.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Model
{
    public static class GM
    {
        public static event Action TurnEnded;
        public static event Action TurnChanged;
        public static event Action<Unit> SelectedUnitChanged;

        public static readonly Random Random = new();
        public static readonly CommandHandler CommandHandler = new();
        public static Map Map { get; private set; }
        public static Player Player1 { get; private set; }
        public static Player Player2 { get; private set; }
        public static Player CurrentPlayer { get; private set; }
        public static List<(int, int)> FieldOfView { get; set; }
        public static GameState GameState { get; private set; }
        public static bool HasTurnEnded { get; private set; }
        private static Unit _SelectedUnit;
        /// <summary>
        /// Unit which was selected
        /// </summary>
        public static Unit SelectedUnit
        {
            get { return _SelectedUnit; }
            set
            {
                _SelectedUnit = value;
                SelectedUnitChanged?.Invoke(_SelectedUnit);
            }
        }
        /// <summary>
        /// Unit under the selection
        /// </summary>
        public static Unit PointedUnit
        {
            get { return Map.Fields[Map.Selection.Item1, Map.Selection.Item2]; }
        }

        static GM()
        {
            Map = new Map(Constants.MapWidth, Constants.MapHeight);
            new DefaultMapGenStrategy().GenMap(Map, Random.Next());
            Player1 = new Player(1, ConsoleColor.Blue);
            Player2 = new Player(2, ConsoleColor.Red);
            CurrentPlayer = Player1;
        }

        public static void Start()
        {
            GameState = GameState.Movement;
            TurnChanged?.Invoke();
        }

        /// <summary>
        /// Recache FieldOfView of current player.
        /// </summary>
        /// <returns>Coords wich are different</returns>
        public static IEnumerable<(int, int)> RecacheFieldOfView()
        {
            List<(int, int)> oldFieldOfView = FieldOfView ?? new();
            FieldOfView = CurrentPlayer.GetFieldOfView().ToList();
            oldFieldOfView.AddRange(FieldOfView); // TODO: Replace with full outer join
            return oldFieldOfView.Distinct();
        }

        public static bool CanCurrentPlayerSee(int x, int y)
        {
            return FieldOfView.Contains((x, y));
        }

        public static bool CanCurrentPlayerSee((int, int) coords)
        {
            return FieldOfView.Contains(coords);
        }

        public static void NextTurn()
        {
            CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;
            GameState = GameState == GameState.Attack && CurrentPlayer == Player1 ? GameState.Movement : GameState.Attack;
            SelectedUnit = null;
            HasTurnEnded = true;
            TurnEnded?.Invoke();
        }

        /// <summary>
        /// Handles user input. If returns false, the program should end execution.
        /// </summary>
        /// <param name="keyInfo">User input</param>
        /// <returns></returns>
        public static bool HandleInput(ConsoleKeyInfo keyInfo)
        {
            if (HasTurnEnded)
            {
                HasTurnEnded = false;
                TurnChanged?.Invoke();
                return true;
            }

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
                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                    EnterPressed();
                    break;
                case ConsoleKey.Tab:
                    TabPressed();
                    break;
                case ConsoleKey.Escape:
                    EscapePressed();
                    break;
            }

            return true;
        }

        private static void EnterPressed()
        {
            if (GameState == GameState.SettingUnits)
            {
                SelectedUnit.TrySet(Map.Selection.Item1, Map.Selection.Item2);
            }
            else
            {
                if (SelectedUnit is null)
                {
                    var unit = Map.GetUnitAtSelection();
                    if (unit?.Player == CurrentPlayer)
                    {
                        if ((GameState == GameState.Movement && unit.CanMove) || (GameState == GameState.Attack && unit.CanAttack))
                        {
                            SelectedUnit = unit;
                        }
                    }
                }
                else
                {
                    if (GameState == GameState.Movement
                        && SelectedUnit.GetMoveArea().Contains((Map.Selection.Item1, Map.Selection.Item2)))
                    {
                        SelectedUnit.TryMove(Map.Selection.Item1, Map.Selection.Item2);
                        SelectedUnit = null;
                    }
                    else if (GameState == GameState.Attack
                        && SelectedUnit.GetAttackTargets().Contains(((ILocateable)Map.GetUnitAtSelection())?.Coords ?? (-1, -1)))
                    {
                        SelectedUnit.TryAttack(Map.Selection.Item1, Map.Selection.Item2);
                        if (!SelectedUnit.CanAttack) SelectedUnit = null;
                    }
                }
            }
        }

        private static void TabPressed()
        {
            NextTurn();
        }

        private static void EscapePressed()
        {
            if (SelectedUnit is not null)
            {
                SelectedUnit = null;
            }
        }
    }
}
