using CoreWebAPI.ControllersSerivce;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreWebAPI
{
    /// <summary>
    /// 中间件
    /// </summary>
    public class Middle_key
    {
        RequestDelegate _next;

        public Middle_key(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null || !ValidateToken(token))
            {
                context.Response.StatusCode = 401; // Unauthorized
                return;
            }

            await _next(context);
        }

        private bool ValidateToken(string token)
        {
            // 在这里执行你的Token验证逻辑
            // 返回true意味着验证通过；返回false则不通过。
            return true; // 假设这里简单返回true，代表所有token都视为有效
        }

    }
    public class TokenValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool hasIgnoreAttribute = context.ActionDescriptor.EndpointMetadata
                                  .Any(em => em.GetType() == typeof(IgnoreTokenVerificationAttribute));

            string token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null || !ValidateToken(token))
            {
                context.Result = new UnauthorizedResult(); // 未授权访问
                return;
            }

            await next();
        }

        private bool ValidateToken(string token)
        {
            // Token验证逻辑
            string jwt = UserAppService.ValidateJwtToken(token);

            if (jwt == null)
                return false;
            else
                return true;
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class IgnoreTokenVerificationAttribute : Attribute
    {
        // 这个Attribute不需要包含任何逻辑，它的存在就是一个标记
    }

}
