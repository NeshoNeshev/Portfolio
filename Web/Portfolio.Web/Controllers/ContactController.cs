namespace Portfolio.Web.Controllers
{
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Common;
    using Portfolio.Services.Messaging;
    using Portfolio.Web.ViewModels.ContactInputModels;

    public class ContactController : Controller
    {
        private readonly IEmailSender emailSender;

        public ContactController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.emailSender.SendEmailAsync(GlobalConstants.SenderEmail,
                model.Name,
                GlobalConstants.Email,
                model.Subject,
                this.ModifyContentMessage(model.Message, model.Email));

            return View();
        }

        private string ModifyContentMessage(string message, string email)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(message);
            sb.AppendLine(email);
            return sb.ToString();
        }
    }
}
