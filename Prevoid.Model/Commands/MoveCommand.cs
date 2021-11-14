namespace Prevoid.Model.Commands
{
    public class MoveCommand : Command
    {
        public Unit Unit { get; private set; }
        public int FromX { get; private set; }
        public int FromY { get; private set; }
        public int ToX { get; private set; }
        public int ToY { get; private set; }

        public MoveCommand(Unit unit, int toX, int toY)
        {
            Unit = unit;
            FromX = Unit.X;
            FromY = Unit.Y;
            ToX = toX;
            ToY = toY;
        }
    }
}
