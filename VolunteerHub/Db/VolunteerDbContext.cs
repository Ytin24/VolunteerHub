using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VolunteerHub.Models;

namespace VolunteerHub.Db;

public partial class VolunteerDbContext : DbContext {
    public VolunteerDbContext() {
    }

    public VolunteerDbContext(DbContextOptions<VolunteerDbContext> options)
        : base(options) {
    }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectParticipation> ProjectParticipations { get; set; }

    public virtual DbSet<ProjectStatus> ProjectStatuses { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(new ConnectionStringProvider().GerMssqlConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Project>(entity => {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__BC799E1F3E6DEB12");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("end_date");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(100)
                .HasColumnName("project_name");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("start_date");

            entity.HasMany(d => d.Statuses).WithMany(p => p.Projects)
                .UsingEntity<Dictionary<string, object>>(
                    "ProjectProjectStatus",
                    r => r.HasOne<ProjectStatus>().WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProjectPr__statu__4222D4EF"),
                    l => l.HasOne<Project>().WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProjectPr__proje__412EB0B6"),
                    j => {
                        j.HasKey("ProjectId", "StatusId").HasName("PK__ProjectP__BF11A54C1FE31384");
                        j.ToTable("ProjectProjectStatus");
                        j.IndexerProperty<int>("ProjectId").HasColumnName("project_id");
                        j.IndexerProperty<int>("StatusId").HasColumnName("status_id");
                    });
        });

        modelBuilder.Entity<ProjectParticipation>(entity => {
            entity.HasKey(e => e.ParticipationId).HasName("PK__ProjectP__908F434B131C34AB");

            entity.ToTable("ProjectParticipation");

            entity.Property(e => e.ParticipationId).HasColumnName("participation_id");
            entity.Property(e => e.ParticipationDate)
                .HasColumnType("date")
                .HasColumnName("participation_date");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectParticipations)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProjectPa__proje__4AB81AF0");

            entity.HasOne(d => d.User).WithMany(p => p.ProjectParticipations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProjectPa__user___49C3F6B7");
        });

        modelBuilder.Entity<ProjectStatus>(entity => {
            entity.HasKey(e => e.StatusId).HasName("PK__ProjectS__3683B53189EAFEA0");

            entity.ToTable("ProjectStatus");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Report>(entity => {
            entity.HasKey(e => e.ReportId).HasName("PK__Reports__779B7C5897E7D4F9");

            entity.Property(e => e.ReportId).HasColumnName("report_id");
            entity.Property(e => e.Attachment)
                .HasMaxLength(255)
                .HasColumnName("attachment");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ReportDate)
                .HasColumnType("date")
                .HasColumnName("report_date");
            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Task).WithMany(p => p.Reports)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reports__task_id__4D94879B");

            entity.HasOne(d => d.User).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reports__user_id__4E88ABD4");
        });

        modelBuilder.Entity<Task>(entity => {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__0492148DE9CBF5A7");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.AssignedTo).HasColumnName("assigned_to");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("('assigned')")
                .HasColumnName("status");
            entity.Property(e => e.TaskName)
                .HasMaxLength(100)
                .HasColumnName("task_name");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("FK__Tasks__assigned___46E78A0C");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tasks__project_i__45F365D3");
        });

        modelBuilder.Entity<User>(entity => {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FBB642BE2");

            entity.HasIndex(e => e.Username, "UQ__Users__AB6E6164079C299D").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__role_id__3A81B327");
        });

        modelBuilder.Entity<UserRole>(entity => {
            entity.HasKey(e => e.RoleId).HasName("PK__UserRole__760965CCCF7ADE2D");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
