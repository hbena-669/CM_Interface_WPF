using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface_WPF.Dtos;

namespace Interface_WPF.Interfaces
{
    public interface IAuthApi
    {
        Task<LoginResponseDto> LoginAsync(string login, string password);
        Task<UserContextDto> GetContextAsync();
        void SetToken(string token);
    }
}
