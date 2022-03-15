using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NS.Veterinary.Api.Models;

namespace NS.Veterinary.Api.Data.FluentApis
{
    public class VeterinarianFA : EntityBaseFA<Veterinarian>
    {
        public override void Configure(EntityTypeBuilder<Veterinarian> builder)
        {
            base.Configure(builder);
            builder.Property(veterinarian => veterinarian.Crmv)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("VARCHAR(50)");
        }
    }
}
