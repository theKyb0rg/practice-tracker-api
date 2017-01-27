using Microsoft.EntityFrameworkCore;
using musicians_practice_log_api.Models;

namespace PracticeLog.Models
{
    public class PracticeLogContext : DbContext
    {
        public PracticeLogContext(DbContextOptions<PracticeLogContext> options) : base(options)
        {

        }

        public DbSet<Category> Categorys { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<PracticeSession> PracticeSessions { get; set; }
        public DbSet<Riff> Riffs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Categorys");
            modelBuilder.Entity<Exercise>().ToTable("Exercises");
            modelBuilder.Entity<Experience>().ToTable("Experience");
            modelBuilder.Entity<Instrument>().ToTable("Instruments");
            modelBuilder.Entity<PracticeSession>().ToTable("PracticeSessions");
            modelBuilder.Entity<Riff>().ToTable("Riffs");
            modelBuilder.Entity<User>().ToTable("Users");
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Filename=PracticeLog.db");
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .HasMany(u => u.Instruments)
        //        .WithOne(u => u.User)
        //        .HasForeignKey(u => u.UserId);

        //    modelBuilder.Entity<Instrument>()
        //        .HasMany(i => i.Categories)
        //        .WithOne(i => i.Instrument)
        //        .HasForeignKey(i => i.InstrumentId);

        //    modelBuilder.Entity<Instrument>()
        //        .HasOne(i => i.Experience)
        //        .WithOne(i => i.Instrument)
        //        .HasForeignKey<Experience>(i => i.InstrumentId);

        //    modelBuilder.Entity<Category>()
        //        .HasMany(i => i.Exercises)
        //        .WithOne(i => i.Category)
        //        .HasForeignKey(i => i.CategoryId);

        //    modelBuilder.Entity<Exercise>()
        //       .HasMany(i => i.PracticeSessions)
        //       .WithOne(i => i.Exercise)
        //       .HasForeignKey(i => i.ExerciseId);
        //}
    }
}
