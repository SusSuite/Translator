using System;
using Impostor.Api.Events;
using Impostor.Api.Plugins;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Translator.Handlers;
using Translator.Services;
using SusSuite.Core;
using System.Text.Json;
using Translator.Plugin.Models;

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
            services.AddSingleton<ISusSuiteCore, SusSuiteCore>();

            services.AddHttpClient<ITranslatorService, TranslatorService>();

            var jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.Converters.Add(new TranslatorSettingPropertyConverter());
            services.AddSingleton(jsonSerializerOptions);
        }
    }
}
