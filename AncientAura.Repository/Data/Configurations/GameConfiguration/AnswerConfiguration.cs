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
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(A => A.Id);
            builder.Property(A => A.Id).UseIdentityColumn(1,1);

            builder.HasOne(A => A.AppUser)
                   .WithMany(AU => AU.Answers)
                   .HasForeignKey(A => A.AppUserId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(A => A.Question)
                   .WithOne(Q => Q.Answer)
                   .HasForeignKey<Answer>(A => A.QuestionId)
                   .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
