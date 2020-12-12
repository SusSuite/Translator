using System.Threading.Tasks;

namespace Translator.Services
{
    public interface ITranslatorService
    {
        Task<string> GetLanguageAsynce(string message);
        Task<string> TranslateMessageAsync(string message, string fromLanguage);
    }
}