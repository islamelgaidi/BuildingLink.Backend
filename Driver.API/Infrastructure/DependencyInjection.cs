using Driver.API.Domain.Interfaces;

namespace Driver.API.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IDbContext, DbContext>();
            services.AddTransient<IDriverDbContext, DriverDbContext>();
            services.AddSingleton<IDbMigration, DbMigration>();
         
        }
        public static void RunDbMigration(this IApplicationBuilder app)
        {
            IDbMigration dbMigration = app.ApplicationServices.GetRequiredService<IDbMigration>();
            dbMigration.Execute();
        }
    }
}
