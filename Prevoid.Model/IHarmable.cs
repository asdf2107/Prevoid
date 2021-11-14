namespace Prevoid.Model
{
    public interface IHarmable
    {
        float Hp { get; }
        void Harm(float damage);
        void Destroy();
    }
}
