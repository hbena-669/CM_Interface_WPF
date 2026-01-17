using Api_Compta.ConstantesAndEnums;
using Api_Compta.Models;
using Api_Compta.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Api_Compta.Services
{
    public class SecuritySeedService
    {
        private readonly AppDbContext _db;

        public SecuritySeedService(AppDbContext db)
        {
            _db = db;
        }

        public async Task SeedAsync()
        {
            // 1. Permissions globales
            await SeedPermissions();

            // 2. Rôles + rattachement des permissions
            await SeedRoles();

            // 3. Établissements
            var defaultEtab = await SeedDefaultEtablissement();
            var ipoca = await SeedIpoca();

            // 4. Utilisateur Admin (DEFAULT + IPOCA)
            await SeedAdminUser();

            // 5. Utilisateur Compta (IPOCA uniquement)
            await SeedComptaUser();
        }


        private async Task SeedPermissions()
        {
            var permissions = new[]
            {
                (Constantes.ADMIN_ACCESS, "Menu Administration"),
                (Constantes.MENU_ETABLISSEMENTS, "Gestion des établissements"),
                (Constantes.MENU_USERS, "Gestion des utilisateurs"),
                (Constantes.MENU_MENUS, "Gestion des menus"),
                (Constantes.MENU_BALANCES, "Consultation des balances"),
                (Constantes.MENU_STATS, "Consultation des statistiques")
            };

            foreach (var (code, desc) in permissions)
            {
                if (!await _db.Permissions.AnyAsync(p => p.Code == code))
                {
                    _db.Permissions.Add(new Permission
                    {
                        PermissionId = Guid.NewGuid(),
                        Code = code,
                        Description = desc
                    });
                }
            }

            await _db.SaveChangesAsync();
        }

        private async Task SeedRoles()
        {
            // ADMIN
            var adminRole = await EnsureRole("ADMIN", "Administrateur");
            var allPermissions = await _db.Permissions.ToListAsync();

            foreach (var perm in allPermissions)
            {
                if (!adminRole.Permissions.Any(p => p.PermissionId == perm.PermissionId))
                    adminRole.Permissions.Add(perm);
            }

            // COMPTA
            var comptaRole = await EnsureRole("COMPTA", "Comptable");

            foreach (var perm in allPermissions
                .Where(p => p.Code != Constantes.ADMIN_ACCESS))
            {
                if (!comptaRole.Permissions.Any(p => p.PermissionId == perm.PermissionId))
                    comptaRole.Permissions.Add(perm);
            }

            await _db.SaveChangesAsync();
        }

        private async Task<Role> EnsureRole(string code, string nom)
        {
            var role = await _db.Roles
                .Include(r => r.Permissions)
                .SingleOrDefaultAsync(r => r.Code == code);

            if (role != null)
                return role;

            role = new Role
            {
                RoleId = Guid.NewGuid(),
                Code = code,
                Nom = nom
            };

            _db.Roles.Add(role);
            await _db.SaveChangesAsync();

            return await _db.Roles
                .Include(r => r.Permissions)
                .SingleAsync(r => r.Code == code);
        }


        private async Task<Etablissement> SeedDefaultEtablissement()
        {
            var etab = await _db.Etablissements.FirstOrDefaultAsync();

            if (etab == null)
            {
                etab = new Etablissement
                {
                    EtablissementId = Guid.NewGuid(),
                    Code = "DEFAULT",
                    Nom = "Établissement principal",
                    IsActive = true
                };

                _db.Etablissements.Add(etab);
                await _db.SaveChangesAsync();
            }

            return etab;
        }

        private async Task<Etablissement> SeedIpoca()
        {
            var etab = await _db.Etablissements
                .SingleOrDefaultAsync(e => e.Code == "IPOCA");

            if (etab != null)
                return etab;

            etab = new Etablissement
            {
                EtablissementId = Guid.NewGuid(),
                Code = "IPOCA",
                Nom = "Clinique IPOCA",
                IsActive = true
            };

            _db.Etablissements.Add(etab);
            await _db.SaveChangesAsync();

            return etab;
        }

        private async Task SeedAdminUser()
        {
            if (await _db.Users.AnyAsync(u => u.Login == "admin"))
                return;

            var etab = await SeedDefaultEtablissement();
            var adminRole = await _db.Roles.SingleAsync(r => r.Code == "ADMIN");

            var admin = new User
            {
                UserId = Guid.NewGuid(),
                Login = "admin",
                Email = "admin@local",
                PasswordHash = PasswordHasher.Hash("Admin123!"),
                IsActive = true,
                Is2FAEnabled = false
            };

            _db.Users.Add(admin);

            _db.UserEtablissementRoles.Add(new UserEtablissementRole
            {
                UserId = admin.UserId,
                EtablissementId = etab.EtablissementId,
                RoleId = adminRole.RoleId
            });

            var ipoca = await SeedIpoca();

            if (!await _db.UserEtablissementRoles.AnyAsync(x =>
                x.UserId == admin.UserId &&
                x.EtablissementId == ipoca.EtablissementId))
            {
                _db.UserEtablissementRoles.Add(new UserEtablissementRole
                {
                    UserId = admin.UserId,
                    EtablissementId = ipoca.EtablissementId,
                    RoleId = adminRole.RoleId
                });
            }

            await _db.SaveChangesAsync();
        }

        private async Task SeedComptaUser()
        {
            if (await _db.Users.AnyAsync(u => u.Login == "compta"))
                return;

            var ipoca = await SeedIpoca();
            var comptaRole = await _db.Roles.SingleAsync(r => r.Code == "COMPTA");

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Login = "compta",
                Email = "compta@ipoca.local",
                PasswordHash = PasswordHasher.Hash("Compta123!"),
                IsActive = true,
                Is2FAEnabled = false
            };

            _db.Users.Add(user);

            _db.UserEtablissementRoles.Add(new UserEtablissementRole
            {
                UserId = user.UserId,
                EtablissementId = ipoca.EtablissementId,
                RoleId = comptaRole.RoleId
            });

            await _db.SaveChangesAsync();
        }

    }
}
