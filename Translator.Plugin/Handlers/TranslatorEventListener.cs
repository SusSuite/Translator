using System;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Impostor.Api.Events;
using Impostor.Api.Events.Player;
using Translator.Models;
using Translator.Services;
using Impostor.Api.Innersloth.Customization;
using SusSuite.Core;
using Translator.Plugin.Models;

namespace Translator.Handlers
{
    public class TranslatorEventListener : IEventListener
    {
        private readonly ISusSuiteCore _susSuiteCore;
        private readonly ITranslatorService _translatorService;
        private readonly TranslatorSettings _translatorSettings;

        public TranslatorEventListener(ISusSuiteCore susSuiteCore, ITranslatorService translatorService)
        {
            _susSuiteCore = susSuiteCore;
            _translatorService = translatorService;
            _translatorSettings = _susSuiteCore.ConfigService.GetConfig<TranslatorSettings>("TranslatorSettings");
        }

        [EventListener]
        public async ValueTask OnPlayerChat(IPlayerChatEvent e)
        {
            _ = Task.Run(async () =>
            {
                var message = e.Message;

                switch (_translatorSettings.TranslatorMode)
                {
                    case TranslatorMode.Every:
                        var lang = await _translatorService.GetLanguageAsynce(message);
                        if (lang != _translatorSettings.MainLanguage)
                        {
                            var translation = await _translatorService.TranslateMessageAsync(message, lang);

                            _susSuiteCore.Logger.LogDebug("Translated {0} to {1}", message, translation);

                            var name = e.ClientPlayer.Character.PlayerInfo.PlayerName;
                            await e.ClientPlayer.Character.SetNameAsync($"[00ff00ff]Translator");
                            await e.ClientPlayer.Character.SendChatAsync(translation);
                            await e.ClientPlayer.Character.SetNameAsync(name);
                        }
                        break;
                    case TranslatorMode.OnCommand:
                        if (message.StartsWith("/t"))
                        {
                            var text = message.Substring(3);
                            lang = await _translatorService.GetLanguageAsynce(text);
                            if (lang != _translatorSettings.MainLanguage)
                            {
                                var translation = await _translatorService.TranslateMessageAsync(text, lang);

                                _susSuiteCore.Logger.LogDebug("Translated {0} to {1}", text, translation);

                                var name = e.ClientPlayer.Character.PlayerInfo.PlayerName;
                                await e.ClientPlayer.Character.SetNameAsync($"[00ff00ff]Translator");
                                await e.ClientPlayer.Character.SendChatAsync(translation);
                                await e.ClientPlayer.Character.SetNameAsync(name);
                            }
                        }
                        break;
                }
            });
        }
    }
}