namespace IndependentSocialApp.Web.Controllers
{
    using System.Threading.Tasks;

    using IndependentSocialApp.Services.Data;
    using IndependentSocialApp.Web.Infrastructure.NloggerExtentions;
    using IndependentSocialApp.Web.ViewModels.Follows;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static IndependentSocialApp.Common.NloggerMessages;

    public class FollowsController : ApiController
    {
        private readonly IFollowService followService;
        private readonly INloggerManager nlog;

        public FollowsController(
            IFollowService followService,
            INloggerManager nlog)
        {
            this.followService = followService;
            this.nlog = nlog;
        }

        [HttpPost]
        [Route(nameof(Create))]
        public async Task<ActionResult> Create(CreateFollowRequestModel model)
        {
            await this.followService.FollowAsync(model);

            this.nlog.LogInfo(string.Format(SuccesfullyFollowed));

            return this.StatusCode(201);
        }

        [HttpDelete]
        [Route("remove/{id}")]
        [AllowAnonymous]

        public async Task<ActionResult> Remove(int id)
        {
            await this.followService.UnFollowAsync(id);

            this.nlog.LogInfo(string.Format(SuccesfullyRemoved, id));

            return this.NoContent();
        }
    }
}
