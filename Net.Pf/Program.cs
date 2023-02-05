using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Net.Pf.Data;

namespace Net.Pf
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Configure(builder, builder.Services);

            var app = builder.Build();

            Use(app);

            app.Run();
        }


        static void Configure(WebApplicationBuilder builder, IServiceCollection services)
        {
            Configuration.Configuration.ConfigureAll(builder);



            services.AddRazorPages();
        }


        static void Use(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
        }


    }


}