using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace FinalApi.FilterHeader
{
    public class SecretKeyFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            StringValues secretKey;
            if (!context.HttpContext.Request.Headers.TryGetValue("secret_key", out secretKey) || secretKey != "123456")
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }
            await next();
        }
    }
}
