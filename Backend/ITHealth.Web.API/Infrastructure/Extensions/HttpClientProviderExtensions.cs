namespace ITHealth.Web.API.Infrastructure.Extensions
{
    public static class HttpClientProviderExtensions
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient("Trello", config =>
            {
                config.BaseAddress = new Uri("https://api.trello.com/1/");
                config.DefaultRequestHeaders.Clear();
            });

            services.AddHttpClient("Clockify", config =>
            {
                config.BaseAddress = new Uri("https://api.clockify.me/api/v1/");
                config.DefaultRequestHeaders.Clear();
            });
            
            services.AddHttpClient("Jira", config =>
            {
                config.DefaultRequestHeaders.Clear();
            });

            return services;
        }
    }
}
