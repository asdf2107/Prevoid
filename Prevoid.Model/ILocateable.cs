namespace Prevoid.Model
{
    public interface ILocateable
    {
        int X { get; }
        int Y { get; }
        (int, int) Coords => (X, Y);
        void SetCoords(int x, int y);
    }
}
