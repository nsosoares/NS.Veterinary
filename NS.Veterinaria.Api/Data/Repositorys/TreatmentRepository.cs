using Microsoft.EntityFrameworkCore;
using NS.Veterinaria.Api.Models;
using NS.Veterinary.Api.Data.Context;
using NS.Veterinary.Api.Interfaces;

namespace NS.Veterinary.Api.Data.Repositorys
{
    public class TreatmentRepository : Repository<Treatment>, ITreatmentRepository
    {
        public TreatmentRepository(VeterinaryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Treatment>> GetByAnimalIdAsync(Guid animalId)
            => await _table.AsNoTracking().Where(treatment => treatment.AnimalId == animalId).ToListAsync();

        public async Task<IEnumerable<Treatment>> GetByVeterinarianIdAsync(Guid veterinarianId)
            => await _table.AsNoTracking().Where(treatment => treatment.VeterinarianId == veterinarianId).ToListAsync();

    }
}
