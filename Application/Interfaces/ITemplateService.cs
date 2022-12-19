namespace Application.Interfaces
{
    public interface ITemplateService
    {
        Task<string> Render(string templateName, Dictionary<string, object> replacementData);
    }
}
