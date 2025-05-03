using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using notification_service.infrastructure.senders.email_sender;

namespace notification_service.application.rest
{
    [Route("api/email_sender")]
    [ApiController]
    public class EmailSenderController(EmailSender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SendEmailConfirmEmail(string registerDataEmail, string code)
        {
            var result = await sender.SendEmailConfirmEmail(registerDataEmail, code);

            return result.IsSuccess 
                ? Ok() 
                : BadRequest(result.Error);
        }
    }
}
