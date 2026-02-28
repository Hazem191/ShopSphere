using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace ShopSphere.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtpHost = _configuration["EmailSettings:SmtpHost"];
            var smtpPortStr = _configuration["EmailSettings:SmtpPort"];
            var smtpUser = _configuration["EmailSettings:SmtpUser"];
            var smtpPass = _configuration["EmailSettings:SmtpPass"];

            // VALIDATION: Prevent System.FormatException by checking for missing credentials
            if (string.IsNullOrWhiteSpace(smtpHost) || 
                string.IsNullOrWhiteSpace(smtpUser) || 
                string.IsNullOrWhiteSpace(smtpPass) ||
                string.IsNullOrWhiteSpace(email))
            {
                // In Development, we skip email if not configured correctly.
                System.Diagnostics.Debug.WriteLine("EmailSender: Missing SMTP configuration in User Secrets or AppSettings.");
                return;
            }

            if (!int.TryParse(smtpPortStr, out int smtpPort)) { smtpPort = 587; }

            try 
            {
                using var client = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUser, smtpPass),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpUser, "ShopSphere"),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Log the exception locally to help the user debug
                System.Diagnostics.Debug.WriteLine($"EmailSender Error: {ex.Message}");
                // In production, use a proper logger here
            }
        }
    }
}
