namespace Net.Pf.Configuration;


public static class ConfigurationUse
{
    public static WebApplication Use(this WebApplication app)
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
        app.UseSession();





        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();



        app.MapRazorPages();




        return app;
    }


}
