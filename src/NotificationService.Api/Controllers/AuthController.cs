[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMailService _mailService;

    public AuthController(IMailService mailService)
    {
        _mailService = mailService;
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> SendPasswordResetEmail([FromBody] ResetRequest request)
    {
        if (string.IsNullOrEmpty(request.Email))
            return BadRequest("Email is required.");

        string resetToken = Guid.NewGuid().ToString();
        string resetLink = $"https://yourdomain.com/reset-password?token={resetToken}";

        bool emailSent = await _mailService.SendResetPasswordEmailAsync(request.Email, resetLink);

        return emailSent ? Ok("Reset email sent.") : StatusCode(500, "Error sending reset email.");
    }
}

public class ResetRequest
{
    public string Email { get; set; }
}
