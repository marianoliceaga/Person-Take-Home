
using System.Diagnostics;

namespace PersonAPI.WebApi.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestId = Guid.NewGuid().ToString("N")[..8];

        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["RequestId"] = requestId,
            ["Method"] = context.Request.Method,
            ["Path"] = context.Request.Path,
            ["QueryString"] = context.Request.QueryString.ToString()
        }))
        {
            _logger.LogInformation("Starting request {RequestId} {Method} {Path}",
                requestId, context.Request.Method, context.Request.Path);

            await _next(context);

            stopwatch.Stop();

            _logger.LogInformation("Completed request {RequestId} in {ElapsedMilliseconds}ms with status {StatusCode}",
                requestId, stopwatch.ElapsedMilliseconds, context.Response.StatusCode);
        }
    }
}


