using System;

namespace RecreateMe.Organizations
{
    public class Organization
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public string CreatorId { get; set; }

        public Organization()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}