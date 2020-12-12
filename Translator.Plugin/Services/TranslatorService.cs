using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SusSuite.Core;
using Translator.Models;
using Translator.Plugin.Models;

namespace Translator.Services
{
    public class TranslatorService : ITranslatorService
    {
        private readonly HttpClient _httpClient;
        private readonly ISusSuiteCore _susSuiteCore;
        private readonly TranslatorSettings _translatorSettings;


        private readonly string _detectEndpoint = "detect?api-version=3.0";
        private readonly string _translateEndpoint = "translate?api-version=3.0";
        public TranslatorService(HttpClient httpClient, ISusSuiteCore susSuiteCore)
        {
            _httpClient = httpClient;
            _susSuiteCore = susSuiteCore;
            _translatorSettings = _susSuiteCore.ConfigService.GetConfig<TranslatorSettings>("TranslatorSettings");

            _httpClient.BaseAddress = new Uri(_translatorSettings.Endpoint);
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _translatorSettings.ApiKey);
        }

        public async Task<string> GetLanguageAsynce(string message)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _detectEndpoint);
            request.Content = new StringContent(JsonSerializer.Serialize(MakeModel(message)));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var translateResponse = JsonSerializer.Deserialize<List<DetectResponse>>(await response.Content.ReadAsStringAsync());
                return translateResponse[0].Language;
            }

            _susSuiteCore.Logger.LogError("Could not talk to translation service {0}", response.ReasonPhrase);
            return string.Empty;
        }

        public async Task<string> TranslateMessageAsync(string message, string fromLanguage)
        {
            var endpoint = $"{_translateEndpoint}&from={fromLanguage}&to={_translatorSettings.MainLanguage}";
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Content = new StringContent(JsonSerializer.Serialize(MakeModel(message)));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var translateResponse = JsonSerializer.Deserialize<List<TranslateResponse>>(await response.Content.ReadAsStringAsync());
                return translateResponse[0].Translations[0].Text;
            }

            _susSuiteCore.Logger.LogError("Could not talk to translation service {0}", response.ReasonPhrase);
            return string.Empty;
        }

        private TextModel[] MakeModel(string message)
        {
            var model = new TextModel() { Text = message };
            return new TextModel[] { model };
        }
    }
}
