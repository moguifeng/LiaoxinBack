using System;

namespace Zzb
{
    public class ZzbException : Exception
    {
        public ZzbException()
        {
        }

        public ZzbException(string message) : base(message)
        {
        }

        public ZzbException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}