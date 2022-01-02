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
            _Listener = new Socket(Constants.AddressFamily, Constants.SocketType, Constants.ProtocolType);
            _Listener.Bind(new IPEndPoint(GetLocalIP(), Constants.Port));
            _Listener.Listen();
            Remote = _Listener.Accept();
        }

        public IPAddress GetLocalIP()
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
        }
    }
}
