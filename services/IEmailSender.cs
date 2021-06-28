using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automated_Wedding_Application.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailWithSenderAsync( string sender, string email, string subject, string message );
    }
}
