using System;

namespace ZooCorp.BusinessLogic.Exceptions
{
    public class NoNeededExperienceException : Exception
    {
        public NoNeededExperienceException(string message) : base(message)
        {
        }
    }
}