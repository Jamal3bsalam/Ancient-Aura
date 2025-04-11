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
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(Q => Q.Id);
            builder.Property(Q => Q.Id).UseIdentityColumn(1,1);

            builder.HasOne(Q => Q.Level)
                   .WithMany(L => L.Questions)
                   .HasForeignKey(Q => Q.LevelId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(Q => Q.Symbol)
                   .WithOne(S => S.Question)
                   .HasForeignKey<Symbol>(S => S.QuestionId)
                   .OnDelete(DeleteBehavior.SetNull);


        }
    }
}
