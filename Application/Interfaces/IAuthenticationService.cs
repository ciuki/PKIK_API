using Application.DTO;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDTO> Login(LoginDTO model);
    }
}
