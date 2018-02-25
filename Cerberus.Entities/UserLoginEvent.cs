namespace Cerberus.Entities
{
    using System;

    public class UserLoginEvent
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime LastSuccessfulLogin { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public string LastUpdatedBy { get; set; }
    }
}
