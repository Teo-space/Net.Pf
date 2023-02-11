using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;


namespace Net.Pf.Configuration;


public static class ConfigurationRateLimiter
{
    //[EnableRateLimiting("fixed")]
    //[EnableRateLimiting("sliding")]
    public static void Configure(this WebApplicationBuilder builder)
    {
        var myOptions = new RateLimitOptions();
        builder.Configuration.GetSection(RateLimitOptions.RateLimit).Bind(myOptions);
        var fixedPolicy = "fixed";

        builder.Services.AddRateLimiter(_ => _
            .AddFixedWindowLimiter(policyName: fixedPolicy, options =>
            {
                options.PermitLimit = myOptions.PermitLimit;
                options.Window = TimeSpan.FromSeconds(myOptions.Window);
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = myOptions.QueueLimit;
            }));


        var slidingPolicy = "sliding";
        builder.Services.AddRateLimiter(_ => _
            .AddSlidingWindowLimiter(policyName: slidingPolicy, options =>
            {
                options.PermitLimit = myOptions.PermitLimit;
                options.Window = TimeSpan.FromSeconds(myOptions.Window);
                options.SegmentsPerWindow = myOptions.SegmentsPerWindow;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = myOptions.QueueLimit;
            }));

        var concurrencyPolicy = "Concurrency";
        builder.Services.AddRateLimiter(_ => _
            .AddConcurrencyLimiter(policyName: concurrencyPolicy, options =>
            {
                options.PermitLimit = myOptions.PermitLimit;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = myOptions.QueueLimit;
            }));
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
