namespace NS.Veterinary.Api.Notifications
{
    public sealed class Notifier : INotifier
    {
        private List<Notification> _notifications;
        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public IReadOnlyCollection<Notification> GetNotifications()
            => _notifications;

        public void Handle(Notification notification)
            => _notifications.Add(notification);

        public bool HasNotification()
            => _notifications.Any();

        public void Dispose()
        {
            _notifications.Clear();
        }
    }
}
