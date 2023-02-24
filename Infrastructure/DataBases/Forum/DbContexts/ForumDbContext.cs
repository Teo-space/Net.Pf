﻿using Infrastructure.DataBases.Forum.Models;

namespace Infrastructure.DataBases.Forum.DbContexts;


public class ForumDbContext : DbContext
{
    public DbSet<ForumFork> Forks { get; set; }
    public DbSet<ForumTopic> Topics { get; set; }
    public DbSet<ForumPost> Posts { get; set; }



    public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}


