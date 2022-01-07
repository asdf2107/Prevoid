using Prevoid.Model.Commands;
using Prevoid.Model.MapGeneration.MapGenStrategies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Model.CommandHandlers
{
    public class SetUnitCommandHandler : CommandHandler
    {
        public static event Action<SetUnitCommand> NeedSetUnitCommandRender;

        public SetUnitCommandHandler() : base(typeof(SetUnitCommand)) { }

        public override void Handle(Command command)
        {
            var setUnitCommand = (SetUnitCommand)command;

            GM.Map.SetUnit(setUnitCommand.Unit, setUnitCommand.AtX, setUnitCommand.AtY);

            if (setUnitCommand.NeedRender) NeedSetUnitCommandRender?.Invoke(setUnitCommand);
        }
    }
}
