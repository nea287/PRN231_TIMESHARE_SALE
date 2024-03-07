using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using PRN231_TIMESHARE_SALES_DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.IServices
{
    public interface IReservationService
    {
        public ResponseResult<ReservationViewModel> GetReservation(int id);
        public DynamicModelResponse.DynamicModelsResponse<ReservationViewModel> GetReservations(PagingRequest paging, ReservationViewModel filter);
        public ResponseResult<ReservationViewModel> CreateReservation(ReservationRequestModel request);
        public ResponseResult<ReservationViewModel> UpdateReservation(ReservationRequestModel request, int id);
        public ResponseResult<ReservationViewModel> DeleteReservation(int id);

    }
}
