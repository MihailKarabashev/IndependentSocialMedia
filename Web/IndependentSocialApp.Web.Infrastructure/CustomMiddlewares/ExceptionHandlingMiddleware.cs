namespace IndependentSocialApp.Web.Infrastructure.CustomMiddlewares
{
    using System;
    using System.Threading.Tasks;
    using IndependentSocialApp.Common.ExecptionFactory.Others;
    using IndependentSocialApp.Web.Common.ExecptionFactory.Auth;
    using Microsoft.AspNetCore.Http;

    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (AuthException ex)
            {
              await HandleAuthExceptionAsync(context, ex);
            }
            catch (ServiceException ex)
            {
                await HandleServiceExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(ex.Message);
            }
        }

        private static async Task HandleServiceExceptionAsync(HttpContext context, ServiceException ex)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                NoPermissionException => StatusCodes.Status401Unauthorized,
                CustomBadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            };

            var errorDetails = new ErrorDetails
            {
                Message = ex.Message,
                StatusCode = context.Response.StatusCode,
            };

            await context.Response.WriteAsync(errorDetails.ToString());
        }

        private static async Task HandleAuthExceptionAsync(HttpContext context, AuthException ex)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = ex switch
            {
                AuthBadRequestException => StatusCodes.Status400BadRequest,
                AuthNotFoundException => StatusCodes.Status404NotFound,
                AuthUnAuthorizedException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError,
            };

            var errorDetails = new ErrorDetails
            {
                Message = ex.Message,
                StatusCode = context.Response.StatusCode,
            };

            await context.Response.WriteAsync(errorDetails.ToString());
        }
    }
}
