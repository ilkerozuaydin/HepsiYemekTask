using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
   public class RedisClientSettings
    {
        public string Ip { get; set; }
        public string Port { get; set; }
    }
}
