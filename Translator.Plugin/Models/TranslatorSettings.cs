using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Translator.Plugin.Models
{
    public class TranslatorSettings
    {
        public string Endpoint { get; set; }
        public string ApiKey { get; set; }
        public TranslatorMode TranslatorMode { get; set; }
        public string MainLanguage { get; set; }
    }

    public enum TranslatorMode
    {
        Every,
        OnCommand,
    }


    public class TranslatorSettingPropertyConverter : JsonConverter<TranslatorSettings>
    {
        public override TranslatorSettings Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            // Don't pass in options when recursively calling Deserialize.
            TranslatorSettings translatorSettings = JsonSerializer.Deserialize<TranslatorSettings>(ref reader);

            if (string.IsNullOrEmpty(translatorSettings.Endpoint)) throw new JsonException("Endpoint is null");
            if (string.IsNullOrEmpty(translatorSettings.ApiKey)) throw new JsonException("ApiKey is null");
            if (string.IsNullOrEmpty(translatorSettings.MainLanguage)) throw new JsonException("MainLanguage is null");
            if (!Enum.IsDefined(translatorSettings.TranslatorMode)) throw new JsonException("TranslatorMode is not valid. Must be 0 (Every), 1 (OnCommand)");

            return translatorSettings;
        }

        public override void Write(Utf8JsonWriter writer, TranslatorSettings translatorSettings, JsonSerializerOptions options)
        {
            translatorSettings.Endpoint = "https://api.cognitive.microsofttranslator.com/";
            translatorSettings.ApiKey = "API_KEY";
            translatorSettings.TranslatorMode = 0;
            translatorSettings.MainLanguage = "en";

            // Don't pass in options when recursively calling Serialize.
            JsonSerializer.Serialize(writer, translatorSettings);
        }
    }
}

