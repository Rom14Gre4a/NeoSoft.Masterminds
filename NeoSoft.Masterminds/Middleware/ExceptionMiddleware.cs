using Microsoft.AspNetCore.Http;
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

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));

        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationErrorException validationErrorException)
            {
                await HandleExceptionAsync(context, new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    ErrorMessage = validationErrorException.Message,
                    ValidationMessages = validationErrorException.ValidationMessages
                });
            }
            catch (NotFoundException notFoundException)
            {
                await HandleExceptionAsync(context, new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    ErrorMessage = notFoundException.Message,
                });
            }
            catch (UnauthorizedException unauthorizedException)
            {
                await HandleExceptionAsync(context, new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    ErrorMessage = unauthorizedException.Message,
                });
            }
            catch (ForbiddenException forbiddenException)
            {
                await HandleExceptionAsync(context, new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.Forbidden,
                    ErrorMessage = forbiddenException.Message,
                });
            }
            catch (ErrorException errorException)
            {
                await HandleExceptionAsync(context, new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ErrorMessage = errorException.Message,
                });
            }
            catch
            {
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
