using NS.Veterinary.Api.Notifications;

namespace NS.Veterinary.Api.Exceptions
{
    public class NotifierException : Exception
    {
        public NotifierException(IReadOnlyCollection<Notification> notifications)
        {
            Notifications = notifications;
        }

        public IReadOnlyCollection<Notification> Notifications { get; }
    }
}
