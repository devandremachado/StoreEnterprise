using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Net.Http;

namespace Store.WebApp.MVC.Extensions.Polly
{
    public static class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> RetryWaitPolicy()
        {
            var policy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                }, (outcome, timespan, retryCount, context) =>
                {
                    //Executar o WebApp em SelfHost para visualizar essas mensagens no Console. Aqui poderia ser registado logs...
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Tentando peela {retryCount} vez...");
                    Console.ForegroundColor = ConsoleColor.White;
                });

            return policy;
        }
    }
}
