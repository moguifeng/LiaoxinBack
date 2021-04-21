using System;

namespace Zzb.Common
{
    public class RandomHelper
    {
        private static object _lock = new object();

        private static int _count = 1;

        private static Random _random = new Random();

        public static int Next(int min, int max)
        {
            lock (_random)
            {
                return _random.Next(min, max);
            }
        }

        public static int Next(int max)
        {
            lock (_random)
            {
                return _random.Next(max);
            }
        }

        public static double NextDouble()
        {
            lock (_random)
            {
                return _random.NextDouble();
            }
        }

        public static string GetRandom(string head)
        {
            lock (_lock)
            {
                if (_count >= 10000)
                {
                    _count = 1;
                }
                var number = head + DateTime.Now.ToString("yyMMddHHmmss") + _count.ToString("0000");
                _count++;
                return number;
            }
        }
    }
}