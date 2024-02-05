using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace backend.Model
{
    public partial class TestDBContext : DbContext
    {
        public TestDBContext()
        {
        }

        public TestDBContext(DbContextOptions<TestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssignedProject> AssignedProjects { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=HASSANSIDDIQUI\\SQLEXPRESS; database=TestDB; trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssignedProject>(entity =>
            {
                entity.HasKey(e => e.ProjectAssignedId)
                    .HasName("PK__Assigned__7BD7841502347B7E");

                entity.ToTable("AssignedProject");

                entity.Property(e => e.ProjectAssignedId).ValueGeneratedNever();

                entity.Property(e => e.IsLead).HasColumnName("isLead");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.AssignedProjects)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AssignedP__Proje__503BEA1C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AssignedProjects)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AssignedP__userI__4F47C5E3");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.BookId)
                    .ValueGeneratedNever()
                    .HasColumnName("bookId");

                entity.Property(e => e.BookAuthor)
                    .HasMaxLength(200)
                    .HasColumnName("bookAuthor");

                entity.Property(e => e.BookDescription).HasColumnName("bookDescription");

                entity.Property(e => e.BookName)
                    .HasMaxLength(500)
                    .HasColumnName("bookName");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.ImgUrl)
                    .HasMaxLength(500)
                    .HasColumnName("imgUrl");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedAt");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("PROJECTS");

                entity.Property(e => e.ProjectId).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Projectname).HasMaxLength(200);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedAt");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("roleId");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(100)
                    .HasColumnName("roleName");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("userId");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(200)
                    .HasColumnName("fullname");

                entity.Property(e => e.Pass)
                    .HasMaxLength(500)
                    .HasColumnName("pass");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__USERS__roleId__3E1D39E1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
