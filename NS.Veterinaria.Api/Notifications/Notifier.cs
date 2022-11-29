using ErrorOr;
using FluentValidation.Results;

namespace NS.Veterinary.Api.Notifications
{
    public sealed class Notifier : INotifier
    {
        private List<Error> _notifications;
        public Notifier()
        {
            _notifications = new List<Error>();
        }

        public IReadOnlyCollection<Error> GetNotifications()
            => _notifications.ToList();

        public void Handle(Error notification)
            => _notifications.Add(notification);

        public bool HasNotification()
            => _notifications.Any();

        public void Dispose()
        {
            _notifications.Clear();
        }
    }
}
