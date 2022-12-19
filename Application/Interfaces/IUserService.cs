using Application.DTO;
using Application.DTO.User;
using Application.DTOs.User;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserBaseDTO>> GetAll();
        Task<UserBaseDTO> Get(string id);
        Task<string> Create(UserCreateDTO userPostDTO);
        Task ConfirmEmail(EmailConfirmationDTO confirmationDTO);

    }
}
