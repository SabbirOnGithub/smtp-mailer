using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MailKit.Security;
using MailSendSmtp.Models;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;

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

            var mm = new System.Net.Mail.MailMessage();
            mm.To.Add(new System.Net.Mail.MailAddress(to));
            mm.Subject = subject;
            mm.Body = body;
            mm.From = new System.Net.Mail.MailAddress("sabbir.ahmed.dev@gmail.com");
            mm.IsBodyHtml = false;

            try
            {
                var client = new System.Net.Mail.SmtpClient("smpt.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new System.Net.NetworkCredential("sabbir.ahmed.dev@gmail.com", "ngbwxkwrmdhadgec")
                };
                client.Send(mm);
                client.Dispose();
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


        public IActionResult MailKitMail()
        {
            var model = new MailModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult MailKitMail(MailModel mail)
        {
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(mail.From.Trim()));
            email.To.Add(MailboxAddress.Parse(mail.To.Trim()));
            email.Subject = mail.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = "<h1>"+ mail.Body +"</h1>" };

            try
            {
                using var smtp = new SmtpClient();
                if (mail.IsTls)
                {
                    smtp.Connect(mail.Host.Trim(), mail.Port, SecureSocketOptions.StartTls);
                }
                else
                {
                    smtp.Connect(mail.Host.Trim(), mail.Port, false);
                }

                smtp.Authenticate(mail.UserName.Trim(), mail.Password.Trim());
                smtp.Send(email);
                smtp.Disconnect(true);
                ViewBag.message = "This mail has been sent to " + email.To + " Successfully...!";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                System.Diagnostics.Trace.WriteLine(e.ToString());
                ViewBag.message = e.ToString();
            }

            // send email
            
            return View();
        }
    }
}
