using Prevoid.Model.Commands;
using Prevoid.ViewModel;
using System.Collections.Generic;

namespace Prevoid.Model
{
    public abstract class Unit : ILocateable, IVisible, IHarmable
    {
        public int X { get; set; } = -1;
        public int Y { get; set; } = -1;
        public SpriteType SpriteType { get; private set; }
        public Player Player { get; private set; }
        public bool CanMove { get => MoveRange > 0; }
        public int MoveRange { get; protected set; }
        public bool CanAttack { get => Weapon != null; }
        public Weapon Weapon { get; protected set; }
        public float MaxHp { get; protected set; }
        public float Hp { get; protected set; }

        protected List<Effect> _Effects = new List<Effect>();

        public Unit(Player player, int moveRange, float maxHP, SpriteType spriteType, Weapon weapon = null)
        {
            Player = player;

            MoveRange = moveRange;
            MaxHp = maxHP;
            Hp = MaxHp;
            SpriteType = spriteType;
            Weapon = weapon;
        }

        public void Move(int toX, int toY)
        {
            GM.CommandHandler.HandleCommand(new MoveCommand(this, toX, toY));
        }

        public virtual List<(int, int)> GetMoveArea()
        {
            return GM.Map.GetArea(X, Y, MoveRange);
        }

        public void Attack(int atX, int atY)
        {
            GM.CommandHandler.HandleCommand(new AttackCommand(this, atX, atY, CalculateDamage(), Weapon.DamageType));
        }

        public virtual List<(int, int)> GetAttackArea()
        {
            return GM.Map.GetArea(X, Y, Weapon?.AttackRange ?? 0);
        }

        public void Harm(float damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                Destroy();
            }
        }

        public void Destroy()
        {
            GM.Map.Fields[X, Y] = null;
        }

        protected virtual float CalculateDamage()
        {
            return GM.Random.NextFloat(Weapon.Damage * (1 - Constants.DamageDelta), Weapon.Damage * (1 + Constants.DamageDelta));
        }

        public void AddEffect<T>(T effect) where T : Effect
        {
            foreach (var existingEffect in _Effects)
            {
                if (existingEffect is T)
                {
                    existingEffect.MergeWith(effect);
                    return;
                }
            }

            _Effects.Add(effect);
        }
    }
}
