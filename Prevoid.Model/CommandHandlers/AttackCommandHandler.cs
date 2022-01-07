using Prevoid.Model.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Model.CommandHandlers
{
    public class AttackCommandHandler : CommandHandler
    {
        public static event Action<AttackCommand> NeedAttackCommandRender;

        public AttackCommandHandler() : base(typeof(AttackCommand)) { }

        public override void Handle(Command command)
        {
            var attackCommand = (AttackCommand)command;

            if (attackCommand.DamageType == DamageType.Point)
            {
                GM.Map.Fields[attackCommand.AtX, attackCommand.AtY]?.Harm(attackCommand.Damage);
            }
            else throw new NotImplementedException();

            if (attackCommand.NeedRender) NeedAttackCommandRender?.Invoke(attackCommand);
        }
    }
}
