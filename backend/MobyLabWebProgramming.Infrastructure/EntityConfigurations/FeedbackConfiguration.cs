using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
	public void Configure(EntityTypeBuilder<Feedback> builder)
	{
		builder.Property(e => e.Id)
			.IsRequired();
		builder.HasKey(x => x.Id);
		builder.Property(e => e.Rating)
			.IsRequired();
		builder.Property(e => e.FrequentedSection)
			.HasMaxLength(25)
			.IsRequired();
		builder.Property(e => e.Suggestion)
			.HasMaxLength(255);
		builder.Property(e => e.ResponseWanted)
			.IsRequired();

		builder.HasIndex(e => e.UserId)
			.IsUnique();
	}
}