using BirthdayTracker.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BirthdayTracker.Database
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
                .HasOne(b => b.BirthdayInfo)
                .WithOne(i => i.User)
                .HasForeignKey<BirthdayInfo>(b => b.UserId);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<BirthdayInfo>().ToTable("BirthdayInfo");
        }

        public void AddUserWithBirthdayInfo(User userData, BirthdayInfo birthdayInfo)
        {
            using var contextTransaction = Database.BeginTransaction();

            userData.BirthdayInfo = birthdayInfo;
            
            User.Add(userData);
        
            SaveChanges();

            contextTransaction.Commit();
        }

    }
}
