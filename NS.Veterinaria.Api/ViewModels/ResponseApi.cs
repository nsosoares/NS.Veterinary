using NS.Veterinary.Api.Notifications;

namespace NS.Veterinary.Api.ViewModels
{
    public class ResponseApi
    {
        public ResponseApi(bool success, List<Notification> notifications, object data)
        {
            Success = success;
            Notifications = notifications;
            Data = data;
        }

        public bool Success { get; private set; }
        public List<Notification> Notifications { get; private set; }
        public object Data { get; private set; }

    }
}
