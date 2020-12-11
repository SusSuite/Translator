using System;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Impostor.Api.Events;
using Impostor.Api.Events.Player;
using Microsoft.Extensions.Logging;
using Translator.Models;
using Translator.Services;
using Impostor.Api.Innersloth.Customization;

namespace Translator.Handlers
{
    public class TranslatorEventListener : IEventListener
    {
        private readonly ILogger<TranslatorEventListener> _logger;
        private readonly ITranslatorService _translatorService;

        public TranslatorEventListener(ILogger<TranslatorEventListener> logger, ITranslatorService translatorService)
        {
            _logger = logger;
            _translatorService = translatorService;
        }

        [EventListener]
        public async ValueTask OnPlayerChat(IPlayerChatEvent e)
        {
            var message = e.Message;
            var lang = await _translatorService.GetLanguageAsynce(message);
            if (lang != "en")
            {
                var translation = await _translatorService.TranslateMessageAsync(message, lang);
                var name = e.ClientPlayer.Character.PlayerInfo.PlayerName;
                await e.ClientPlayer.Character.SetNameAsync($"[00ff00ff]Translator");
                await e.ClientPlayer.Character.SendChatAsync(translation);
                await e.ClientPlayer.Character.SetNameAsync(name);
            }
        }
    }
}