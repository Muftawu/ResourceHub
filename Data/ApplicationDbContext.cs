using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using group4.Models;

namespace group4.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Resource> Resources { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId)
                .HasName("PK3")
                .IsClustered(false);

            entity.ToTable("Course");

            entity.Property(e => e.CourseId).HasColumnName("Course_ID");
            entity.Property(e => e.CourseName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DepartmentId).HasColumnName("Department_ID");

            entity.HasOne(d => d.Department).WithMany(p => p.Courses)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefDepartment2");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId)
                .HasName("PK2")
                .IsClustered(false);

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentId).HasColumnName("Department_ID");
            entity.Property(e => e.DepName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Dep_Name");
        });

        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => new { e.ResourceId, e.UserId })
                .HasName("PK4")
                .IsClustered(false);

            entity.ToTable("Resource");

            entity.Property(e => e.ResourceId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Resource_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
            entity.Property(e => e.Comment)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CourseId).HasColumnName("Course_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Resource1)
                .HasMaxLength(255)
                .HasColumnName("Resource");
            entity.Property(e => e.Topic)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Course).WithMany(p => p.Resources)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefCourse3");

            entity.HasOne(d => d.User).WithMany(p => p.Resources)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefUsers1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId)
                .HasName("PK1")
                .IsClustered(false);

            entity.Property(e => e.UserId).HasColumnName("User_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
