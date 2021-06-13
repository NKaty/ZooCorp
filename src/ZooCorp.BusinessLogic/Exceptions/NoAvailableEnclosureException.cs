using System;

namespace ZooCorp.BusinessLogic.Exceptions
{
    public class NoAvailableEnclosureException : Exception
    {
        public NoAvailableEnclosureException(string message) : base(message)
        {
        }
    }
}