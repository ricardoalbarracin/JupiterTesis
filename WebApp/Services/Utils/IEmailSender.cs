using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Utils
{
    public interface IEmailSender
    {
        Result<Nothing> SendEmailResetPassword(string email, string usuario, string password);
    }
}
