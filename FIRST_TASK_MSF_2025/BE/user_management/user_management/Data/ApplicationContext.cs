using Microsoft.EntityFrameworkCore;
using user_management.Models;

namespace user_management.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) {}

        #region DbSet
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BlaskListToken> BlaskListTokens { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<ApiLog> ApiLogs { get; set; }

        #endregion

    }
}
