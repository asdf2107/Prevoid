using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Model.EventArgs
{
    public class TurnEndedEventArgs
    {
        public IEnumerable<Command> Commands { get; set; }
        public bool SendToOtherOnlinePlayer { get; set; }

        public TurnEndedEventArgs() { }

        public TurnEndedEventArgs(IEnumerable<Command> commands, bool sendToOtherOnlinePlayer = true)
        {
            Commands = commands;
            SendToOtherOnlinePlayer = sendToOtherOnlinePlayer;
        }

    }
}
