namespace NS.Veterinary.Api.Notifications
{
    public interface INotifier : IDisposable
    {
        void Handle(Notification notification);
        IReadOnlyCollection<Notification> GetNotifications();
        bool HasNotification();
    }
}
