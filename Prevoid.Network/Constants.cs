using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Prevoid.Network
{
    public static class Constants
    {
        public static readonly AddressFamily AddressFamily = AddressFamily.InterNetwork;
        public static readonly SocketType SocketType = SocketType.Stream;
        public static readonly ProtocolType ProtocolType = ProtocolType.Tcp;

        public const int Port = 11000;
    }
}
