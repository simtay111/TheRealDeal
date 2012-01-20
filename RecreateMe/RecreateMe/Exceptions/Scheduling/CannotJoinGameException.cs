using System;

namespace RecreateMe.Exceptions.Scheduling
{
    public class CannotJoinGameException : Exception
    {
        public CannotJoinGameException(string message) : base(message)
        {
        }
    }
}