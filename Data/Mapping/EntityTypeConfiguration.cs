using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mobiroller.Data.Entity;

namespace Mobiroller.Data.Mapping
{
    public class EntityTypeConfiguration<TKey, TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreateDate)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}