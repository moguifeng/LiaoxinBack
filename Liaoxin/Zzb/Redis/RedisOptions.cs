using System;
using System.Collections.Generic;
using System.Text;

namespace Zzb.Redis
{
    public class RedisOptions
    {
        public string[] Endpoints { get; set; }
        public bool AllowAdmin { get; set; } = false;
        public bool AbortOnConnectFail { get; set; } = true;
        public int ConnectTimeout { get; set; } = 2000;
        public int SyncTimeout { get; set; } = 2000;
        public int Db { get; set; } = 0;

        public string Password { get; set; } = null;
    }
}
