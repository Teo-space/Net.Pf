namespace Net.Pf.Configuration
{
    public static class ConfigurationMetrics
    {
        public static void Configure(this IServiceCollection services)
        {

            //ConfigureMiniProfiler(builder);



        }

        static void ConfigureMiniProfiler(this IServiceCollection services)
        {
            services.AddMiniProfiler(options =>
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
