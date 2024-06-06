using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace backend.Model
{
    public partial class TMSBackupContext : DbContext
    {
        public TMSBackupContext()
        {
        }

        public TMSBackupContext(DbContextOptions<TMSBackupContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssignedProject> AssignedProjects { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\LocalSDB;Database=TMSBackup;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssignedProject>(entity =>
            {
                entity.HasKey(e => e.ProjectAssignedId)
                    .HasName("PK__Assigned__7BD78415E9523B53");

                entity.ToTable("AssignedProject");

                entity.Property(e => e.ProjectAssignedId).ValueGeneratedNever();

                entity.Property(e => e.IsLead).HasColumnName("isLead");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.AssignedProjects)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AssignedP__Proje__412EB0B6");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AssignedProjects)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AssignedP__userI__403A8C7D");
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

                entity.Property(e => e.AvatarUrl)
                    .HasMaxLength(500)
                    .HasColumnName("avatarUrl");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Projectkey)
                    .HasMaxLength(50)
                    .HasColumnName("projectkey");

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

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.RoomName)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.SessionId).HasMaxLength(100);

                entity.Property(e => e.Token).IsUnicode(false);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.TicketId)
                    .ValueGeneratedNever()
                    .HasColumnName("ticketId");

                entity.Property(e => e.AssignedTo).HasColumnName("assignedTo");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.ProjectId).HasColumnName("projectId");

                entity.Property(e => e.ReportedBy).HasColumnName("reportedBy");

                entity.Property(e => e.Ticketdescription)
                    .HasMaxLength(500)
                    .HasColumnName("ticketdescription");

                entity.Property(e => e.Ticketpriority)
                    .HasMaxLength(50)
                    .HasColumnName("ticketpriority");

                entity.Property(e => e.Ticketstatus)
                    .HasMaxLength(50)
                    .HasColumnName("ticketstatus");

                entity.Property(e => e.Ticketsummary)
                    .HasMaxLength(100)
                    .HasColumnName("ticketsummary");

                entity.Property(e => e.Tickettype)
                    .HasMaxLength(50)
                    .HasColumnName("tickettype");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.AssignedToNavigation)
                    .WithMany(p => p.TicketAssignedToNavigations)
                    .HasForeignKey(d => d.AssignedTo)
                    .HasConstraintName("FK__Tickets__assigne__4222D4EF");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tickets__project__440B1D61");

                entity.HasOne(d => d.ReportedByNavigation)
                    .WithMany(p => p.TicketReportedByNavigations)
                    .HasForeignKey(d => d.ReportedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tickets__reporte__4316F928");
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
                    .HasConstraintName("FK__USERS__roleId__44FF419A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
