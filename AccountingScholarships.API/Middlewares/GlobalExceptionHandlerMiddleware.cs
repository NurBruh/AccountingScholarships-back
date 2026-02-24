using System.Net;
using System.Text.Json;

namespace AccountingScholarships.API.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (FluentValidation.ValidationException ex)
        {
            _logger.LogWarning(ex, "Ошибка валидации: {Errors}", string.Join(", ", ex.Errors.Select(e => e.ErrorMessage)));
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var response = new
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Ошибка валидации",
                Errors = ex.Errors.Select(e => new { Field = e.PropertyName, Error = e.ErrorMessage }),
                CorrelationId = context.Items["CorrelationId"]?.ToString()
            };

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Бизнес-ошибка: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Непредвиденная ошибка: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            StatusCode = (int)statusCode,
            Message = statusCode == HttpStatusCode.InternalServerError
                ? "Произошла внутренняя ошибка сервера."
                : exception.Message,
            CorrelationId = context.Items["CorrelationId"]?.ToString()
        };

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}