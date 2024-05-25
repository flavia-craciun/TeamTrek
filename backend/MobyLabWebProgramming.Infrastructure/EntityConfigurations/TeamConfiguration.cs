using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id);
        builder.HasAlternateKey(e => e.TeamName);
        builder.Property(e => e.TeamName)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.TeamLeaderId)
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
        builder.HasOne(e => e.TeamLeader)
            .WithMany()
            .HasForeignKey(e => e.TeamLeaderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}