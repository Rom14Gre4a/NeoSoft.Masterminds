using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NeoSoft.Masterminds.Domain.Models.Exceptions;
using NeoSoft.Masterminds.Domain.Models.Responses;
using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationErrorException validationErrorException)
            {
                var options = new JsonSerializerOptions
                {
                    IgnoreNullValues = true,
                    PropertyNameCaseInsensitive = true,
                };
                options.Converters.Add(new JsonStringEnumConverter());
                var validationData = JsonSerializer.Serialize(validationErrorException.ValidationMessages, options);


                _logger.LogError($"Validation Error Exception. Message -> {validationErrorException.Message}. Data -> {validationData}. StackTrace -> {validationErrorException.StackTrace}");

               
                   await HandleExceptionAsync(context, new ApiResponse
                   {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    ErrorMessage = validationErrorException.Message,
                    ValidationMessages = validationErrorException.ValidationMessages
                   }) ;
            }
            catch (NotFoundException notFoundException)
            {
                _logger.LogError($"NotFound Exception. Message -> {notFoundException.Message}. StackTrace -> {notFoundException.StackTrace}");
             
                await HandleExceptionAsync(context, new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    ErrorMessage = notFoundException.Message,
                });
            }
            catch (UnauthorizedException unauthorizedException)
            {
                _logger.LogError($"unauthorized Exception. Message -> {unauthorizedException.Message}. StackTrace -> {unauthorizedException.StackTrace}");
               
                await HandleExceptionAsync(context, new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    ErrorMessage = unauthorizedException.Message,
                });
            }
            catch (ForbiddenException forbiddenException)
            {
                _logger.LogError($"Forbidden Exception. Message -> {forbiddenException.Message}. StackTrace -> {forbiddenException.StackTrace}");

                await HandleExceptionAsync(context, new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.Forbidden,
                    ErrorMessage = forbiddenException.Message,
                });
            }
            catch (ErrorException errorException)
            {
                _logger.LogError($"Error Exception. Message -> {errorException.Message}. StackTrace -> {errorException.StackTrace}");
                
                await HandleExceptionAsync(context, new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ErrorMessage = errorException.Message,
                });
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error Exception. Message -> {ex.Message}. StackTrace -> {ex.StackTrace}");

                await HandleExceptionAsync(context, new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ErrorMessage = "Internal Server Error",
                });
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, ApiResponse response)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.StatusCode;

            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter());

            var responseJson = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(responseJson);
        }
    }
}
