using Driver.API.Domain.Interfaces;

namespace Driver.API.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IDriverCommandHandler,DriverCommandHandler>();
            services.AddTransient<IDriverQueryHandler,DriverQueryHandler>();
            services.AddTransient<IAlphabetizedNameHandler,AlphabetizedNameHandler>();
            services.AddTransient<IGenerateRandomDriversHandler,GenerateRandomDriversHandler>();
        }
    }
}
