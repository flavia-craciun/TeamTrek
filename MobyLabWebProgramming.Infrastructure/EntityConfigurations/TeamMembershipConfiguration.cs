using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class TeamMembershipConfiguration : IEntityTypeConfiguration<TeamMembership>
{
    public void Configure(EntityTypeBuilder<TeamMembership> builder)
    {
        builder.Property(tm => tm.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        builder.HasKey(tm => tm.Id);
        builder.HasOne(tm => tm.Team)
            .WithMany(t => t.TeamMemberships)
            .HasForeignKey(tm => tm.TeamId)
            .IsRequired();
        builder.HasOne(tm => tm.User)
            .WithOne(t => t.Membership)
            .HasForeignKey<TeamMembership>(tm => tm.UserId)
            .IsRequired();
    }
}