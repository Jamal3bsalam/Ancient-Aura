using AncientAura.Core.Entities.GameEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Repository.Data.Configurations.GameConfiguration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(G => G.Id);
            builder.Property(G => G.Id).UseIdentityColumn(1,1);

            builder.HasOne(G =>G.AppUser)
                   .WithMany(A => A.Games)
                   .HasForeignKey(G => G.AppUserId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
