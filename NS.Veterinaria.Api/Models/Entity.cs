namespace NS.Veterinaria.Api.Models
{
    public abstract class Entity
    {
        //Constructor EF
        public Entity() { }

        protected Entity(Guid id, DateTime creationDate)
        {
            Id = id;
            CreationDate = creationDate;
        }

        public void ToGenerate()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public Guid Id { get; protected set; }
        public DateTime CreationDate { get; protected set; }
    }
}
