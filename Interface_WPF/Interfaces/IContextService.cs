using Interface_WPF.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Interfaces
{
    public interface IContextService
    {
        Task LoadAsync(string token);
        UserContextDto Context { get; }
    }
}
