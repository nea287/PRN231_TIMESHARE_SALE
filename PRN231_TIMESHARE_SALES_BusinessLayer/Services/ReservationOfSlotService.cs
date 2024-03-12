using AutoMapper;
using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
using PRN231_TIMESHARE_SALES_BusinessLayer.Helpers;
using PRN231_TIMESHARE_SALES_Repository.IRepository;
using PRN231_TIMESHARE_SALES_Repository.Repository;
using System.Timers;
using Timer = System.Timers.Timer;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.Services
{
    public interface IReservationOfSlotService
    {
        //public void SummarizeSlot();
    }
    public static class ReservationOfSlotService //: IReservationOfSlotService
    {
        private static Timer _timer;
        //private readonly IAvailableTimeRepository _availableTimeRepository;
        //private readonly IReservationRepository _reservationRepository;

        //public ReservationOfSlotService(IAvailableTimeRepository availableTimeRepository, IReservationRepository reservationRepository)
        //{
        //    _timer = new Timer(20);
        //    _availableTimeRepository = availableTimeRepository;
        //    _reservationRepository = reservationRepository;
        //    //_timer.Elapsed += SummarizeSlotTimerElapsed;
        //    //_timer.AutoReset = true;
        //    //_timer.Start();
        //}
        public static void SummarizeSlot()
        {
            _timer = new Timer(3 * 60 * 10000);
            _timer.Elapsed += SummarizeSlotTimerElapsed;
            _timer.AutoReset = true;
            _timer.Start();
        }

        public static void SummarizeSlotTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            SummarizeSlotByTimer();
        }
        public static void SummarizeSlotByTimer()
        {
            IAvailableTimeRepository _availableTimeRepository = new AvailableTimeRepository();
            IReservationRepository _reservationRepository = new ReservationRepository();
            try
            {
                foreach (var slot in _availableTimeRepository.GetAll())
                {
                    if (slot.EndDate == DateTime.Now.AddDays(1))
                    {
                        var reservation = _reservationRepository.GetMin(x => x.ReservationDate < slot.EndDate);

                        reservation.Status = (int)ReservationStatus.IN_PROGRESS;
                        slot.Status = (int)AvailableStatus.RESERVED;

                        _availableTimeRepository.UpdateById(slot, slot.AvailableTimeId);
                        _reservationRepository.UpdateById(reservation, reservation.ReservationId);

                        _availableTimeRepository.SaveChages();
                        _reservationRepository.SaveChages();

                        string content = "Kính gửi quý khách hàng thông tin " 
                            + Enum.GetName(typeof(DepartmentConstructionType), slot.Department.ConstructionType.Value) + " " 
                            + slot.Department.DepartmentName + ": \n" + "Ngày nhận phòng: " + slot.StartDate + "\n"
                            + "Ngày trả phòng: " + slot.EndDate + "\n" + "Rất mong sự có mặt của bạn!\n" + "TimeSharing";

                        SupportingFeature.Instance.SendEmail(reservation.Customer.Email, content,
                            "Chúc mừng quý khách hàng giữ chỗ " + slot.Department.DepartmentName + " thành công!");
                         
                    }

                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                lock (_availableTimeRepository) ;
                lock (_reservationRepository) ;
            }

        }

    }
}
