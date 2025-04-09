using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeowSpace.Application.Exceptions;
using MeowSpace.Application.Interfaces.Identity;
using MeowSpace.Application.Models.Identity;
using MeowSpace.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace MeowSpace.Identity.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly SignInManager<ApplicationUser> _signInManager;
        //readonly JwtSettings _jwtSettings;

        //Constructor
        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager/*, IOptions<JwtSettings> jwtSettings*/)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_jwtSettings = jwtSettings.Value;
        }
        public Task<bool> Login(AuthRequest user)
        {
            throw new NotImplementedException();
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest user)
        {
            var checkExisitingUser = _userManager.FindByEmailAsync(user.Email);
            if(checkExisitingUser != null)
            {
                throw new BadRequestException($"User with email {user.Email} already exists");
            }
            var newUser = new ApplicationUser
            {
                UserName = user.Username,
                PetName = user.PetName,
                Email = user.Email,
                EmailConfirmed = true,
                PetType = user.PetType,
                PetProfileImg = user.PetProfileImg
            };
            var registration = await _userManager.CreateAsync(newUser, user.Password);
            if (registration.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "User");
                return new RegistrationResponse() { UserID = newUser.Id };
            }
            else
            {
                var errorResult = string.Join(", ", registration.Errors.Select(e => e.Description));
                throw new BadRequestException($"{errorResult}");
            }
        }
    }
}
