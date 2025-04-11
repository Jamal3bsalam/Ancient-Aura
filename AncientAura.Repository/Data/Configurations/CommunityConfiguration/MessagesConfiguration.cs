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
    public class MessagesConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(M => M.Id);
            builder.Property(M => M.Id).UseIdentityColumn(1, 1);
        }
    }
}
