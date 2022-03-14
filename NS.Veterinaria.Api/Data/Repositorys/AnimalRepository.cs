using NS.Veterinaria.Api.Models;
using NS.Veterinary.Api.Data.Context;
using NS.Veterinary.Api.Interfaces;

namespace NS.Veterinary.Api.Data.Repositorys
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        public AnimalRepository(VeterinaryContext context) : base(context)
        {
        }
    }
}
