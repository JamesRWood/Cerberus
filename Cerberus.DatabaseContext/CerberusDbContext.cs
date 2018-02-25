namespace Cerberus.DatabaseContext
{
    using Cerberus.DatabaseContext.Interfaces;
    using Cerberus.Entities;
    using Microsoft.EntityFrameworkCore;

    public class CerberusDbContext : ICerberusDbContext
    {
        public DbSet<User> User { get; set; }

        public DbSet<UserLoginEvent> UserLoginEvent { get; set; }

        public DbSet<UserPassword> UserPassword { get; set; }
    }
}
