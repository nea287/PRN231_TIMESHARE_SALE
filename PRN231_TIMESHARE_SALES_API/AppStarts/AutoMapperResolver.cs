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
            CreateMap<Project, ProjectViewModel>()
                .ForMember(x => x.TotalRevenue, dest =>
                    dest.MapFrom(opt =>
                          opt.DepartmentOfProjects.Select(x => x.Department)
                          .SelectMany(t => t.AvailableTimes)
                          .SelectMany(a => a.Contracts)
                          .Sum(t => t.ContractAmount)))
                .ReverseMap();
            CreateMap<Project, ProjectRequestModel>().ReverseMap();
            CreateMap<ProjectRequestModel, ProjectViewModel>().ReverseMap();
            #endregion

            #region UsageHistory
            CreateMap<UsageHistory, UsageHistoryViewModel>().ReverseMap();
            CreateMap<UsageHistory, UsageHistoryRequestModel>().ReverseMap();
            CreateMap<UsageHistoryRequestModel, UsageHistoryViewModel>().ReverseMap();
            #endregion

            #region Account
            CreateMap<Account, AccountViewModel>()
                .ForMember(x => x.TotalRevenue, dest =>
                    dest.MapFrom(opt =>
                        opt.ContractStaffs.Sum(x => x.ContractAmount)))
                .ForMember(x => x.TotalCommission, dest => 
                    dest.MapFrom(opt => opt.ContractStaffs.Sum(a => a.CommissionAmount)))
                .ReverseMap();
            CreateMap<Account, AccountRequestModel>().ReverseMap();
            CreateMap<AccountViewModel, AccountRequestModel>().ReverseMap();
            #endregion

            #region StaffOfProject
            CreateMap<StaffOfProject, StaffOfProjectRequestModel>().ReverseMap();
            CreateMap<StaffOfProject, StaffOfProjectsViewModel>().ReverseMap();
            CreateMap<StaffOfProjectRequestModel, StaffOfProjectsViewModel>().ReverseMap();

            #endregion

            #region Reservation
            CreateMap<Reservation, ReservationViewModel>().ReverseMap();
            CreateMap<Reservation, ReservationRequestModel>().ReverseMap();
            CreateMap<ReservationViewModel, ReservationRequestModel>().ReverseMap();
            #endregion

            #region UsageRight
            CreateMap<UsageRight, UsageRightViewModel>().ReverseMap();
            CreateMap<UsageRight, UsageRightRequestModel>().ReverseMap();
            CreateMap<UsageRightViewModel, UsageRightRequestModel>().ReverseMap();
            #endregion

            #region AvailableTime
            CreateMap<AvailableTime, AvailableTimeRequestModel>().ReverseMap();
            CreateMap<AvailableTime, AvailableTimeViewModel>().ReverseMap();
            CreateMap<AvailableTimeViewModel, AvailableTimeRequestModel>();
            #endregion

            #region Contract
            CreateMap<Contract, ContractViewModel>().ReverseMap();
            CreateMap<Contract, ContractRequestModel>().ReverseMap();
            CreateMap<ContractViewModel, ContractRequestModel>().ReverseMap();
            #endregion

            #region Facillity
            CreateMap<Facility, FacilityViewModel>().ReverseMap();
            CreateMap<Facility, FacilityRequestModel>().ReverseMap();
            CreateMap<FacilityViewModel, FacilityRequestModel>().ReverseMap();
            #endregion

            #region Feedback
            CreateMap<Feedback, FeedbackViewModel>().ReverseMap();
            CreateMap<Feedback, FeedbackRequestModel>().ReverseMap();
            CreateMap<FeedbackRequestModel, FeedbackViewModel>();
            #endregion

            #region Owner
            CreateMap<Owner, OwnerViewModel>().ReverseMap();
            CreateMap<Owner, OwnerRequestModel>().ReverseMap();
            CreateMap<OwnerViewModel, OwnerRequestModel>().ReverseMap();
            #endregion

            #region Department
            CreateMap<Department, DepartmentViewModel>()
                .ForMember(x => x.TotalRevenue, dest =>
                    dest.MapFrom(opt =>
                         opt.AvailableTimes.SelectMany(t => t.Contracts)
                                           .Sum(a => a.ContractAmount)))
                .ReverseMap();
            CreateMap<DepartmentRequestModel, Department>().ReverseMap();
            CreateMap<DepartmentViewModel, DepartmentRequestModel>().ReverseMap();
            #endregion

            #region CustomerRequest
            CreateMap<CustomerRequest, CustomerRequestRequestModel>().ReverseMap();
            CreateMap<CustomerRequest, CustomerRequestViewModel>().ReverseMap();
            CreateMap<CustomerRequestViewModel, CustomerRequestRequestModel>().ReverseMap();
            #endregion

            #region DepartmentOfProject
            CreateMap<DepartmentOfProject, DepartmentOfProjectViewModel>().ReverseMap();
            #endregion


        }
    }
}
