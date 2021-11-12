using System;
using System.Collections.Generic;
using System.Text;

namespace Prevoid.Model
{
    public static class GM
    {
        public static event Action TurnChanged;

        public static Map Map;
        public static readonly Random Random = new Random();
        public static readonly CommandHandler CommandHandler = new CommandHandler();
    }
}
