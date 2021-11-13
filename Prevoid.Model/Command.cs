namespace Prevoid.Model
{
    public abstract class Command
    {
        public bool NeedRender { get; set; } = true;
    }
}
