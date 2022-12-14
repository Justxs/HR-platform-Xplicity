using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace XplicityHRplatformBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendEmail(string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("jedidiah.weimann@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("hr@xplicity.com"));
            email.Subject = "Priminimas dėl susisiekimo su kandidatu!";
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("jedidiah.weimann@ethereal.email", "xzgeukanJ2VTQf3uVk");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();
        }
    }
}
