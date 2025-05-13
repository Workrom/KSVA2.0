using System;
using System.Collections.Generic;
using KSVA2._0_WPF.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace KSVA2._0_WPF.Services;

public partial class ApplicationContext : DbContext
{
    private string spasss = Environment.GetEnvironmentVariable("spass");
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<order> orders { get; set; }

    public virtual DbSet<review> reviews { get; set; }

    public virtual DbSet<teacher_profile> teacher_profiles { get; set; }

    public virtual DbSet<user> users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql($"server=localhost;user=adminuser;password={spasss};database=ksva", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.41-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<order>(entity =>
        {
            entity.HasKey(e => e.order_id).HasName("PRIMARY");

            entity.HasIndex(e => e.teacher_id, "fk_orders_teacher_profile1_idx");

            entity.HasIndex(e => e.student_id, "fk_orders_users1_idx");

            entity.Property(e => e.scheduled_date).HasColumnType("datetime");
            entity.Property(e => e.status).HasColumnType("enum('AWAIT','ONGOING','DONE','LATE')");
            entity.Property(e => e.subject).HasMaxLength(100);

            entity.HasOne(d => d.student).WithMany(p => p.orders)
                .HasForeignKey(d => d.student_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders_users1");

            entity.HasOne(d => d.teacher).WithMany(p => p.orders)
                .HasForeignKey(d => d.teacher_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders_teacher_profile1");
        });

        modelBuilder.Entity<review>(entity =>
        {
            entity.HasKey(e => e.review_id).HasName("PRIMARY");

            entity.HasIndex(e => e.order_id, "fk_reviews_orders1_idx");

            entity.HasIndex(e => e.teacher_id, "fk_reviews_teacher_profile1_idx");

            entity.HasIndex(e => e.student_id, "fk_reviews_users1_idx");

            entity.Property(e => e.date).HasColumnType("datetime");
            entity.Property(e => e.review_text).HasMaxLength(400);

            entity.HasOne(d => d.order).WithMany(p => p.reviews)
                .HasForeignKey(d => d.order_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_reviews_orders1");

            entity.HasOne(d => d.student).WithMany(p => p.reviews)
                .HasForeignKey(d => d.student_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_reviews_users1");

            entity.HasOne(d => d.teacher).WithMany(p => p.reviews)
                .HasForeignKey(d => d.teacher_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_reviews_teacher_profile1");
        });

        modelBuilder.Entity<teacher_profile>(entity =>
        {
            entity.HasKey(e => e.teacher_id).HasName("PRIMARY");

            entity.ToTable("teacher_profile");

            entity.Property(e => e.teacher_id).ValueGeneratedOnAdd();
            entity.Property(e => e.expertise).HasColumnType("enum('high school','university','scientist')");
            entity.Property(e => e.price).HasPrecision(10, 2);
            entity.Property(e => e.subject).HasMaxLength(100);

            entity.HasOne(d => d.teacher).WithOne(p => p.teacher_profile)
                .HasForeignKey<teacher_profile>(d => d.teacher_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_teacher_profile_users");
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.user_id).HasName("PRIMARY");

            entity.Property(e => e.name).HasMaxLength(100);
            entity.Property(e => e.password).HasMaxLength(255);
            entity.Property(e => e.phone).HasMaxLength(20);
            entity.Property(e => e.role).HasColumnType("enum('student','teacher')");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
