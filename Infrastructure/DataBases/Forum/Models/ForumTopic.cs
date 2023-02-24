using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataBases.Forum.Models;


public class ForumTopic
{
    public Guid ForumTopicId { get; set; }
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }

    //Creator
    public Guid userId { get; set; }
    public string userName { get; set; }

    //Когда кто то отвечает
    public DateTime? LastReplied { get; set; }
    public Guid? LastReplied_UserId { get; set; }
    public string? LastReplied_UserName { get; set; }
    public int CountReplies { get; set; } = 0;


    public Guid ForumForkId { get; set; }
    public ForumFork forumFork { get; set; }



    public List<ForumPost> forumPosts { get; set; } = new();
}



public class ForumTopicConfiguration : IEntityTypeConfiguration<ForumTopic>
{
    public void Configure(EntityTypeBuilder<ForumTopic> builder)
    {
        builder.HasKey(x => x.ForumTopicId);

        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.ShortDescription).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(4000).IsRequired();


        builder.Property(x => x.LastReplied).IsRequired();
        builder.Property(x => x.LastReplied_UserId).IsRequired();
        builder.Property(x => x.LastReplied_UserName).IsRequired();
        builder.Property(x => x.CountReplies).IsRequired();


        builder.HasOne(x => x.forumFork).WithMany(x => x.forumTopics).HasForeignKey(x => x.ForumForkId);

        builder.HasMany(x => x.forumPosts).WithOne(x => x.forumTopic).HasForeignKey(x => x.ForumTopicId);
    }


}

