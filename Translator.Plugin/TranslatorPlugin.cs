using System.Threading.Tasks;
using Impostor.Api.Plugins;
using SusSuite.Core;
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
        private readonly ISusSuiteCore _susSuiteCore;

        public TranslatorPlugin(ISusSuiteCore susSuiteCore)
        {
            _susSuiteCore = susSuiteCore;
            _susSuiteCore.PluginName = "Translator";
        }

        public override ValueTask EnableAsync()
        {
            _susSuiteCore.Logger.LogInformation("Enabled");

            var settings = _susSuiteCore.ConfigService.GetConfig<TranslatorSettings>("TranslatorSettings");

            return default;
        }

        public override ValueTask DisableAsync()
        {
            _susSuiteCore.Logger.LogInformation("Enabled");
            return default;
        }
    }
}
