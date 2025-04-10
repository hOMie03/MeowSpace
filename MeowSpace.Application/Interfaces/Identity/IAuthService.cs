using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeowSpace.Application.Models.Identity;

namespace MeowSpace.Application.Interfaces.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest user);
        Task<RegistrationResponse> Register(RegistrationRequest user);
        Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
