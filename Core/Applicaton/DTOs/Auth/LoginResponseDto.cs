namespace Applicaton.DTOs.Auth;

public class LoginResponseDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expiration {  get; set; }
}
