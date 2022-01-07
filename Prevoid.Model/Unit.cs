using Prevoid.Model.Commands;
using Prevoid.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Model
{
    public abstract class Unit : ILocateable, IVisible, IHarmable
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int X { get; protected set; } = -1;
        public int Y { get; protected set; } = -1;
        public SpriteType SpriteType { get; private set; }
        public Player Player { get; private set; }
        public bool CanMove { get => !HasMoved && MoveRange > 0; }
        public bool CanAttack { get => Weapon != null && Weapon.Rounds > 0; }
        public bool HasMoved { get; private set; }
        public int MoveRange { get; protected set; }
        public int FieldOfView { get; protected set; }
        public Weapon Weapon { get; protected set; }
        public float MaxHp { get; protected set; }
        public float Hp { get; protected set; }

        protected List<Effect> _Effects = new List<Effect>();

        private static int IdCounter = 0;

        static Unit()
        {
            GM.GameStarted += () => IdCounter = 0;
        }

        public Unit(Player player, string name, int moveRange, float maxHP, SpriteType spriteType, int fieldOfView, Weapon weapon = null)
        {
            Id = IdCounter++;
            Player = player;
            Name = name;

            MoveRange = moveRange;
            MaxHp = maxHP;
            Hp = MaxHp;
            SpriteType = spriteType;
            FieldOfView = fieldOfView;
            Weapon = weapon;

            GM.TurnChanged += HandleNextTurn;
        }

        private void HandleNextTurn()
        {
            HasMoved = false;
            Weapon?.RefillRounds();
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
            CommandManager.HandleCommand(new MoveCommand(this, toX, toY));
            HasMoved = true;
        }

        public virtual IEnumerable<(int, int)> GetMoveArea()
        {
            List<(int, int, int)> coordsWithDist = GM.Map.GetArea(X, Y, MoveRange);
            coordsWithDist.RemoveAll(c => GM.Map.Fields[c.Item1, c.Item2] != null
                || c.Item3 - GetMovementBonus(GM.Map.TerrainTypes[c.Item1, c.Item2]) > MoveRange);
            return coordsWithDist.GetCoords();
        }

        protected virtual int GetMovementBonus(TerrainType terrainType)
        {
            return terrainType switch
            {
                TerrainType.Flat => 0,
                TerrainType.SparceForest => -1,
                TerrainType.DeepForest => -2,
                TerrainType.Mountain => -2,
                TerrainType.Water => Constants.ImpossibleValue,
                _ => throw new NotImplementedException(),
            };
        }

        public void TrySet(int atX, int atY)
        {
            if (!GM.Map.ContainsUnit(this) && GetSetArea().Contains((atX, atY)))
                Set(atX, atY);
        }

        private void Set(int toX, int toY)
        {
            CommandManager.HandleCommand(new SetUnitCommand(this, toX, toY));
        }

        public virtual IEnumerable<(int, int)> GetSetArea()
        {
            return GM.FieldOfView;
        }

        protected virtual int GetInvisibilityBonus(TerrainType terrainType)
        {
            return terrainType switch
            {
                TerrainType.Flat => 0,
                TerrainType.SparceForest => 1,
                TerrainType.DeepForest => 2,
                TerrainType.Mountain => 1,
                TerrainType.Water => 0,
                _ => throw new NotImplementedException(),
            };
        }

        public virtual IEnumerable<(int, int)> GetAttackArea()
        {
            return GM.Map.GetArea(X, Y, Weapon?.AttackRange ?? 0).GetCoords();
        }

        public virtual IEnumerable<(int, int)> GetAttackTargets()
        {
            List<(int, int, int)> coordsWithDist = GM.Map.GetArea(X, Y, Weapon?.AttackRange ?? 0);
            var result = coordsWithDist.Where(c => GM.Map.Fields[c.Item1, c.Item2] != null
                && GM.Map.Fields[c.Item1, c.Item2].Player != Player).GetCoords();   
            return result.Where(c => GM.CanCurrentPlayerSee(c));
        }

        public virtual IEnumerable<(int, int)> GetFieldOfView()
        {
            List<(int, int, int)> coordsWithDist = GM.Map.GetArea(X, Y, FieldOfView, true);
            return coordsWithDist.Where(c =>
                c.Item3 + GetInvisibilityBonus(GM.Map.TerrainTypes[c.Item1, c.Item2]) <= FieldOfView).GetCoords();
        }

        public void TryAttack(int toX, int toY)
        {
            if (CanAttack) Attack(toX, toY);
        }

        private void Attack(int atX, int atY)
        {
            Weapon.RemoveRound();
            CommandManager.HandleCommand(new AttackCommand(this, atX, atY, CalculateDamage(), Weapon.DamageType));
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
