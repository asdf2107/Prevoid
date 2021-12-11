using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Prevoid.Network
{
    public class Connection
    {
        public Socket Remote { get; private set; }
        private static readonly Encoding _Encoding = Encoding.UTF8;
        private const int _Port = 11000;
        private const int _MessageMaxSize = 2048;

        /// <summary>
        /// Create connection as a server.
        /// </summary>
        public Connection()
        {
            throw new NotImplementedException();    
        }

        /// <summary>
        /// Create connection as a client.
        /// </summary>
        /// <param name="ip">Server's IP</param>
        public Connection(string ip)
        {
            Remote = new Socket(SocketType.Stream, ProtocolType.Tcp);
            Remote.Connect(ip, _Port);
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
