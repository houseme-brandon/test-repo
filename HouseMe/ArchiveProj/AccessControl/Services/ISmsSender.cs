using System.Threading.Tasks;

namespace AccessControl.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
