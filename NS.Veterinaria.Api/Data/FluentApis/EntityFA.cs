using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NS.Veterinaria.Api.Models;

namespace NS.Veterinary.Api.Data.FluentApis
{
    public abstract class EntityFA<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.HasIndex(entity => entity.Id);
            builder.ToTable(typeof(TEntity).Name);
        }
    }
}
