using System.Net;
using System.Net.Mail;

namespace Business_Layer;

public class EmailManager
{
    private readonly SmtpClient _client;
    private readonly EmailOptions _options;
    
    public EmailManager(EmailOptions options)
    {
        _options = options;
        _client = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            EnableSsl = true,
            UseDefaultCredentials = false,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Credentials = new NetworkCredential(options.Email, options.Password)
        };
    }

    public void SendEmail(string subject, string body, string to)
    {
        var message = new MailMessage()
        {
            From = new MailAddress(_options.Email),
            Subject = subject,
            IsBodyHtml = true,
            Body = body
        };
        foreach (var email in to.Split(";"))
        {
            message.To.Add(new MailAddress(email));
        }
        
        _client.Send(message);
    }
    
}