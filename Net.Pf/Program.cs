using Net.Pf.Configuration;

WebApplication
    .CreateBuilder(args)
    .ConfigureAll()
    .Build()
    .Use()
    .Run();