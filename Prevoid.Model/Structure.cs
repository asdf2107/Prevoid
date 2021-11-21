using Prevoid.ViewModel;

namespace Prevoid.Model
{
    public abstract class Structure : ILocateable, IVisible
    {
        public int X { get; set; } = -1;
        public int Y { get; set; } = -1;
        public SpriteType SpriteType { get; private set; }
        /// <summary>
        /// How well this structure can hide units (in the Fog of war). Can't be less than 0.
        /// </summary>
        public int FogBonus { get; private set; }
        /// <summary>
        /// Increses or decreses movement range.
        /// </summary>
        public int MovementBonus { get; private set; }
        /// <summary>
        /// Increses or decreses attack range.
        /// </summary>
        public int AttackRangeBonus { get; private set; }

        public Structure(SpriteType spriteType, int fogBonus, int movementBonus, int attackRangeBonus)
        {
            SpriteType = spriteType;
            FogBonus = fogBonus;
            MovementBonus = movementBonus;
            AttackRangeBonus = attackRangeBonus;
        }

        public void SetCoords(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
