using NS.Veterinaria.Api.Models;

namespace NS.Veterinary.Api.Interfaces
{
    public interface ITreatmentRepository : IRepository<Treatment>
    {
        Task<IEnumerable<Treatment>> GetByVeterinarianIdAsync(Guid veterinarianId);
        Task<IEnumerable<Treatment>> GetByAnimalIdAsync(Guid animalId);
    }
}
