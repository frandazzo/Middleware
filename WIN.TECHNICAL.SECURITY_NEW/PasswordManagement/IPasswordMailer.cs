using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace WIN.TECHNICAL.SECURITY_NEW.PasswordManagement
{
    public interface IPasswordMailer
    {
        void SendMail(string to,  string subject, string body);
        void SendMail(string[] to, string subject, string body);
    }
}
