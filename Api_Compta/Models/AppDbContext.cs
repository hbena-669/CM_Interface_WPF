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

        public virtual DbSet<Balance> Balances { get; set; } = null!;
        public virtual DbSet<Etablissement> Etablissements { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<RCGCodesComptum> RCGCodesCompta { get; set; } = null!;
        public virtual DbSet<RCodesGroupe> RCodesGroupes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Rubrique> Rubriques { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserEtablissementRole> UserEtablissementRoles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Balance>(entity =>
            {
                entity.HasKey(e => new { e.AuditNumber, e.NumCompte });

                entity.ToTable("Balance");

                entity.Property(e => e.AuditNumber)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NumCompte)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CodeMapping)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.DateCompte).HasColumnType("date");

                entity.Property(e => e.Libelle).HasMaxLength(255);

                entity.Property(e => e.MvtCredit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MvtDebit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SoldeCredit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SoldeDebit).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Etablissement)
                    .WithMany(p => p.Balances)
                    .HasForeignKey(d => d.EtablissementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Balance_Etablissement");
            });

            modelBuilder.Entity<Etablissement>(entity =>
            {
                entity.ToTable("Etablissement");

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
                entity.ToTable("Permission");

                entity.HasIndex(e => e.Code, "UQ__Permissi__A25C5AA7A0A9139A")
                    .IsUnique();

                entity.Property(e => e.PermissionId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(300);
            });

            modelBuilder.Entity<RCGCodesComptum>(entity =>
            {
                entity.HasKey(e => e.CodeComptaId)
                    .HasName("PK__RCGCodes__1FADAF97DF9E97BC");

                entity.HasIndex(e => new { e.CodeGroupeId, e.CodeCompta }, "UQ_RCodesCompta")
                    .IsUnique();

                entity.Property(e => e.CodeComptaId).ValueGeneratedNever();

                entity.Property(e => e.CodeCompta)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeGroupe)
                    .WithMany(p => p.RCGCodesCompta)
                    .HasForeignKey(d => d.CodeGroupeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RCodesCompta_CodeGroupe");
            });

            modelBuilder.Entity<RCodesGroupe>(entity =>
            {
                entity.HasKey(e => e.CodeGroupeId)
                    .HasName("PK__RCodesGr__D09C1294340E1D86");

                entity.ToTable("RCodesGroupe");

                entity.HasIndex(e => new { e.RubriqueId, e.CodeGroupe }, "UQ_RCodesGroupe")
                    .IsUnique();

                entity.Property(e => e.CodeGroupeId).ValueGeneratedNever();

                entity.Property(e => e.CodeGroupe)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Libelle).HasMaxLength(150);

                entity.HasOne(d => d.Rubrique)
                    .WithMany(p => p.RCodesGroupes)
                    .HasForeignKey(d => d.RubriqueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RCodesGroupe_Rubrique");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

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

            modelBuilder.Entity<Rubrique>(entity =>
            {
                entity.ToTable("Rubrique");

                entity.Property(e => e.RubriqueId).ValueGeneratedNever();

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Libelle).HasMaxLength(150);

                entity.HasOne(d => d.Etablissement)
                    .WithMany(p => p.Rubriques)
                    .HasForeignKey(d => d.EtablissementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rubrique_Etablissement");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

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

                entity.ToTable("UserEtablissementRole");

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
