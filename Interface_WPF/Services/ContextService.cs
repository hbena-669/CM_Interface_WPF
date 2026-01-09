using Interface_WPF.Dtos;
using Interface_WPF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Services
{
    public class ContextService : IContextService
    {
        private readonly IAuthApi _authApi;

        public UserContextDto Context { get; private set; }

        public ContextService(IAuthApi authApi)
        {
            _authApi = authApi;
        }

        public async Task LoadAsync(string token)
        {
            _authApi.SetToken(token);
            Context = await _authApi.GetContextAsync();
        }
    }

}
