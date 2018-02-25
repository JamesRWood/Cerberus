namespace Cerberus.DatabaseContext.Interfaces
{
    using Cerberus.Entities;
    using Hades.DataAccess.Contracts;
    using Microsoft.EntityFrameworkCore;

    public interface ICerberusDbContext : IDbContext<ICerberusDbToken>
    {
        DbSet<User> User { get; set; }

        DbSet<UserLoginEvent> UserLoginEvent { get; set; }

        DbSet<UserPassword> UserPassword { get; set; }
    }
}
