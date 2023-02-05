using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Net.Pf.Configuration;
using Net.Pf.Data;


namespace Net.Pf;


public class Program
{
    public static void Main(string[] args)
    {
        WebApplication
            .CreateBuilder(args)
            .ConfigureAll()
            .Build()
            .Use()
            .Run();
    }
}