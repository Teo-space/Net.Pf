using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Forums.Models;


public class Forum
{
    public Guid ForumId { get; set; }

    public ForumGroup Group { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }



    public List<Topic> Topics { get; set; } = new();
}

//public record ForumDto(Guid ForumId, ForumGroup Group, string Name, string Description);



public class ForumConfiguration : IEntityTypeConfiguration<Forum>
{
    public void Configure(EntityTypeBuilder<Forum> builder)
    {
        builder.HasKey(x => x.ForumId);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Group).IsRequired()
            .HasConversion(group => group.ToString(), value => Enum.Parse<ForumGroup>(value))
            ;

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(255);

        builder.HasMany(x => x.Topics).WithOne(x => x.Forum).HasForeignKey(x => x.ForumId)
            .OnDelete(DeleteBehavior.Cascade)
            ;
    }
}

