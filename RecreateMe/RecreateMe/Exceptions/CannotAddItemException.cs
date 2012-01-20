using System;

namespace RecreateMe.Exceptions
{
    public class CannotAddItemException : Exception
    {
        public CannotAddItemException(string message) : base(message)
        {
        }
    }
}