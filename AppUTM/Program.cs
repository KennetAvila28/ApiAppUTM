using AppUTM.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Radzen;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using AppUTM.Helpers;
using AppUTM.Interfaces;

namespace AppUTM
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddHttpClient("ApiAppUTM", client =>
             {
                 client.BaseAddress = new Uri("https://localhost:44305/api/");
             }).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("ApiAppUTM"));
            //builder.Services.AddScoped<AuthenticationStateProvider, AuthHelper>();
            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes
                .Add("https://graph.microsoft.com/User.Read");
            });
            builder.Services.AddScoped<ILocalStorage, LocalStorage>();
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();
            builder.Services.AddGraphClient("https://graph.microsoft.com/User.Read");
            await builder.Build().RunAsync();
        }
    }
}