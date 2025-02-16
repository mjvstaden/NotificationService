public interface IMailService
{
    Task<bool> SendResetPasswordEmailAsync(string toEmail, string resetLink);
}
