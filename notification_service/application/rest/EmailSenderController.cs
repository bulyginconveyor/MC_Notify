using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using notification_service.services.email_sender;

namespace notification_service.application.rest
{
    [Route("api/email_sender")]
    [ApiController]
    public class EmailSenderController(EmailSender sender) : ControllerBase
    {
        private readonly EmailSender _sender = sender;

        [HttpPost]
        public async Task<IActionResult> SendEmailConfirmEmail(string registerDataEmail, string code)
        {
            var result = await _sender.SendEmailConfirmEmail(registerDataEmail, code);
            
            return Ok(result);
        }
    }
}
