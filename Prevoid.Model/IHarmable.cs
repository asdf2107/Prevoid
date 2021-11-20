namespace Prevoid.Model
{
    public interface IHarmable
    {
        float Hp { get; }
        Player Player { get; }
        void Harm(float damage);
        void Destroy();
    }
}
