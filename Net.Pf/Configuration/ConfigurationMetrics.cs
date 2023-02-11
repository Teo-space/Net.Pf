namespace Net.Pf.Configuration
{
    public static class ConfigurationMetrics
    {
        public static void Configure(this WebApplicationBuilder builder)
        {

            //ConfigureMiniProfiler(builder);



        }

        static void ConfigureMiniProfiler(WebApplicationBuilder builder)
        {
            builder.Services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler";
                options.TrackConnectionOpenClose = true;
                options.ColorScheme = StackExchange.Profiling.ColorScheme.Dark;

                options.EnableMvcFilterProfiling = true;
                options.MvcFilterMinimumSaveMs = 1.0m;
                options.EnableMvcViewProfiling = true;

            })
            .AddEntityFramework();
        }
    }


}
