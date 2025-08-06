
using System.Net;

namespace PersonAPI.WebApi.Middleware;

public class ClientIdValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ClientIdValidationMiddleware> _logger;
    private const string ClientIdHeader = "x-client-id";
    private const string ValidClientId = "PersonAPI-Client-2024";

    public ClientIdValidationMiddleware(RequestDelegate next, ILogger<ClientIdValidationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip validation for health checks, swagger, and other system endpoints
        if (ShouldSkipValidation(context.Request.Path))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue(ClientIdHeader, out var clientIdValues))
        {
            _logger.LogWarning("Missing {HeaderName} header from {Path}", ClientIdHeader, context.Request.Path);
            await WriteErrorResponse(context, HttpStatusCode.BadRequest, $"Missing required header: {ClientIdHeader}");
            return;
        }

        var clientId = clientIdValues.FirstOrDefault();
        if (string.IsNullOrWhiteSpace(clientId))
        {
            _logger.LogWarning("Empty {HeaderName} header from {Path}", ClientIdHeader, context.Request.Path);
            await WriteErrorResponse(context, HttpStatusCode.BadRequest, $"Header {ClientIdHeader} cannot be empty");
            return;
        }

        if (clientId != ValidClientId)
        {
            _logger.LogWarning("Invalid {HeaderName} header value: {ClientId} from {Path}", 
                ClientIdHeader, clientId, context.Request.Path);
            await WriteErrorResponse(context, HttpStatusCode.Unauthorized, "Invalid client ID");
            return;
        }

        await _next(context);
    }

    private static bool ShouldSkipValidation(PathString path)
    {
        var pathValue = path.Value?.ToLowerInvariant() ?? string.Empty;
        
        return pathValue.StartsWith("/health") ||
               pathValue.StartsWith("/swagger") ||
               pathValue.StartsWith("/api/health") ||
               pathValue == "/" ||
               pathValue == "/favicon.ico";
    }

    private static async Task WriteErrorResponse(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = message,
            timestamp = DateTime.UtcNow,
            path = context.Request.Path.Value
        };

        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
}


