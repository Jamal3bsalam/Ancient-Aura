using AncientAura.Core.Entities.Community;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Repository.Data.Configurations.CommunityConfiguration
{
    public class ChatsConfigurations : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(C => C.Id);
            builder.Property(C => C.Id).UseIdentityColumn(1,1);

            builder.HasMany(C => C.Messages)
                   .WithOne(M => M.Chat)
                   .HasForeignKey(M => M.ChatId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(C => C.UserChats)
                   .WithOne(UC => UC.Chat)
                   .HasForeignKey(UC => UC.ChatId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
