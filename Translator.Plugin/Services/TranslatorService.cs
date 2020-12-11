using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Translator.Models;

namespace Translator.Services
{
    public class TranslatorService : ITranslatorService
    {
        private readonly ILogger<TranslatorService> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _detectEndpoint = "detect?api-version=3.0";
        private readonly string _translateEndpoint = "translate?api-version=3.0";
        public TranslatorService(ILogger<TranslatorService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
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

            _logger.LogWarning(response.ReasonPhrase);
            return string.Empty;
        }

        public async Task<string> TranslateMessageAsync(string message, string fromLanguage, string toLanguage = "en")
        {
            var endpoint = $"{_translateEndpoint}&from={fromLanguage}&to={toLanguage}";
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Content = new StringContent(JsonSerializer.Serialize(MakeModel(message)));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var translateResponse = JsonSerializer.Deserialize<List<TranslateResponse>>(await response.Content.ReadAsStringAsync());
                return translateResponse[0].Translations[0].Text;
            }

            _logger.LogWarning(response.ReasonPhrase);
            return string.Empty;
        }

        private TextModel[] MakeModel(string message)
        {
            var model = new TextModel() { Text = message };
            return new TextModel[] { model };
        }
    }
}
