using NS.Veterinaria.Api.Models;
using NS.Veterinary.Api.Data.Context;
using NS.Veterinary.Api.Interfaces;

namespace NS.Veterinary.Api.Data.Repositorys
{
    public class VeterinarianRepository : Repository<Veterinarian>, IVeterinarianRepository
    {
        public VeterinarianRepository(VeterinaryContext context) : base(context)
        {
        }
    }
}
