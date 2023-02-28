using System.Text.RegularExpressions;

namespace Infrastructure.Forums;

public record Forum(ForumGroup group, string Name, string Description);

public static class ForumGroupper
{
    //public static IReadOnlyList<Forum> Forums { get; set; } = new List<Forum>();

    public static IReadOnlyList<Forum> Create()
    {
        var forums = new List<Forum>();
        int lim = 7;

        for(int i = 0; i < lim; i++)
        {
            forums.Add(new Forum(
                ForumGroup.General, 
                "General Discussion",
                "Talk about sports, entertainment, music, movies, your favorite color, talk about enything."));
        }

        for (int i = 0; i < lim; i++)
        {
            forums.Add(new Forum(
                ForumGroup.Android,
                "Android Discussion",
                "Various versions have evolved over the years, sometimes by accident."));
        }

        for (int i = 0; i < lim; i++)
        {
            forums.Add(new Forum(
                ForumGroup.Other,
                "Lorem Ipsum is simply dummy text.",
                "Various versions have evolved over the years, sometimes by accident, sometimes on purpose passage of Lorem Ipsum (injected humour and the like)."));
        }





        return forums;
    }


    public static IReadOnlyList<IGrouping<ForumGroup, Forum>> Order(IReadOnlyList<Forum> forums)
        => forums.OrderBy(x => x.group).ThenBy(x => x.Name).GroupBy(x => x.group).ToList();


}
