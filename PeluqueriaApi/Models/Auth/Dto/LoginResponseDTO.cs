using PeluqueriaApi.Models.User.Dto;

namespace PeluqueriaApi.Models.Auth.Dto
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = null!;
        public UserLoginResponseDTO User { get; set; } = null!;
    }
}
