using Prevoid.Model.Commands;
using Prevoid.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Model
{
    public abstract class Unit : ILocateable, IVisible, IHarmable
    {
        public int X { get; protected set; } = -1;
        public int Y { get; protected set; } = -1;
        public SpriteType SpriteType { get; private set; }
        public Player Player { get; private set; }
        public bool CanMove { get => !HasMoved && MoveRange > 0; }
        public bool CanAttack { get => !HasAttacked && Weapon != null; }
        public bool HasMoved { get; private set; }
        public bool HasAttacked { get; private set; }
        public int MoveRange { get; protected set; }
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

            GM.TurnChanged += HandleNextTurn;
        }

        private void HandleNextTurn()
        {
            HasMoved = false;
            HasAttacked = false;
        }

        public void SetCoords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void TryMove(int toX, int toY)
        {
            if (CanMove) Move(toX, toY);
        }

        private void Move(int toX, int toY)
        {
            GM.CommandHandler.HandleCommand(new MoveCommand(this, toX, toY));
            HasMoved = true;
        }

        public virtual IEnumerable<(int, int)> GetMoveArea()
        {
            List<(int, int, int)> coordsWithDist = GM.Map.GetArea(X, Y, MoveRange);
            coordsWithDist.RemoveAll(c => GM.Map.Fields[c.Item1, c.Item2] != null
                || c.Item3 - GM.Map.TerrainTypes[c.Item1, c.Item2].GetMovementBonus() > MoveRange);
            return coordsWithDist.GetCoords();
        }

        public virtual IEnumerable<(int, int)> GetAttackArea()
        {
            return GM.Map.GetArea(X, Y, Weapon?.AttackRange ?? 0).GetCoords();
        }

        public virtual IEnumerable<(int, int)> GetAttackTargets()
        {
            List<(int, int, int)> coordsWithDist = GM.Map.GetArea(X, Y, Weapon?.AttackRange ?? 0);
            return coordsWithDist.Where(c => GM.Map.Fields[c.Item1, c.Item2] != null
                && GM.Map.Fields[c.Item1, c.Item2].Player != Player).GetCoords();     
        }

        public void TryAttack(int toX, int toY)
        {
            if (CanAttack) Attack(toX, toY);
        }

        private void Attack(int atX, int atY)
        {
            GM.CommandHandler.HandleCommand(new AttackCommand(this, atX, atY, CalculateDamage(), Weapon.DamageType));
            HasAttacked = true;
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
