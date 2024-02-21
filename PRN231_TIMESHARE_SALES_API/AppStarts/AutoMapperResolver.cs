using AutoMapper;
using AutoMapper.Configuration.Annotations;
using PRN231_TIMESHARE_SALES_BusinessLayer.Filters;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.Services;
using PRN231_TIMESHARE_SALES_DataLayer.Models;

namespace PRN231_TIMESHARE_SALES_API.AppStarts
{
    public class AutoMapperResolver : Profile
    {
        public AutoMapperResolver()
        {
            #region Project
            CreateMap<Project, ProjectViewModel>().ReverseMap();
            CreateMap<Project, ProjectRequestModel>().ReverseMap();
            CreateMap<ProjectRequestModel, ProjectViewModel>().ReverseMap();
            #endregion

            #region UsageHistory
            CreateMap<UsageHistory, UsageHistoryViewModel>().ReverseMap();
            CreateMap<UsageHistory, UsageHistoryRequestModel>().ReverseMap();
            CreateMap<UsageHistoryRequestModel, UsageHistoryViewModel>().ReverseMap();
            #endregion

            #region Account
            CreateMap<Account, AccountViewModel>().ReverseMap();



            CreateMap<Account, AccountRequestModel>().ReverseMap();
            CreateMap<AccountViewModel, AccountRequestModel>().ReverseMap();
            #endregion

            #region StaffOfProject
            //CreateMap<Account, AccountViewModel>()
            //    .ForMember(x => x.Projects, dest => dest.MapFrom(o => o.Projects));

            CreateMap<StaffOfProjectFilter, AccountViewModel>().ForMember(x => x.Projects, dest => dest.MapFrom(opt => new Project() { 
            ProjectCode = opt.ProjectCode,
            ProjectName = opt.ProjectName,
            ProjectId = opt.ProjectId.Value,
            RegistrationEndDate = opt.RegistrationEndDate.Value,
            EndDate = opt.EndDate.Value,
            StartDate = opt.StartDate.Value,
            RegistrationOpeningDate = opt.RegistrationOpeningDate.Value,
            PriorityType = opt.PriorityType,
            Status = opt.Status.Value,
            TotalSlot = opt.TotalSlot.Value,
            Departments = new List<Department>(),
            staff = new List<Account>()
            })).ReverseMap();


            //CreateMap<AccountViewModel, StaffOfProjectFilter>()
            //    .ForMember(x => x.ProjectCode, dest => dest.MapFrom(opt => opt.Projects.First().ProjectCode));
            
            //CreateMap<AccountViewModel, StaffOfProjectFilter>()
            //    .ForMember(x => x.ProjectName, dest => dest.MapFrom(opt => opt.Projects.First().ProjectName));

            #endregion

        }
    }
}
