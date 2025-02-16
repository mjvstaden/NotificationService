using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using NotificationService.Core.Interfaces;
using System.Threading.Tasks;

public class MailjetService : IMailService
{
    private readonly string _apiKey;
    private readonly string _apiSecret;
    private readonly string _fromEmail;
    private readonly string _fromName;

    public MailjetService(string apiKey, string apiSecret, string fromEmail, string fromName)
    {
        _apiKey = apiKey;
        _apiSecret = apiSecret;
        _fromEmail = fromEmail;
        _fromName = fromName;
    }

    public async Task<bool> SendResetPasswordEmailAsync(string toEmail, string resetLink)
    {
        var client = new MailjetClient(_apiKey, _apiSecret);
        var request = new MailjetRequest { Resource = Send.Resource }
            .Property(Send.Messages, new JArray {
                new JObject {
                    { "From", new JObject { { "Email", _fromEmail }, { "Name", _fromName } } },
                    { "To", new JArray { new JObject { { "Email", toEmail }, { "Name", toEmail } } } },
                    { "Subject", "Password Reset Request" },
                    { "TextPart", $"Click the following link to reset your password: {resetLink}" },
                    { "HTMLPart", $"<p>Click <a href='{resetLink}'>here</a> to reset your password.</p>" }
                }
            });

        var response = await client.PostAsync(request);
        return response.IsSuccessStatusCode;
    }
}
