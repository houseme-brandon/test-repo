using AccessControl.Domain.Services.Enums;
using AccessControl.Domain.UserAggregate;

namespace AccessControl.Domain.Services.Interfaces
{
    public interface INotificationDispatcher
    {
        void Dispatch(NotificationDispatchType type, User user, string message);
    }
}
