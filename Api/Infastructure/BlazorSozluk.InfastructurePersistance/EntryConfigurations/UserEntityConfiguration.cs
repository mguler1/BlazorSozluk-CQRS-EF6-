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
    public class UserEntityConfiguration: BaseEntityConfiguration<Api.Domain.Models.User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.ToTable("user", BlazorSozlukContext.DEFAULT_SCHEMA);
        }
    }
}
