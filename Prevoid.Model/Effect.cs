namespace Prevoid.Model
{
    public abstract class Effect
    {
        /// <summary>
        /// Duration (in turns)
        /// </summary>
        public int Duration { get; set; }
        public bool IsActive { get => Duration > 0; }

        public Effect()
        {
            GM.TurnChanged += () => { if (Duration > 0) Duration--; };
        }

        public abstract void MergeWith(Effect effect);
    }
}
