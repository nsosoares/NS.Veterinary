using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NS.Veterinary.Api.Models;

namespace NS.Veterinary.Api.Data.FluentApis
{
    public class TreatmentFA : EntityFA<Treatment>
    {
        public override void Configure(EntityTypeBuilder<Treatment> builder)
        {
            base.Configure(builder);
            builder.HasIndex(treatment => treatment.VeterinarianId);
            builder.HasIndex(treatment => treatment.AnimalId);

            builder.Property(treatmnet => treatmnet.Recipe)
                .IsRequired(false)
                .HasMaxLength(250)
                .HasColumnType("VARCHAR(250)");

            builder.HasOne(treatment => treatment.Veterinarian)
                .WithMany(veterinarian => veterinarian.Treatments)
                .HasForeignKey(treatment => treatment.VeterinarianId);
            builder.HasOne(treatment => treatment.Animal)
                .WithMany(animal => animal.Treatments)
                .HasForeignKey(treatment => treatment.AnimalId);
        }
    }
}
