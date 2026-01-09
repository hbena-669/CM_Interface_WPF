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
            await SeedPermissions();
            await SeedRoles();
            await SeedAdminUser();
        }

        private async Task SeedPermissions()
        {
            var permissions = new[]
            {
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
            if (!await _db.Roles.AnyAsync(r => r.Code == "ADMIN"))
            {
                var adminRole = new Role
                {
                    RoleId = Guid.NewGuid(),
                    Code = "ADMIN",
                    Nom = "Administrateur"
                };

                _db.Roles.Add(adminRole);
                await _db.SaveChangesAsync();

                var adminRoleWithpermission = await _db.Roles
                    .Include(r => r.Permissions)
                    .SingleAsync(r => r.Code == "ADMIN");

                foreach (var perm in await _db.Permissions.ToListAsync())
                {
                    if (!adminRoleWithpermission.Permissions.Any(p => p.PermissionId == perm.PermissionId))
                        adminRoleWithpermission.Permissions.Add(perm);
                }


                await _db.SaveChangesAsync();
            }
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

            await _db.SaveChangesAsync();
        }

    }
}
