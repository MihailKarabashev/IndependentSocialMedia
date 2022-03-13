namespace IndependentSocialApp.Web.Infrastructure.CustomFilters
{
    using System.Threading.Tasks;
    using IndependentSocialApp.Web.Infrastructure.NloggerExtentions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using static IndependentSocialApp.Common.NloggerMessages;

    public class ValidateModelStateFilter : IAsyncActionFilter
    {
        private readonly INloggerManager _nloger;

        public ValidateModelStateFilter(INloggerManager nloger)
        {
            this._nloger = nloger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var controllerName = context.RouteData.Values["controller"].ToString();
                var actionName = context.RouteData.Values["action"].ToString();

                this._nloger.LogError(string.Format(ValidationException, controllerName, actionName));
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
                return;
            }

            await next();
        }
    }
}
