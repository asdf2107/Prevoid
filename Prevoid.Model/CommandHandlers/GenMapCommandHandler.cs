using Prevoid.Model.Commands;
using Prevoid.Model.MapGeneration.MapGenStrategies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Model.CommandHandlers
{
    public class GenMapCommandHandler : CommandHandler
    {
        public static event Action NeedGenMapCommandRender;

        public GenMapCommandHandler() : base(typeof(GenMapCommand)) { }

        public override void Handle(Command command)
        {
            var genMapCommand = (GenMapCommand)command;

            new DefaultMapGenStrategy().GenMap(GM.Map, genMapCommand.Seed);

            if (genMapCommand.NeedRender) NeedGenMapCommandRender?.Invoke();
        }
    }
}
