using System;

namespace ZooCorp.BusinessLogic.Exceptions
{
    public class NotNeededMedicineException : Exception
    {
        public NotNeededMedicineException(string message) : base(message)
        {
        }
    }
}