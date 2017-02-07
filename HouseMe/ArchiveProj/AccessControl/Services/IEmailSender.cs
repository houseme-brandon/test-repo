using System.Threading.Tasks;

namespace AccessControl.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string name, string subject, string message);
    }
}
