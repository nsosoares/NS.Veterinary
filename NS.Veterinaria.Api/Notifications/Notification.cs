namespace NS.Veterinary.Api.Notifications
{
    public class Notification
    {
        public Notification(string mensagem)
        {
            Mensagem = mensagem;
        }

        public Notification(int codigo, string mensagem)
        {
            Codigo = codigo;
            Mensagem = mensagem;
        }

        public int Codigo { get; private set; }
        public string Mensagem { get; private set; }
    }
}
