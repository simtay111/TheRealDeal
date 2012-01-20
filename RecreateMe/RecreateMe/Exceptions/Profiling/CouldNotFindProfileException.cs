using System;

namespace RecreateMe.Exceptions.Profiling
{
    public class CouldNotFindProfileException : Exception
    {
        public CouldNotFindProfileException(string message) : base(message)
        {
        }
    }
}