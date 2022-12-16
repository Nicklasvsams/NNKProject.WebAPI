using Microsoft.EntityFrameworkCore;
using NNKProject.DAL.Database.Entities;

namespace NNKProject.DAL.Database
{
    public class GameDBContext : DbContext
    {
        public GameDBContext() { }

        public GameDBContext(DbContextOptions<GameDBContext> options) : base(options) { }

        public DbSet<AccountEntity> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountEntity>()
                .HasIndex(l => l.Name)
                .IsUnique();

            modelBuilder.Entity<AccountEntity>().HasData(
                new AccountEntity()
                {
                    Id = 1,
                    Name= "Test",
                    Password = "Passw0rd",
                    SaveData = "UNDEFINED" // TODO: Figure out how we manage savestates
                });
        }
    }
}
