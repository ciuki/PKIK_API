using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTO
{
    public class LoginResponseDTO
    {
        [SwaggerSchema("Object that contains token info")]
        public TokenInfoDTO TokenInfo { get; set; }
        public LoginResponseDTO(TokenInfoDTO tokenInfo)
        {
            TokenInfo = tokenInfo;
        }
    }
}
