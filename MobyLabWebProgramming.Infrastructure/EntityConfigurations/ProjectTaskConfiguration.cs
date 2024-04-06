using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id);
        builder.Property(e => e.TaskName)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.Description)
            .HasMaxLength(255);
        builder.Property(e => e.Status)
            .HasMaxLength(16)
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
        builder.HasOne(e => e.Project)
            .WithMany(e => e.ProjectTasks)
            .HasForeignKey(e => e.ProjectId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(e => e.AssignedToUser)
            .WithMany(u => u.ProjectTasks)
            .HasForeignKey(e => e.AssignedToUserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}