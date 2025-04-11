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
    public class ScoreConfiguration : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            builder.HasKey(S => S.Id);
            builder.HasOne(S => S.Answer)
                   .WithOne(A => A.Score)
                   .HasForeignKey<Score>(S => S.AnswerId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
