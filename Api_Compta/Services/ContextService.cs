using Api_Compta.Interfaces;
using Api_Compta.Models;
using Api_Compta.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Api_Compta.Services
{
    public class ContextService : IContextService
    {
        private readonly AppDbContext _db;

        public ContextService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UserContextDto> GetUserContextAsync(Guid userId)
        {
            var user = await _db.Users
                .Include(u => u.UserEtablissementRoles)
                    .ThenInclude(uer => uer.Etablissement)
                .Include(u => u.UserEtablissementRoles)
                    .ThenInclude(uer => uer.Role)
                        .ThenInclude(r => r.Permissions)
                .SingleOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                throw new UnauthorizedAccessException("Utilisateur introuvable");

            return new UserContextDto
            {
                UserId = user.UserId,
                Login = user.Login,

                Etablissements = user.UserEtablissementRoles
                    .Select(uer => uer.Etablissement)
                    .Distinct()
                    .Select(e => new EtablissementDto
                    {
                        EtablissementId = e.EtablissementId,
                        Nom = e.Nom,
                        Code = e.Code
                    })
                    .ToList(),

                Permissions = user.UserEtablissementRoles
                    .SelectMany(uer => uer.Role.Permissions)
                    .Select(p => p.Code)
                    .Distinct()
                    .ToList()
            };
        }
    }
}
