using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class ProjectMembershipConfiguration : IEntityTypeConfiguration<ProjectMembership>
{
    public void Configure(EntityTypeBuilder<ProjectMembership> builder)
    {
        builder.Property(tm => tm.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        builder.HasKey(tm => tm.Id);
        builder.HasOne(pm => pm.Project)
            .WithMany(t => t.ProjectMemberships)
            .HasForeignKey(pm => pm.ProjectId);
        builder.HasOne(pm => pm.User)
            .WithMany(u => u.ProjectMemberships)
            .HasForeignKey(pm => pm.UserId);
    }
}