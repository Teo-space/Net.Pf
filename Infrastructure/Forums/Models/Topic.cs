using Infrastructure.DataBases.Forum.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Forums.Models;


public class Topic
{
    public Guid TopicId { get; set; }
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


    public Guid ForumId { get; set; }
    public Forum Forum { get; set; }


	public List<Post> Posts { get; set; } = new();
}



public class ForumTopicConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.HasKey(x => x.TopicId);

        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.ShortDescription).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(4000).IsRequired();


        builder.Property(x => x.LastReplied).IsRequired();
        builder.Property(x => x.LastReplied_UserId).IsRequired();
        builder.Property(x => x.LastReplied_UserName).IsRequired();
        builder.Property(x => x.CountReplies).IsRequired();


        builder.HasOne(x => x.Forum)
            .WithMany(x => x.Topics)
            .HasForeignKey(x => x.ForumId);

        builder.HasMany(x => x.Posts)
            .WithOne(x => x.Topic)
            .HasForeignKey(x => x.TopicId)
			.OnDelete(DeleteBehavior.Cascade)
			;
	}


}