using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
            var remoteIP = IPAddress.Parse(ip);
            Remote = new Socket(remoteIP.AddressFamily, Constants.SocketType, Constants.ProtocolType);
            Remote.Connect(remoteIP, Constants.Port);
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

        public static IPAddress GetLocalIP()
        {
            foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }

            throw new Exception("No network adapters with an IPv4 address in the system");
        }
    }
}
