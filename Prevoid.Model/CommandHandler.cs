using Prevoid.Model.Commands;
using System;
using System.Collections.Generic;

namespace Prevoid.Model
{
    public class CommandHandler
    {
        public event Action<MoveCommand> NeedMoveCommandRender;
        public event Action<AttackCommand> NeedAttackCommandRender;
        public event Action<SetUnitCommand> NeedSetUnitCommandRender;

        public List<Command> TurnCommands { get; private set; } = new();

        public CommandHandler()
        {
            GM.TurnChanged += () => TurnCommands.Clear();
        }

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
            else if (command is SetUnitCommand setUnitCommand)
            {
                SetUnit(setUnitCommand);
                if (command.NeedRender) NeedSetUnitCommandRender?.Invoke(setUnitCommand);
            }
            else
            {
                throw new NotImplementedException();
            }

            TurnCommands.Add(command);
        }

        private void MoveUnit(MoveCommand command)
        {
            GM.Map.Fields[command.Unit.X, command.Unit.Y] = null;
            command.Unit.SetCoords(command.ToX, command.ToY);
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

        private void SetUnit(SetUnitCommand command)
        {
            GM.Map.SetUnit(command.Unit, command.AtX, command.AtY);
        }

    }
}
