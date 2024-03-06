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
            CreateMap<StaffOfProject, StaffOfProjectRequestModel>().ReverseMap();
            CreateMap<StaffOfProject, StaffOfProjectsViewModel>().ReverseMap();
            CreateMap<StaffOfProjectRequestModel, StaffOfProjectsViewModel>().ReverseMap();

            #endregion

            #region Reservation
            CreateMap<Reservation, ReservationViewModel>().ReverseMap();
            CreateMap<Reservation, ReservationRequestModel>().ReverseMap();
            CreateMap<ReservationViewModel, ReservationRequestModel>();
            #endregion

            #region UsageRight
            CreateMap<UsageRight, UsageRightViewModel>();
            #endregion

        }
    }
}
