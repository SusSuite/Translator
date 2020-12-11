using System;
using Impostor.Api.Events;
using Impostor.Api.Plugins;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Translator.Handlers;
using Translator.Services;

namespace Translator
{
    public class TranslatorStartup : IPluginStartup
    {
        public void ConfigureHost(IHostBuilder host)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IEventListener, TranslatorEventListener>();

            services.AddHttpClient<ITranslatorService, TranslatorService>(client =>
            {
                client.BaseAddress = new Uri("https://api.cognitive.microsofttranslator.com/");
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4251832fd38c4e75b10344f6d7ff4591");
            });
        }
    }
}
