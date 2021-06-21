using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mobiroller.Data.Entity;

namespace Mobiroller.Data.Mapping
{
    public class LocaleMapping : EntityTypeConfiguration<int, Locale>
    {
        public override void Configure(EntityTypeBuilder<Locale> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired();

            base.Configure(builder);
        }
    }
}