using Prevoid.Model.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prevoid.Model
{
    public class CommandHandler
    {
        public event Action<MoveCommand> NeedMoveCommandRender;
        public event Action<AttackCommand> NeedAttackCommandRender;

        public void HandleCommand(Command command)
        {
            if (command is MoveCommand moveCommand)
            {
                MoveUnit(moveCommand);
                if (command.NeedRender) NeedMoveCommandRender?.Invoke(moveCommand);
            }
            else if (command is AttackCommand attackCommand)
            {
                AttackUnit(attackCommand);
                if (command.NeedRender) NeedAttackCommandRender?.Invoke(attackCommand);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void MoveUnit(MoveCommand command)
        {
            GM.Map.Fields[command.Unit.X, command.Unit.Y] = null;
            command.Unit.X = command.ToX;
            command.Unit.Y = command.ToY;
            GM.Map.Fields[command.ToX, command.ToY] = command.Unit;
        }

        private void AttackUnit(AttackCommand command)
        {
            if (command.DamageType == DamageType.Point)
            {
                GM.Map.Fields[command.AtX, command.AtY]?.Harm(command.Damage);
            }
            else throw new NotImplementedException();
        }
    }
}
