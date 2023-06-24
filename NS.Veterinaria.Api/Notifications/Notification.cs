namespace NS.Veterinary.Api.Notifications
{
    public class Notification
    {
        public Notification(string mensagem)
        {
            Message = mensagem;
        }

        public Notification(string field, string message)
        {
            Field = field;
            Message = message;
        }

        public int Code { get; private set; }
        public string Field { get; private set; }
        public string Message { get; private set; }
    }
}
