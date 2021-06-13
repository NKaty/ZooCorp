using System;

namespace ZooCorp.BusinessLogic.Exceptions
{
    public class UnknownEmployeeException : Exception
    {
        public UnknownEmployeeException(string message) : base(message)
        {
        }
    }
}