using InFatec.API.Util.Interfaces;
using System.Net;
using System.Net.Mail;

namespace InFatec.API.Util
{
    public class EmailUtil : IEmailUtil
    {
        private IConfiguration _configuration;
        public EmailUtil(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmail(string email, string subject, string body)
        {
            try
            {
                var all = new
                {
                    host = _configuration.GetValue<string>("SMTP:Host"),
                    Name = _configuration.GetValue<string>("SMTP:Name"),
                    username = _configuration.GetValue<string>("SMTP:Username"),
                    password = _configuration.GetValue<string>("SMTP:Password"),
                    port = _configuration.GetValue<int>("SMTP:Port"),
                };

                var mail = new MailMessage() { From = new MailAddress ( all.username, all.Name ) };
                mail.Subject = subject;
                mail.To.Add(email);
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(all.host, all.port))
                {
                    smtp.Credentials = new NetworkCredential(all.username, all.password);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                    return true;
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
    }
}
