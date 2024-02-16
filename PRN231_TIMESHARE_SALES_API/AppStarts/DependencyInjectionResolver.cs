using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.Services;
using PRN231_TIMESHARE_SALES_Repository.IRepository;
using PRN231_TIMESHARE_SALES_Repository.Repository;

namespace PRN231_TIMESHARE_SALES_API.AppStarts
{
    public static class DependencyInjectionResolver
    {
        public static void ConfigDI(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectService, ProjectService>();

            services.AddScoped<IUsageHistoryRepository, UsageHistoryRepository>();
            services.AddScoped<IUsageHistoryService, UsageHistoryService>();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();
            
            services.AddScoped<IStaffOfProjectService, StaffOfProjectService>();

        }
    }
}
