using Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace Application.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly ITemplateService _templateService;

        public MailService(IConfiguration configuration, ITemplateService templateService)
        {
            _configuration = configuration;
            _templateService = templateService;
        }

        public async Task SendEmailAsync(string to, string subject, string templateName, Dictionary<string, object> replacementData)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["Mail:Email"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            string html = await _templateService.Render(templateName, replacementData);
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration["Mail:Host"], Int32.Parse(_configuration["Mail:Port"]), SecureSocketOptions.Auto);
            smtp.Authenticate(_configuration["Mail:Email"], _configuration["Mail:Password"]);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
