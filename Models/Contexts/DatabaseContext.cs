using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MedChart.Models.Contexts
{
    public partial class DatabaseContext : DbContext
    {
        public DbSet<BloodPressure> BloodPressures { get; set; }

        /// <summary>
        /// Default connection string
        /// </summary>
        public static string ConnectionString { get; set; } = @"Name=DefaultConnection";

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        partial void CustomInit(DbContextOptionsBuilder optionsBuilder);
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            CustomInit(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<BloodPressure>().ToTable("BloodPressures").HasKey(t => t.Id);
            modelBuilder.Entity<BloodPressure>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<BloodPressure>().Property(t => t.CreatedDate).HasDefaultValueSql("GetUtcDate()");
            modelBuilder.Entity<BloodPressure>().Property(t => t.CreatedDate).HasColumnType("DateTime");
            modelBuilder.Entity<BloodPressure>().Property(t => t.ExamDate).HasColumnType("DateTime").IsRequired();
            modelBuilder.Entity<BloodPressure>().Property(t => t.ExamDate).HasColumnType("DateTime");
            modelBuilder.Entity<BloodPressure>().Property(t => t.SystolicReading).IsRequired();
            modelBuilder.Entity<BloodPressure>().Property(t => t.DiastolicReading).IsRequired();
            modelBuilder.Entity<BloodPressure>().Property(t => t.HeartRate).IsRequired();        
        }
    }
}
