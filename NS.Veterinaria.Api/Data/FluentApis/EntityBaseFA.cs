using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NS.Veterinary.Api.Models;

namespace NS.Veterinary.Api.Data.FluentApis
{
    public abstract class EntityBaseFA<TEntityBase> : EntityFA<TEntityBase> where TEntityBase : EntityBase
    {
        public override void Configure(EntityTypeBuilder<TEntityBase> builder)
        {
            base.Configure(builder);
            builder.HasIndex(entityBase => entityBase.Name);
            builder.Property(entityBase => entityBase.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("VARCHAR");
        }
    }
}
