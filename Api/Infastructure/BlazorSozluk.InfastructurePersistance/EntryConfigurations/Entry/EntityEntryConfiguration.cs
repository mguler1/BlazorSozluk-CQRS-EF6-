using BlazorSozluk.InfastructurePersistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.InfastructurePersistance.EntryConfigurations.Entry
{
    public class EntityEntryConfiguration : BaseEntityConfiguration<Api.Domain.Models.Entry>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Models.Entry> builder)
        {
            base.Configure(builder);
            builder.ToTable("entry", BlazorSozlukContext.DEFAULT_SCHEMA);
            builder.HasOne(x => x.CreatedBy)
                .WithMany(x => x.Entries)
                .HasForeignKey(x => x.CreatedById);
        }
    }
}
