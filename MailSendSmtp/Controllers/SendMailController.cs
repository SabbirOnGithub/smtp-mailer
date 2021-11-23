using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MailSendSmtp.Models;

namespace MailSendSmtp.Controllers
{
    public class SendMailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(Email email)
        {
            string to = email.To;
            string subject = email.Subject;
            string body = email.Body;

            MailMessage mm = new MailMessage();
            mm.To.Add(new MailAddress(to));
            mm.Subject = subject;
            mm.Body = body;
            mm.From = new MailAddress("sabbir.ahmed.dev@gmail.com");
            mm.IsBodyHtml = false;

            try
            {
                SmtpClient client = new SmtpClient("smpt.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new System.Net.NetworkCredential("sabbir.ahmed.dev@gmail.com", "ngbwxkwrmdhadgec")
                };
                client.Send(mm);
                ViewBag.message = "This mail has been sent to " + email.To + " Successfully...!";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                System.Diagnostics.Trace.WriteLine(e.ToString());
                ViewBag.message = e.ToString();
            }

            
            return View();
        }
    }
}
