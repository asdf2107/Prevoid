using Prevoid.Model.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prevoid.Model
{
    public class Unit : ILocateable
    {
        public int X { get; set; } = -1;
        public int Y { get; set; } = -1;
        public Player Player { get; private set; }
        public bool CanMove { get; protected set; }
        public int MoveRange { get; protected set; }
        public Weapon Weapon { get; protected set; }
        public float MaxHp { get; protected set; }
        public float Hp { get; protected set; }


        protected List<Effect> Effects = new List<Effect>();

        public Unit(Player player, bool canMove, int moveRange, float maxHP, Weapon weapon = null)
        {
            Player = player;

            CanMove = canMove;
            MoveRange = moveRange;

            MaxHp = maxHP;
            Hp = MaxHp;
            Weapon = weapon;
        }

        public void Move(int toX, int toY)
        {
            GM.CommandHandler.HandleCommand(new MoveCommand()
            {
                Unit = this,
                ToX = toX,
                ToY = toY,
            });
        }

        public void Attack(int atX, int atY)
        {
            GM.CommandHandler.HandleCommand(new AttackCommand()
            {
                Unit = this,
                AtX = atX,
                AtY = atY,
                Damage = CalculateDamage(),
                DamageType = Weapon.DamageType,
            });
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
            throw new NotImplementedException(); // ?
        }

        private float CalculateDamage()
        {
            return GM.Random.NextFloat(Weapon.Damage * (1 - Constants.DamageDelta), Weapon.Damage * (1 + Constants.DamageDelta));
        }
    }
}
