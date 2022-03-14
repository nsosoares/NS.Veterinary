using NS.Veterinary.Api.Data.Context;
using NS.Veterinary.Api.Interfaces;

namespace NS.Veterinary.Api.Data.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VeterinaryContext _context;
        public UnitOfWork(VeterinaryContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
