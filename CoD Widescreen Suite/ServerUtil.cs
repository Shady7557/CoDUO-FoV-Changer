using ShadyPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoD_Widescreen_Suite
{
    internal class ServerUtil
    {

        public static string GetIpAndPort(Server server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            var sb = Pool.Get<StringBuilder>();

            try
            {
                return sb
                    .Clear()
                    .Append(server.Ip)
                    .Append(server.Port)
                    .ToString();
            }
            finally { Pool.Free(ref sb); }

        }

    }
}
