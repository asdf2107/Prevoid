using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Prevoid.Network.Connections
{
    public class ServerConnection : Connection
    {
        private Socket _Listener;

        /// <summary>
        /// Create connection as a server.
        /// </summary>
        public ServerConnection() : base()
        {
            var localIP = GetLocalIP();
            _Listener = new Socket(localIP.AddressFamily, Constants.SocketType, Constants.ProtocolType);
            _Listener.Bind(new IPEndPoint(localIP, Constants.Port));
            _Listener.Listen();
            Remote = _Listener.Accept();
        }
    }
}
