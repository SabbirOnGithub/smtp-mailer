using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailSendSmtp.Models
{
    public class MailModel
    {
        public MailModel()
        {
            IsTls = true;
            From = "sabbir.ahmed.dev@gmail.com";
            Port = 587;
            Host = "smtp.gmail.com";
            UserName = "sabbir.ahmed.dev@gmail.com";
            Password = "ngbwxkwrmdhadgec";
        }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool IsTls { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
