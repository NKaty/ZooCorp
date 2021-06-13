using System;

namespace ZooCorp.BusinessLogic.Exceptions
{
    public class NotFriendlyAnimalException : Exception
    {
        public NotFriendlyAnimalException(string message) : base(message)
        {
        }
    }
}