using System;

namespace ZooCorp.BusinessLogic.Exceptions
{
    public class UnknownAnimalException : Exception
    {
        public UnknownAnimalException(string message) : base(message)
        {
        }
    }
}