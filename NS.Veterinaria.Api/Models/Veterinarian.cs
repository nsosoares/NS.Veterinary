namespace NS.Veterinary.Api.Models
{
    public class Veterinarian : EntityBase
    {
        //Constructor EF
        public Veterinarian() { }

        public Veterinarian(Guid id, DateTime creationDate, string name, string crmv) 
            : base(id, creationDate, name)
        {
            Crmv = crmv;
        }

        public string Crmv { get; private set; }

        //NAVIGATION PROPERTIES
        public IReadOnlyCollection<Treatment> Treatments { get; private set; }
    }
}
