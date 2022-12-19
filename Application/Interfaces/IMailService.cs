namespace Application.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(string to, string subject, string templateName, Dictionary<string, object> replacementData);
    }
}