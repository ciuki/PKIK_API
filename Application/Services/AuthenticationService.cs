using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using Core.Entities;
using Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUserRepository userRepository, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<LoginResponseDTO> Login(LoginDTO model)
        {
            User? user = (await _userRepository.GetByLambdaAsync(user => user.Email == model.Email)).FirstOrDefault();
            if (user is null)
            {
                throw new ObjectNotFoundException("User with this combination of username and password does not exist");
            }
            if (!user.EmailConfirmed)
            {
                throw new ObjectValidationException("User not activated. Confirm your email!");
            }
            SignInResult loginResult;
            loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (loginResult.Succeeded)
            {
                TokenInfoDTO tokenInfo = await GenerateToken(user);
                return new LoginResponseDTO(tokenInfo);
            }
            if (loginResult.IsLockedOut)
            {
                throw new ObjectValidationException("User is loceked out. Try again in 15 minutes");
            }
            throw new ObjectNotFoundException("User with this combination of username and password does not exist");
        }

        private async Task<TokenInfoDTO> GenerateToken(User user)
        {
            await _userManager.RemoveAuthenticationTokenAsync(user, "loginProviderName", "JwtToken");
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            IList<Claim> claims = await _userManager.GetClaimsAsync(user);
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Email));
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"), claims);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(31),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = tokenHandler.WriteToken(token);
            await _userManager.SetAuthenticationTokenAsync(user, "loginProviderName", "JwtToken", jwtToken);
            return new TokenInfoDTO { Token = jwtToken, TokenValidUntil = tokenDescriptor.Expires.Value };

        }


    }
}
