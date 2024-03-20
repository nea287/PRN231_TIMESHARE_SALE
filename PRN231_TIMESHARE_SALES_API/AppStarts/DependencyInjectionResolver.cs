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

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectService, ProjectService>();

            services.AddScoped<IUsageHistoryRepository, UsageHistoryRepository>();
            services.AddScoped<IUsageHistoryService, UsageHistoryService>();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();
            
            services.AddScoped<IStaffOfProjectRepository, StaffOfProjectRepository>();
            services.AddScoped<IStaffOfProjectService, StaffOfProjectService>();

            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IReservationService, ReservationService>();

            services.AddScoped<IUsageRightRepository, UsageRightRepository>();
            services.AddScoped<IUsageRightService, UsageRightService>();

            services.AddScoped<IAvailableTimeRepository, AvailableTimeRepository>();
            services.AddScoped<IAvailableTimeService, AvailableTimeService>();

            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IContractService, ContractService>();

            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IOwnerService, OwnerService>();

            services.AddScoped<IFacilityRepository, FacilityRepository>();
            services.AddScoped<IFacilityService, FacilityService>();  
            
            services.AddScoped<ICustomerRequestRepository, CustomerRequestRepository>();
            services.AddScoped<ICustomerRequestService, CustomerRequestService>();

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IFeedbackService, FeedbackService>();

            services.AddScoped<IDashboardService, DashboardService>();

        }
    }
}
