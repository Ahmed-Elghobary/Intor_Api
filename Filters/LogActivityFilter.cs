using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace ApiBeginner.Filters
{
    public class LogActivityFilter : IActionFilter,IAsyncActionFilter
    {
        private readonly ILogger<LogActivityFilter> _logger;

        public LogActivityFilter(ILogger<LogActivityFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Executing action {context.ActionDescriptor.DisplayName} on controller {context.Controller} with Arguments {JsonSerializer.Serialize( context.ActionArguments)}");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"Executing action {context.ActionDescriptor.DisplayName} Finished on controller {context.Controller}");

        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"(Async)  Executing action {context.ActionDescriptor.DisplayName} on controller {context.Controller} with Arguments {JsonSerializer.Serialize( context.ActionArguments)}");
            await next();
            _logger.LogInformation($"Executing action {context.ActionDescriptor.DisplayName} Finished on controller {context.Controller}");

        }
    }
}
