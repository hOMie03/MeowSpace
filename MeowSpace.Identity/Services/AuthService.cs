using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MeowSpace.Application.Exceptions;
using MeowSpace.Application.Interfaces.Identity;
using MeowSpace.Application.Models.Identity;
using MeowSpace.Identity.Context;
using MeowSpace.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MeowSpace.Identity.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly SignInManager<ApplicationUser> _signInManager;
        readonly JwtSettings _jwtSettings;
        readonly MeowSpaceIdentityDBContext _idbcontext;

        //Constructor
        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings, MeowSpaceIdentityDBContext idbcontext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _idbcontext = idbcontext;
        }
        public async Task<AuthResponse> Login(AuthRequest user)
        {
            var checkUser = await _userManager.FindByEmailAsync(user.Email);
            AuthResponse response = new AuthResponse();
            if (checkUser == null)
            {
                throw new UserNotFoundException($"User with email {user.Email} not exists");
            }
            var checkUserPswd = await _signInManager.CheckPasswordSignInAsync(checkUser, user.Password, false);
            if(!checkUserPswd.Succeeded)
            {
                throw new BadRequestException($"Incorrect Password!");
            }
            JwtSecurityToken jwtSecurityToken = await GenerateToken(checkUser);
            var refreshToken = GenerateRefreshToken();
            checkUser.RefreshToken = refreshToken;
            checkUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(10);
            _idbcontext.Users.Update(checkUser);
            await _idbcontext.SaveChangesAsync();
            response.RefreshToken = refreshToken;
            response.RefreshTokenExpiration = DateTime.UtcNow.AddDays(10);
            response.IsAuthenticated = true;
            response.Id = checkUser.Id;
            response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = checkUser.Email;
            response.Username = checkUser.UserName;
            return response;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var response = new RefreshTokenResponse();
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken && u.Email == request.Email && u.RefreshTokenExpiryTime > DateTime.UtcNow);
            if(user == null)
            {
                throw new BadRequestException($"Token did not match any user or refresh token expired");
            }
            
            //Generate new Refresh Token and save to DB
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(10);
            _idbcontext.Update(user);
            await _idbcontext.SaveChangesAsync();

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
            response.JwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = user.Email;
            response.RefreshToken = newRefreshToken;
            response.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(10);
            return response;

        }

        public async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            // Get user info from DB
            var userClaims = await _userManager.GetClaimsAsync(user);
            // Get list of roles that user belongs to
            var roles = await _userManager.GetRolesAsync(user);
            // Convert into Claims
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
            // Create Claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UID", user.Id)
            }.Union(userClaims).Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256); // Generally used this type
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
            );
            return jwtSecurityToken;
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
