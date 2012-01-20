using System;

namespace RecreateMe.Exceptions.Formatting
{
    public class BadFormatException : Exception
    {
        public BadFormatException(string message) : base(message)
        {
        }
    }
}