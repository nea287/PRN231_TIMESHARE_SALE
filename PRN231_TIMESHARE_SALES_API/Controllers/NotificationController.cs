using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;

namespace PRN231_TIMESHARE_SALES_API.Controllers
{
    [EnableCors("AllowAnyOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }
        [HttpPost("SendMailNotification/{email}/{title}")]
        public bool SendMailNotification([FromBody]string content, string email, string title)
        {
            return _service.SendMailNotification(email, content, title);
        }
    }
}
