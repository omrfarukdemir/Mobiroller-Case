using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mobiroller.Data.Entity;

namespace Mobiroller.Data.Mapping
{
    public class IncidentMapping : EntityTypeConfiguration<int, Incident>
    {
        public override void Configure(EntityTypeBuilder<Incident> builder)
        {
            builder.Property(x => x.Category)
                .IsRequired();

            builder.Property(x => x.Time)
                .IsRequired();

            builder.Property(x => x.Event)
                .IsRequired();

            builder.HasOne(x => x.Locale)
                .WithMany(x => x.Incidents)
                .HasForeignKey(x => x.LocaleId);

            base.Configure(builder);
        }
    }
}