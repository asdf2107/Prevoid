using System;
using System.Collections.Generic;
using System.Text;

namespace Prevoid.Model
{
    public class Effect
    {
        public EffectType Type { get; set; }
        /// <summary>
        /// Duration (in turns)
        /// </summary>
        public int Duration { get; set; }
        public bool IsActive { get => Duration > 0; }

        public Effect()
        {
            GM.TurnChanged += () => { if (Duration > 0) Duration--; };
        }
    }
}
