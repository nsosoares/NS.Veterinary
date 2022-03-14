namespace NS.Veterinaria.Api.Models
{
    public abstract class EntityBase : Entity
    {
        //Constructor EF
        protected EntityBase() { }

        protected EntityBase(Guid id, DateTime creationDate, string name) : base(id, creationDate)
        {
            Name = name;
        }

        public string Name { get; protected set; }
    }
}
