using System.Threading.Tasks;
using Impostor.Api.Plugins;
using SusSuite.Core;
using Translator.Plugin;
using Translator.Plugin.Models;

namespace Translator
{
    [ImpostorPlugin(
         package: "Translator",
         name: "Translator",
         author: "Gavin Steinhoff",
         version: "1.0.0")]
    public class TranslatorPlugin : PluginBase
    {
        private readonly SusTranslatorPlugin _susSuiteCore;

        public TranslatorPlugin(SusTranslatorPlugin susSuiteCore)
        {
            _susSuiteCore = susSuiteCore;
            var translatorSettings = _susSuiteCore.ConfigService.GetConfig<TranslatorSettings>();
        }

        public override ValueTask EnableAsync()
        {
            _susSuiteCore.Logger.LogInformation("Enabled");
            return default;
        }

        public override ValueTask DisableAsync()
        {
            _susSuiteCore.Logger.LogInformation("Enabled");
            return default;
        }
    }
}
