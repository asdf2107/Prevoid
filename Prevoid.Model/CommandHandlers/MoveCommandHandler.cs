using Prevoid.Model.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Model.CommandHandlers
{
    public class MoveCommandHandler : CommandHandler
    {
        public static event Action<MoveCommand> NeedMoveCommandRender;

        public MoveCommandHandler() : base(typeof(MoveCommand)) { }

        public override void Handle(Command command)
        {
            var moveCommand = (MoveCommand)command;

            GM.Map.Fields[moveCommand.Unit.X, moveCommand.Unit.Y] = null;
            moveCommand.Unit.SetCoords(moveCommand.ToX, moveCommand.ToY);
            GM.Map.Fields[moveCommand.ToX, moveCommand.ToY] = moveCommand.Unit;

            if (moveCommand.NeedRender) NeedMoveCommandRender?.Invoke(moveCommand);
        }
    }
}
