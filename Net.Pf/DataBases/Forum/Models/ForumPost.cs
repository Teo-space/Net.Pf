using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Net.Pf.DataBases.Forum.Models;


public class ForumPost
{
    public Guid ForumPostId { get; set; }

    public string Text { get; set; }

    public Guid CreatorUserId { get; set; }
    public string CreatorUserName { get; set; }

    public DateTime addedAt { get; set; }
    public DateTime editedAt { get; set; }


    public Guid ForumTopicId { get; set; }
    public ForumTopic forumTopic { get; set; }
}




public class ForumPostConfiguration : IEntityTypeConfiguration<ForumPost>
{
    public void Configure(EntityTypeBuilder<ForumPost> builder)
    {
        builder.HasKey(x => x.ForumPostId);

        builder.Property(x => x.Text).IsRequired();

        builder.Property(x => x.CreatorUserId).IsRequired();
        builder.Property(x => x.CreatorUserName).HasMaxLength(100).IsRequired();

        builder.Property(x => x.addedAt).HasColumnType(nameof(DateTime)).IsRequired();
        builder.Property(x => x.editedAt).HasColumnType(nameof(DateTime)).IsRequired();

        builder.HasOne(x => x.forumTopic).WithMany(x => x.forumPosts)
            .HasForeignKey(x => x.ForumPostId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}


