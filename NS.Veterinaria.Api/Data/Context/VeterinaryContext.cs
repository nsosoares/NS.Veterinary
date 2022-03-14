using Microsoft.EntityFrameworkCore;
using NS.Veterinaria.Api.Models;

namespace NS.Veterinary.Api.Data.Context
{
    public class VeterinaryContext : DbContext
    {
        public VeterinaryContext(DbContextOptions<VeterinaryContext> options) : base(options) { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Veterinarian> Veterinarians { get; set; }
        public DbSet<Treatment> Treatments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VeterinaryContext).Assembly);
        }
    }
}
