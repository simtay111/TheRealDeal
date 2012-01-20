using System;

namespace RecreateMe.Exceptions
{
    public class NotEnoughInfoException : Exception
    {
        public NotEnoughInfoException(string message) : base(message)
        {
        }
    }
}