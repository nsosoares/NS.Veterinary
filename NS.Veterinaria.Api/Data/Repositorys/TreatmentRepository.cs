using Microsoft.EntityFrameworkCore;
using NS.Veterinary.Api.Models;
using NS.Veterinary.Api.Data.Context;
using NS.Veterinary.Api.Interfaces;

namespace NS.Veterinary.Api.Data.Repositorys
{
    public class TreatmentRepository : Repository<Treatment>, ITreatmentRepository
    {
        public TreatmentRepository(VeterinaryContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Treatment>> GetAllAsync()
        {
            return await _table.AsNoTracking()
                .Include(entity => entity.Veterinarian)
                .Include(entity => entity.Animal)
                .ToListAsync();
        }

        public async Task<IEnumerable<Treatment>> GetByAnimalIdAsync(Guid animalId)
            => await _table.AsNoTracking().Where(treatment => treatment.AnimalId == animalId).ToListAsync();

        public async Task<IEnumerable<Treatment>> GetByVeterinarianIdAsync(Guid veterinarianId)
            => await _table.AsNoTracking().Where(treatment => treatment.VeterinarianId == veterinarianId).ToListAsync();

    }
}
