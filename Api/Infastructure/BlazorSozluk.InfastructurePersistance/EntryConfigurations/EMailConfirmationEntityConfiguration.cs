using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.InfastructurePersistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.InfastructurePersistance.EntryConfigurations
{
    public class EMailConfirmationEntityConfiguration: BaseEntityConfiguration<Api.Domain.Models.EMailConfirmation>
    {
        public override void Configure(EntityTypeBuilder<EMailConfirmation> builder)
        {
            base.Configure(builder);
            builder.ToTable("emailconfirmation", BlazorSozlukContext.DEFAULT_SCHEMA);
        }
    }
}
