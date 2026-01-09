namespace Api_Compta.Models.Dtos
{
    public class AuthResponse
    {
        public bool Requires2FA { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? TempToken { get; set; }
    }
}
