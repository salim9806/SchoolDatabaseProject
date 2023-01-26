using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DataAccess.DataEntities;

namespace DataAccess
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Occupation> Occupations { get; set; } = null!;
        public virtual DbSet<Personnel> Personnel { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
            
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-HE6C0TMN\\SQLEXPRESS;Initial Catalog=School2Database;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.ClassName)
                    .HasName("PK__Class__F8BF561A1B51A8C4");

                entity.ToTable("Class");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseName)
                    .HasName("PK__Course__9526E276F265C99F");

                entity.ToTable("Course");

                entity.Property(e => e.CourseName).HasMaxLength(10);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepartmentName)
                    .HasName("PK__Departme__D949CC35EE53E888");

                entity.ToTable("Department");

                entity.Property(e => e.DepartmentName).HasMaxLength(20);
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.TakenCourse })
                    .HasName("PK__Grade__43F4B9CAF1916A7C");

                entity.ToTable("Grade");

                entity.Property(e => e.TakenCourse).HasMaxLength(10);

                entity.Property(e => e.Rating)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AppointedByNavigation)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.AppointedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Grade__Appointed__398D8EEE");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Grade__StudentId__37A5467C");

                entity.HasOne(d => d.TakenCourseNavigation)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.TakenCourse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Grade__TakenCour__38996AB5");
            });

            modelBuilder.Entity<Occupation>(entity =>
            {
                entity.HasKey(e => e.Title)
                    .HasName("PK__Occupati__2CB664DD617206A6");

                entity.ToTable("Occupation");

                entity.Property(e => e.Title).HasMaxLength(15);
            });

            modelBuilder.Entity<Personnel>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(10);

                entity.Property(e => e.LastName).HasMaxLength(10);

                entity.Property(e => e.StartedDate).HasColumnType("date");

                entity.Property(e => e.WorksInDepartment).HasMaxLength(20);

                entity.HasOne(d => d.WorksInDepartmentNavigation)
                    .WithMany(p => p.Personnel)
                    .HasForeignKey(d => d.WorksInDepartment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Personnel__Works__267ABA7A");

                entity.HasMany(d => d.OccupationTitles)
                    .WithMany(p => p.Personnel)
                    .UsingEntity<Dictionary<string, object>>(
                        "PersonnelOccupation",
                        l => l.HasOne<Occupation>().WithMany().HasForeignKey("OccupationTitle").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Personnel__Occup__2D27B809"),
                        r => r.HasOne<Personnel>().WithMany().HasForeignKey("PersonnelId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Personnel__Perso__2C3393D0"),
                        j =>
                        {
                            j.HasKey("PersonnelId", "OccupationTitle").HasName("PK__Personne__8848C1D1E300BD9E");

                            j.ToTable("PersonnelOccupation");

                            j.IndexerProperty<string>("OccupationTitle").HasMaxLength(15);
                        });
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.BelongToClass)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(10);

                entity.Property(e => e.LastName).HasMaxLength(10);

                entity.Property(e => e.SocialSecurity).HasColumnType("numeric(4, 0)");

                entity.HasOne(d => d.BelongToClassNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.BelongToClass)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Student__BelongT__31EC6D26");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
