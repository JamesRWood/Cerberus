namespace Cerberus.Entities
{
    using System;

    public class User
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string PrimaryEmail { get; set; }

        public string SecondaryEmail { get; set; }

        public bool AccountLocked { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public string LastUpdatedBy { get; set; }
    }
}
