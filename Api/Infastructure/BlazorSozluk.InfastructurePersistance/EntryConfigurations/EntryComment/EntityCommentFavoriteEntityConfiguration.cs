using BlazorSozluk.InfastructurePersistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.InfastructurePersistance.EntryConfigurations.EntryComment
{
    public class EntityCommentFavoriteEntityConfiguration: BaseEntityConfiguration<Api.Domain.Models.EntryCommentFavorite>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Models.EntryCommentFavorite> builder)
        {
            base.Configure(builder);
            builder.ToTable("entrycommentfavorite", BlazorSozlukContext.DEFAULT_SCHEMA);
            builder.HasOne(i => i.EntryComment)
                 .WithMany(i => i.EntryCommentFavorite).
          HasForeignKey(i => i.EntryCommentId);
            builder.HasOne(i => i.CreatedUser)
                 .WithMany(i => i.EntryCommentFavorites)
                 .HasForeignKey(i => i.CreatedById);
        }
    }
}
