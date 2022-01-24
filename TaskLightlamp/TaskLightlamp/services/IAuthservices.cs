using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLightlamp.models;

namespace TaskLightlamp.services
{
    public interface IAuthservices
    {
        Task<Authmodel> RegisterAsync(RegisterModel modil);
        Task<Authmodel> GetTokenAsync(TokenRequestModel model);
        
    }
}
