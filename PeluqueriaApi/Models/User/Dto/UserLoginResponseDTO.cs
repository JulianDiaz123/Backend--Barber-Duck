namespace PeluqueriaApi.Models.User.Dto
{
    public class UserLoginResponseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Email { get; set; }
        public string UserName { get; set; } = null!;
        public List<string> Roles { get; set; } = null!;
    }
}
