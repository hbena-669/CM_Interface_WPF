using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Api_Compta.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Etablissement> Etablissements { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserEtablissementRole> UserEtablissementRoles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Etablissement>(entity =>
            {
                entity.ToTable("Etablissement", "dbo");

                entity.HasIndex(e => e.Code, "UQ__Etabliss__A25C5AA77C5BDCBC")
                    .IsUnique();

                entity.Property(e => e.EtablissementId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DateCreation).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nom).HasMaxLength(200);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission", "dbo");

                entity.HasIndex(e => e.Code, "UQ__Permissi__A25C5AA7A0A9139A")
                    .IsUnique();

                entity.Property(e => e.PermissionId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(300);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role", "dbo");

                entity.HasIndex(e => e.Code, "UQ__Role__A25C5AA7B26FAB5E")
                    .IsUnique();

                entity.Property(e => e.RoleId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nom).HasMaxLength(200);

                entity.HasMany(d => d.Permissions)
                    .WithMany(p => p.Roles)
                    .UsingEntity<Dictionary<string, object>>(
                        "RolePermission",
                        l => l.HasOne<Permission>().WithMany().HasForeignKey("PermissionId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_RolePermission_Permission"),
                        r => r.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_RolePermission_Role"),
                        j =>
                        {
                            j.HasKey("RoleId", "PermissionId");

                            j.ToTable("RolePermission");
                        });
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "dbo");

                entity.HasIndex(e => e.Email, "IX_User_Email");

                entity.HasIndex(e => e.Login, "IX_User_Login");

                entity.HasIndex(e => e.Login, "UQ__User__5E55825B1FF744B7")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__User__A9D10534B6542BBF")
                    .IsUnique();

                entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreation).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Login).HasMaxLength(100);

                entity.Property(e => e.PasswordHash).HasMaxLength(500);
            });

            modelBuilder.Entity<UserEtablissementRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.EtablissementId });

                entity.ToTable("UserEtablissementRole", "dbo");

                entity.HasIndex(e => e.EtablissementId, "IX_UER_Etablissement");

                entity.HasIndex(e => e.UserId, "IX_UER_User");

                entity.HasOne(d => d.Etablissement)
                    .WithMany(p => p.UserEtablissementRoles)
                    .HasForeignKey(d => d.EtablissementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UER_Etablissement");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserEtablissementRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UER_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserEtablissementRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UER_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
