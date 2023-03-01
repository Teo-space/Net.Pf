using Extensions.Configuration;
using FluentValidation.AspNetCore;
using Infrastructure.Forums.DbContexts;
using Infrastructure.Forums.Managers.ForumManager;
using Infrastructure.Forums.Managers.PostManager;
using Infrastructure.Forums.Managers.TopicManager;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Forums.Configuration;


public static class ExtensionsAddForum
{
    public static IServiceCollection AddForums(this IServiceCollection services)
    {
        //ForumDbContext
        services.AddDbContext<ForumDbContext>(options =>
        {
            //options.UseSqlServer(connectionString)
            //options.UseInMemoryDatabase("ForumDbContext.db");
            //options.UseSqlite($"Data Source=Infrastructure/DataBases/Forum/Data/ForumDbContext.db");
            options.UseSqlite($"Data Source=ForumDbContext2.db");
        });

        services.AddUserAccessor();

        services.AddScoped<IForumManager, ForumManager>();
        services.AddScoped<ITopicManager, TopicManager>();
        services.AddScoped<IPostManager, PostManager>();


        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        


        return services;
    }


}