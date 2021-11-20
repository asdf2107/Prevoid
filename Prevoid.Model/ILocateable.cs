namespace Prevoid.Model
{
    public interface ILocateable
    {
        int X { get; }
        int Y { get; }
        void SetCoords(int x, int y);
    }
}
