using backend.DTOS;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        private readonly EmailSendService emailSendService;
        private readonly IConfiguration configuration;
        public EmailController(IConfiguration configuration)
        {
            this.emailSendService = new EmailSendService();
            this.configuration = configuration;
        }
        [HttpPost]
        public ActionResult Send([FromBody] EmailDTO emailDto)
        {
            try
            {
               var senderEmail = configuration["EmailSenderKey:SenderEmail"];
               var senderName = configuration["EmailSenderKey:SenderName"];
               var SentEmail = emailSendService.SendEmail(senderEmail, senderName,
                   emailDto.recieverName,emailDto.recieverEmail,emailDto.subject,emailDto.message);
                return Ok(SentEmail);
            }
            catch (Exception ex) 
            { 
            return BadRequest(ex.Message);
            }
            
        }
    }
}
