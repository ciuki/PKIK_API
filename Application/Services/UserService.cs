using Application.DTO;
using Application.DTO.User;
using Application.DTOs.User;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IMailService _mailService;

        public UserService(IMapper mapper, UserManager<User> userManager, IMailService mailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _mailService = mailService;
        }

        public async Task<IEnumerable<UserBaseDTO>> GetAll()
        {
            IEnumerable<User> users = await _userManager.Users.ToListAsync();
            return _mapper.Map<List<UserBaseDTO>>(users);
        }

        public async Task<UserBaseDTO> Get(string id)
        {
            User? user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
            {
                throw new ObjectNotFoundException("User does not exists");
            }
            UserBaseDTO userDTO = _mapper.Map<UserBaseDTO>(user);

            return userDTO;
        }

        public async Task<string> Create(UserCreateDTO userCreateDTO)
        {
            var userExists = await _userManager.FindByNameAsync(userCreateDTO.Email);
            if (userExists != null)
            {
                throw new ObjectAlreadyExistsException("User with this username already exists");
            }

            User user = new()
            {
                Email = userCreateDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userCreateDTO.Email,
                FirstName = userCreateDTO.FirstName,
                LastName = userCreateDTO.LastName
            };
            var result = await _userManager.CreateAsync(user, userCreateDTO.Password);
            if (!result.Succeeded)
            {
                string errors = "";
                foreach (var error in result.Errors)
                {
                    errors += error.Description;
                }
                throw new ObjectValidationException(errors);
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);
            var encodedId = HttpUtility.UrlEncode(user.Id);
            var replacementData = new Dictionary<string, object>
                {
                    {
                        "ConfirmationLink", $"https://smprojekt.herokuapp.com/ConfirmEmail/{encodedId}/{encodedToken}"
                    }
                };
            await _mailService.SendEmailAsync(user.Email, $"Confirm mail", "confirmation", replacementData);
            return user.Id;

        }

        public async Task ConfirmEmail(EmailConfirmationDTO confirmationDTO)
        {
            var userId = HttpUtility.UrlDecode(confirmationDTO.UserId);
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new ObjectNotFoundException("User does not exists");
            }

            var token = HttpUtility.UrlDecode(confirmationDTO.ConfirmationToken);
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                string errors = "";
                foreach (var error in result.Errors)
                {
                    errors += error.Description;
                }
                throw new ObjectValidationException(errors);
            }
        }

    }
}
