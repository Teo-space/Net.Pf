using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataBases.Forum.Models;


public class ForumFork
{
    public Guid ForumForkId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }


    public List<ForumTopic> forumTopics { get; set; } = new();
}



public class ForumForkConfiguration : IEntityTypeConfiguration<ForumFork>
{
    public void Configure(EntityTypeBuilder<ForumFork> builder)
    {
        builder.HasKey(x => x.ForumForkId);

        builder.HasIndex(x => x.Name).IsUnique();


        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(255);

		builder.HasMany(x => x.forumTopics).WithOne(x => x.forumFork)
            .HasForeignKey(x => x.ForumForkId)
            //.OnDelete(DeleteBehavior.Cascade)
            ;
    }
}

