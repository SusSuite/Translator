using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Plugin.Models
{
    public class TranslatorSettings
    {
        public string DetectEndpoint { get; set; } = "detect?api-version=3.0";
        public string TranslateEndpoint { get; set; } = "translate?api-version=3.0";
    }
}
