using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Net.Pf.DataBases.Forum.Models;


public class ForumFork
{
    public Guid ForumForkId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }


    public List<ForumTopic> forumTopics { get; set; } = new();
}

//ForumTopic


public class ForumForkConfiguration : IEntityTypeConfiguration<ForumFork>
{
    public void Configure(EntityTypeBuilder<ForumFork> builder)
    {
        builder.HasKey(x => x.ForumForkId);

        builder.HasIndex(x => x.Name).IsUnique();


        builder.Property(x => x.Name).IsRequired();
        builder.Property(x =>x.Description).IsRequired();

        builder.HasMany(x => x.forumTopics).WithOne(x => x.forumFork)
            .HasForeignKey(x => x.ForumForkId)
            //.OnDelete(DeleteBehavior.Cascade)
            ;
    }
}

