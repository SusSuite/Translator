using System.Text.Json.Serialization;

namespace Translator.Models
{
    public class TranslateResponse
    {
        [JsonPropertyName("translations")]
        public Translation[] Translations { get; set; }
    }

    public class Translation
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("to")]
        public string To { get; set; }
    }
}
