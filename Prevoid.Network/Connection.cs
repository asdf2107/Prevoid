using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Network
{
    public class Connection
    {
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
            throw new NotImplementedException();
        }

        public async Task SendText(string text)
        {
            throw new NotImplementedException();
        }

        public string RecieveText()
        {
            throw new NotImplementedException();
        }
    }
}
