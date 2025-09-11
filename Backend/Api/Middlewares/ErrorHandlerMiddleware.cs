using Core.Exceptions;
using Core.Http.Features.HttpClients;
using System.Net;
using System.Text.Json;

namespace Api.Middlewares
{
    public class ErrorHandlerMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context, IHostEnvironment environment)
        {
            try
            {
                await next(context);
            }

            catch (UnauthorizedAccessException e)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                var result = JsonSerializer.Serialize(new BadResponseModel { Message = e.Message });
                await response.WriteAsync(result);
            }
            catch (ObjectExistsException e)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                var result = JsonSerializer.Serialize(new BadResponseModel { Message = e.Message });
                await response.WriteAsync(result);
            }
            catch (BusinessLogicException e)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                var result = JsonSerializer.Serialize(new BadResponseModel { Message = e.Message });
                await response.WriteAsync(result);
            }
            catch (ObjectNotFoundException e)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.NotFound;
                var result = JsonSerializer.Serialize(new BadResponseModel { Message = e.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
