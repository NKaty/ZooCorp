using System;

namespace ZooCorp.BusinessLogic.Exceptions
{
    public class NoAvailableSpaceException : Exception
    {
        public NoAvailableSpaceException(string message) : base(message)
        {
        }
    }
}