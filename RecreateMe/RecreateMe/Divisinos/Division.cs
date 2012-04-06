using System;

namespace RecreateMe.Divisinos
{
    public class Division
    {
        public Division()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string Id { get; set; }
    }
}