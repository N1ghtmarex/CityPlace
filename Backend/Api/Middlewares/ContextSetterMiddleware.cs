using Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace Api.Middlewares
{
    public class ContextSetterMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context, ICurrentHttpContextAccessor currentHttpContextAccessor)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
            {
                await next(context);
                return;
            }
            currentHttpContextAccessor.SetContext(context);
            await next(context);
        }
    }
}
