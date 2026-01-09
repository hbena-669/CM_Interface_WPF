using Api_Compta.Models.Dtos;

namespace Api_Compta.Interfaces
{
    public interface IContextService
    {
        Task<UserContextDto> GetUserContextAsync(Guid userId);
    }
}
