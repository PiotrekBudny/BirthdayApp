using System.Reflection;
using BirthdayApp.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BirthdayApp.Database
{
    public class BirthdayDbContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public BirthdayDbContext(DbContextOptions<BirthdayDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<User> User { get; set; }
        public DbSet<BirthdayInfo> BirthdayInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Configuration.GetConnectionString("BirthdayDb"));

            base.OnConfiguring(optionsBuilder);            
        }

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
