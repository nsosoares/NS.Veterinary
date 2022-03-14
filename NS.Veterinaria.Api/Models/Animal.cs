using NS.Veterinaria.Api.Enums;

namespace NS.Veterinaria.Api.Models
{
    public class Animal : EntityBase
    {
        //Constructor EF
        public Animal() { }

        public Animal(Guid id, DateTime creationDate, string name, int age, string race, ESize size) 
            : base(id, creationDate, name)
        {
            Age = age;
            Race = race;
            Size = size;
        }

        public int Age { get; private set; }
        public string Race { get; private set; }
        public ESize Size { get; private set; }

        //NAVIGATION PROPERTIES
        public IReadOnlyCollection<Treatment> Treatments { get; private set; }
    }
}
