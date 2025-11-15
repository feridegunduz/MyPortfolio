using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Models;
using System.Net.Mail;
using System.Net;

namespace MyPortfolio.Controllers
{
    public class ContactController : Controller
    {
        [HttpPost]
        public JsonResult Send(ContactFormModel model)
        {
            string message = "";
            string color = "green";

            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(model.Email);
                    mail.To.Add("gndz.frde@gmail.com"); 
                    mail.Subject = string.IsNullOrEmpty(model.Subject) ? "No Subject" : model.Subject;
                    mail.Body = $"Name: {model.Name}\nEmail: {model.Email}\n\nMessage:\n{model.Message}";

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new NetworkCredential("gndz.frde@gmail.com", "dqlu yigl coiy tyuz"); // Gmail App Password
                    smtp.EnableSsl = true;
                    smtp.Send(mail);

                    message = "Your message was sent, thank you!";
                }
                catch
                {
                    message = "Something went wrong. Please try again.";
                    color = "red";
                }
            }
            else
            {
                message = "Please fill in all required fields.";
                color = "red";
            }

            // AJAX ile JSON dönecek
            return Json(new { message = message, color = color });
        }
    }
}
