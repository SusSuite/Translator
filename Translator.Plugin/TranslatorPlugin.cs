using System.Threading.Tasks;
using Impostor.Api.Plugins;
using Microsoft.Extensions.Logging;

namespace Translator
{
    [ImpostorPlugin(
         package: "Translator",
         name: "Translator",
         author: "Gavin Steinhoff",
         version: "1.0.0")]
    public class TranslatorPlugin : PluginBase
    {
        private readonly ILogger<TranslatorPlugin> _logger;

        public TranslatorPlugin(ILogger<TranslatorPlugin> logger)
        {
            _logger = logger;
        }

        public override ValueTask EnableAsync()
        {
            _logger.LogInformation("TranslatorPlugin is being enabled.");
            return default;
        }

        public override ValueTask DisableAsync()
        {
            _logger.LogInformation("TranslatorPlugin is being disabled.");
            return default;
        }
    }
}
