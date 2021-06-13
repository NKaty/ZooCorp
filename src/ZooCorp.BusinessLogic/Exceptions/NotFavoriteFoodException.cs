using System;

namespace ZooCorp.BusinessLogic.Exceptions
{
    public class NotFavoriteFoodException : Exception
    {
        public NotFavoriteFoodException(string message) : base(message)
        {
        }
    }
}