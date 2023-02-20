using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Net.Pf.DataBases.Forum.Models;


public class ForumTopic
{
    public Guid ForumTopicId { get; set; }
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }


    //Когда кто то отвечает
    public DateTime? LastReplied { get; set; }
    public Guid? LastReplied_UserId { get; set; }
    public string? LastReplied_UserName { get; set; }
    public int CountReplies { get; set; } = 0;



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



    }
}

