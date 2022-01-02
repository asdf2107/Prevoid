using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Prevoid.Network.Connections
{
    public class Connection
    {
        protected Socket Remote { get; set; }
        private static readonly Encoding _Encoding = Encoding.UTF8;
        private const int _MessageMaxSize = 2048;

        /// <summary>
        /// Create connection as a server.
        /// </summary>
        protected Connection() { }

        /// <summary>
        /// Create connection as a client.
        /// </summary>
        /// <param name="ip">Server's IP</param>
        public Connection(string ip)
        {
            Remote = new Socket(Constants.AddressFamily, Constants.SocketType, Constants.ProtocolType);
            Remote.Connect(ip, Constants.Port);
        }

        public async Task SendText(string text)
        {
            await Task.Run(() => Remote.Send(_Encoding.GetBytes(text)));
        }

        public string RecieveText()
        {
            byte[] buffer = new byte[_MessageMaxSize];
            Remote.Receive(buffer);
            return _Encoding.GetString(buffer);
        }
    }
}
