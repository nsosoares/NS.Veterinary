using ErrorOr;
using FluentValidation.Results;

namespace NS.Veterinary.Api.Notifications
{
    public interface INotifier : IDisposable
    {
        void Handle(Error notification);
        IReadOnlyCollection<Error> GetNotifications();
        bool HasNotification();
    }
}
