using System.Text.Json.Serialization;

namespace Translator.Models
{
    public class DetectResponse
    {
        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("score")]
        public double Score { get; set; }

        [JsonPropertyName("isTranslationSupported")]
        public bool IsTranslationSupported { get; set; }

        [JsonPropertyName("isTransliterationSupported")]
        public bool IsTransliterationSupported { get; set; }

        [JsonPropertyName("alternatives")]
        public DetectResponse[] Alternatives { get; set; }
    }
}
