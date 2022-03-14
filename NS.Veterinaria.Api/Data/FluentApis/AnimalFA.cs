using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NS.Veterinaria.Api.Models;

namespace NS.Veterinary.Api.Data.FluentApis
{
    public class AnimalFA : EntityBaseFA<Animal>
    {
        public override void Configure(EntityTypeBuilder<Animal> builder)
        {
            base.Configure(builder);
            builder.Property(animal => animal.Age)
                .IsRequired();
            builder.Property(animal => animal.Race)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("VARCHAR(50)");
            builder.Property(animal => animal.Size)
                .IsRequired();
        }
    }
}
