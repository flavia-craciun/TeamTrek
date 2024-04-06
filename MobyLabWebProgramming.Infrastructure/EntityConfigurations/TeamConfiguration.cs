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
        builder.Property(e => e.TeamName)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.TeamLeaderId)
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
        builder.HasMany(e => e.TeamMemberships)
            .WithOne(e => e.Team)
            .HasForeignKey(e => e.TeamId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(e => e.TeamLeader)
            .WithMany()
            .HasForeignKey(e => e.TeamLeaderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}