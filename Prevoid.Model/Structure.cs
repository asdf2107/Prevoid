using Prevoid.ViewModel;

namespace Prevoid.Model
{
    public abstract class Structure : ILocateable, IVisible
    {
        public int X { get; set; } = -1;
        public int Y { get; set; } = -1;
        public SpriteType SpriteType { get; private set; }
        /// <summary>
        /// How well this structure can hide units (in the Fog of war). Can't be less than None.
        /// </summary>
        public Scale FogBonus { get; private set; }
        /// <summary>
        /// Increses or decreses movement range.
        /// </summary>
        public Scale MovementBonus { get; private set; }
        /// <summary>
        /// Increses or decreses attack range.
        /// </summary>
        public Scale AttackRangeBonus { get; private set; }

        public Structure(SpriteType spriteType, Scale fogBonus, Scale movementBonus, Scale attackRangeBonus)
        {
            SpriteType = spriteType;
            FogBonus = fogBonus;
            MovementBonus = movementBonus;
            AttackRangeBonus = attackRangeBonus;
        }
    }
}
