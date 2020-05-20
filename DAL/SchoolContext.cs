using Microsoft.EntityFrameworkCore;
using EfGetStart2.Models;

namespace EfGetStart2.DAL
{
    public partial class SchoolContext: DbContext
    {
        public SchoolContext()
        {
        }
        public SchoolContext(DbContextOptions<SchoolContext> options): base(options)
        {    
        }
        public DbSet<Student> Students{get;set;}
        public DbSet<Enrollment> Enrollments{get;set;}
        public DbSet<Course> Courses{get;set;}
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                string path = "Data Source=.\\School.db;";
                optionsBuilder.UseSqlite(path);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity => 
            {
                // can in put the ID value: ValueGeneratedNever()
                // default: entity.Property(e => e.StudentId) => automaticaly generated value by DB 
                entity.Property(e => e.StudentId);
                entity.Property(e => e.FirstName).HasColumnType("VARCHAR(50)");
                entity.Property(e => e.LastName).HasColumnType("VARCHAR(50)");
                entity.Property(e => e.EnrollmentDate).HasColumnType("VARCHAR(50)");                
            });
            modelBuilder.Entity<Enrollment>(entity => 
            {
                // can in put the ID value: ValueGeneratedNever()
                // default: entity.Property(e => e.StudentId) => automaticaly generated value by DB
                entity.Property(e => e.EnrollmentID);
                entity.Property(e => e.CourseID).HasColumnType("VARCHAR(50)");
                entity.Property(e => e.StudentID).HasColumnType("VARCHAR(50)");
                entity.Property(e => e.Grade).HasColumnType("VARCHAR(10)");                
            });
            modelBuilder.Entity<Course>(entity => 
            {
                // can in put the ID value: ValueGeneratedNever()
                // default: entity.Property(e => e.StudentId) => automaticaly generated value by DB
                entity.Property(e => e.CourseID);
                entity.Property(e => e.Credits).HasColumnType("VARCHAR(50)");
                entity.Property(e => e.Title).HasColumnType("VARCHAR(50)");
                                
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder); 
    }
}