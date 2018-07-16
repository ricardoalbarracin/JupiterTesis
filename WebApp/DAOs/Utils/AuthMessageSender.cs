using Core.Models.Utils;
using Core.Services.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace DAOs.Utils
{
    public class AuthMessageSender : IEmailSender
    {
        private IHostingEnvironment _env;
        public AuthMessageSender(IOptions<EmailSettings> emailSettings, IHostingEnvironment env)
        {
            _emailSettings = emailSettings.Value;
            _env = env;
        }

        public EmailSettings _emailSettings { get; }

        public Result SendEmailResetPassword(string email, string usuario, string password)
        {
            var result = new Result();
            try
            {
                string toEmail = email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail)
                };
                mail.To.Add(new MailAddress(toEmail));

                mail.Subject = "contraseña restaurada";
                
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                var webRoot = _env.WebRootPath;
                var pathToFile = _env.WebRootPath
                                + Path.DirectorySeparatorChar.ToString()
                                + "EmailTemplates"
                                + Path.DirectorySeparatorChar.ToString()
                                + "resetPassword.html";
                var body = "";
                using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                {
                    body = SourceReader.ReadToEnd();
                }
                string username = HttpUtility.UrlEncode(usuario);
                string UserPassword = HttpUtility.UrlEncode(password);
                string messageBody = string.Format(body, username, UserPassword);

                mail.Body = messageBody;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    smtp.SendMailAsync(mail).Wait();
                }
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = "No se ha podido realizar el envio del correo electronico";
            }
            return result;
            
        }

    }
}
