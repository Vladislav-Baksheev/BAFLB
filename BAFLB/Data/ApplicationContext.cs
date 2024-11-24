using Microsoft.EntityFrameworkCore;

namespace BAFLB.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null;

        public DbSet<Round> Rounds { get; set; }

        public ApplicationContext() 
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=baflb;Username=postgres;Password=Vladik@2003");
        }
    }
}
