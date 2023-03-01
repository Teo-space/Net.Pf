using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Forums.Models;

public class Post
{
    public Guid PostId { get; set; }

    public string Text { get; set; }
    public DateTime addedAt { get; set; }
    public DateTime editedAt { get; set; }

    public Guid CreatorUserId { get; set; }
    public string CreatorUserName { get; set; }


    public Guid TopicId { get; set; }
    public Topic Topic { get; set; }
}




public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(x => x.PostId);

        builder.Property(x => x.Text).IsRequired().HasMaxLength(1000);


        builder.Property(x => x.addedAt).HasColumnType(nameof(DateTime)).IsRequired();
        builder.Property(x => x.editedAt).HasColumnType(nameof(DateTime)).IsRequired();


        builder.Property(x => x.CreatorUserId).IsRequired();
        builder.Property(x => x.CreatorUserName).HasMaxLength(100).IsRequired();



        builder.HasOne(x => x.Topic).WithMany(x => x.Posts)
            ;
    }

}
