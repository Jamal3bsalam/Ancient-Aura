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
    public class LevelConfiguration : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder.HasKey(L => L.Id);
            builder.Property(L => L.Id).UseIdentityColumn(1, 1);

            builder.HasOne(L => L.Game)
                   .WithMany(G => G.Levels)
                   .HasForeignKey(L => L.GameId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
