using Microsoft.EntityFrameworkCore;

namespace BAFLB.Data
{
    /// <summary>
    /// Содержит логику создания базы данных.
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Пользователи.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Раунды.
        /// </summary>
        public DbSet<Round> Rounds { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
