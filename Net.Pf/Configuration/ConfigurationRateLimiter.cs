using Microsoft.AspNetCore.RateLimiting;
using Microsoft.CodeAnalysis.Options;
using System.Threading.RateLimiting;


namespace Net.Pf.Configuration;


public static class ConfigurationRateLimiter
{

    /// <summary>
    /// [EnableRateLimiting("fixed")]
    /// [EnableRateLimiting("sliding")]
    /// app.MapRazorPages().RequireRateLimiting(slidingPolicy);
    /// app.MapDefaultControllerRoute().RequireRateLimiting(fixedPolicy);
    /// </summary>
    /// <param name="builder"></param>
    public static void Configure(this WebApplicationBuilder builder)
    {
        var myOptions = new RateLimitOptions();
        builder.Configuration.GetSection(RateLimitOptions.RateLimit).Bind(myOptions);


        builder.Services.AddRateLimiter(options => options
            .AddFixedWindowLimiter(policyName: RateLimiterPolicy.Fixed.ToString(), options =>
            {
                options.PermitLimit = myOptions.PermitLimit;
                options.Window = TimeSpan.FromSeconds(myOptions.Window);
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = myOptions.QueueLimit;
            }));



        builder.Services.AddRateLimiter(options => options
            .AddSlidingWindowLimiter(policyName: RateLimiterPolicy.Sliding.ToString(), options =>
            {
                options.PermitLimit = myOptions.PermitLimit;
                options.Window = TimeSpan.FromSeconds(myOptions.Window);
                options.SegmentsPerWindow = myOptions.SegmentsPerWindow;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = myOptions.QueueLimit;
            }));


        builder.Services.AddRateLimiter(options => options
            .AddConcurrencyLimiter(policyName: RateLimiterPolicy.Concurrency.ToString(), options =>
            {
                options.PermitLimit = myOptions.PermitLimit;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = myOptions.QueueLimit;
            }));


    }

    public enum RateLimiterPolicy
    {
        Fixed,
        Sliding,
        Concurrency
    }


    public class RateLimitOptions
    {
        public const string RateLimit = "RateLimit";
        public int PermitLimit { get; set; } = 100;
        public int Window { get; set; } = 10;
        public int ReplenishmentPeriod { get; set; } = 2;
        public int QueueLimit { get; set; } = 2;
        public int SegmentsPerWindow { get; set; } = 8;
        public int TokenLimit { get; set; } = 10;
        public int TokenLimit2 { get; set; } = 20;
        public int TokensPerPeriod { get; set; } = 4;
        public bool AutoReplenishment { get; set; } = false;
    }


}
