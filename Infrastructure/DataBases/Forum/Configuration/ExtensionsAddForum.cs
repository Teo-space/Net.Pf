using Extensions.Configuration;
using FluentValidation.AspNetCore;
using Infrastructure.DataBases.Forum.DbContexts;
using Infrastructure.DataBases.Forum.Managers.ForkManager;
using Infrastructure.DataBases.Forum.Managers.PostManager;
using Infrastructure.DataBases.Forum.Managers.TopicManager;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DataBases.Forum.Configuration;

public static class ExtensionsAddForum
{
    public static IServiceCollection AddForum(this IServiceCollection services)
    {
        //ForumDbContext
        services.AddDbContext<ForumDbContext>(options =>
        {
            //options.UseSqlServer(connectionString)
            //options.UseInMemoryDatabase("ForumDbContext.db");
            //options.UseSqlite($"Data Source=Infrastructure/DataBases/Forum/Data/ForumDbContext.db");
            options.UseSqlite($"Data Source=ForumDbContext.db");
        });

        services.AddUserAccessor();
        services.AddScoped<IPostManager, PostManager>();
        services.AddScoped<ITopicManager, TopicManager>();
        services.AddScoped<IForkManager, ForkManager>();


		services.AddFluentValidationAutoValidation();
		services.AddFluentValidationClientsideAdapters();
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		return services;
    }


}


