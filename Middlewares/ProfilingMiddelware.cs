using System.Diagnostics;

namespace ApiBeginner.Middlewares
{
    public class ProfilingMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProfilingMiddelware> _logger;

        public ProfilingMiddelware(RequestDelegate next, ILogger<ProfilingMiddelware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await _next(context);
            stopWatch.Stop();
            _logger.LogInformation($"request {context.Request.Path} took {stopWatch.ElapsedMilliseconds}ms ");
        }
    }
}
