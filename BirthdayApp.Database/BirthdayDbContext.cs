using BirthdayApp.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BirthdayApp.Database
{
    public class BirthdayDbContext : DbContext
    {
        public BirthdayDbContext(DbContextOptions<BirthdayDbContext> options)
            : base(options)
        { }

        public DbSet<User> User { get; set; }
        public DbSet<BirthdayInfo> BirthdayInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(a => a.BirthdayInfo).WithOne(b => b.User)
                .HasForeignKey<BirthdayInfo>(e => e.BirthdayId);
            
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<BirthdayInfo>().ToTable("BirthdayInfo");
        }

    }
}
