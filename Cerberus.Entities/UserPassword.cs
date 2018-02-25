namespace Cerberus.Entities
{
    using System;

    public class UserPassword
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Password { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public string LastUpdatedBy { get; set; }
    }
}
