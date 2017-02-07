using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using StrongGrid;
using StrongGrid.Model;

namespace AccessControl.Services
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private const string apiKey = "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";

        public Task SendEmailAsync(string email, string name, string subject, string message)
        {
            var client = new Client(apiKey);
            return client.Mail.SendToSingleRecipientAsync(new MailAddress(email, name), new MailAddress("noreply@houseme.co.za", "HouseME"), subject, message, message);
        }

        public Task SendSmsAsync(string number, string message)
        {
            var client = new HttpClient {BaseAddress = new Uri("https://api.twilio.com")};
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"AC3697e8cbb9febaad1de2c2ebfccaa040:f662407d786c78da925af841a61b6043")));

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("To",$"+{number}"),
                    new KeyValuePair<string, string>("From", "+14845524450"),
                    new KeyValuePair<string, string>("Body", message)
                });
         
               return client.PostAsync("/2010-04-01/Accounts/AC3697e8cbb9febaad1de2c2ebfccaa040/Messages.json", content);
                            
        }
    }
}
