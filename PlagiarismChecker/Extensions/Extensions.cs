using Microsoft.Extensions.DependencyInjection;
using PlagiarismChecker.HttpClients;
using PlagiarismChecker.Interfaces;
using System;

namespace PlagiarismChecker.Extensions
{
    public static class MyExtensions
    {
        public static IServiceCollection AddMyHttpClient(this IServiceCollection services, string baseAddress)
        {
            services.AddHttpClient<IHttpClient, HttpClientBase>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            return services;
        }
    }
}
