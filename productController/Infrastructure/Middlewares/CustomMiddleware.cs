using Azure.Core;
using Data;

namespace productController.Infrastructure.Middlewares;

public class CustomMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)

    {
        AppProvider.Instance.BaseUrl = context.Request.BaseUrl();

        await next(context);
    }
}