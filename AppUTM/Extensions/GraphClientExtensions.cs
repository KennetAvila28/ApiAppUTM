using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppUTM.Extensions
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
    using Microsoft.Authentication.WebAssembly.Msal.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Graph;

    internal static class GraphClientExtensions
    {
        public static IServiceCollection AddGraphClient(
            this IServiceCollection services, params string[] scopes)
        {
            services.Configure<RemoteAuthenticationOptions<MsalProviderOptions>>(
                options =>
                {
                    foreach (var scope in scopes)
                    {
                        options.ProviderOptions.AdditionalScopesToConsent.Add(scope);
                    }
                });

            services.AddScoped<IAuthenticationProvider,
                NoOpGraphAuthenticationProvider>();
            services.AddScoped<IHttpProvider, HttpClientHttpProvider>(sp =>
                new HttpClientHttpProvider(new HttpClient()));
            services.AddScoped(sp => new GraphServiceClient(
                sp.GetRequiredService<IAuthenticationProvider>(),
                sp.GetRequiredService<IHttpProvider>()));

            return services;
        }

        private class NoOpGraphAuthenticationProvider : IAuthenticationProvider
        {
            public NoOpGraphAuthenticationProvider(IAccessTokenProvider tokenProvider)
            {
                TokenProvider = tokenProvider;
            }

            private IAccessTokenProvider TokenProvider { get; }

            public async Task AuthenticateRequestAsync(HttpRequestMessage request)
            {
                var result = await TokenProvider.RequestAccessToken(
                    new AccessTokenRequestOptions()
                    {
                        Scopes = new[] { "https://graph.microsoft.com/User.Read" }
                    });

                if (result.TryGetToken(out var token))
                {
                    request.Headers.Authorization ??= new AuthenticationHeaderValue(
                        "Bearer", token.Value);
                }
            }
        }

        private class HttpClientHttpProvider : IHttpProvider
        {
            private readonly HttpClient http;

            public HttpClientHttpProvider(HttpClient http)
            {
                this.http = http;
            }

            public ISerializer Serializer { get; } = new Serializer();

            public TimeSpan OverallTimeout { get; set; } = TimeSpan.FromSeconds(300);

            public void Dispose()
            {
            }

            public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
            {
                return http.SendAsync(request);
            }

            public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                HttpCompletionOption completionOption,
                CancellationToken cancellationToken)
            {
                return http.SendAsync(request, completionOption, cancellationToken);
            }
        }

        public static async Task<string> ImageToBase64(byte[] buffer)
        {
            return await Task.Run(() => Convert.ToBase64String(buffer));
        }

        public static byte[] ReadToEnd(Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
    }
}