namespace NS.Veterinaria.Api.Models
{
    public class Treatment : Entity
    {
        //Constructor EF
        public Treatment() { }

        public Treatment(Guid id, DateTime creationDate, Guid veterinarianId, Guid animalId, string recipe)
            : base(id, creationDate)
        {
            VeterinarianId = veterinarianId;
            AnimalId = animalId;
            Recipe = recipe;
        }

        public Guid VeterinarianId { get; private set; }
        public Guid AnimalId { get; private set; }
        public string Recipe { get; private set; }

        //NAVIGATION PROPERTIES
        public Veterinarian Veterinarian { get; private set; }
        public Animal Animal { get; private set; }
    }
}
