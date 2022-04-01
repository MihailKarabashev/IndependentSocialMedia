namespace IndependentSocialApp.Web.Controllers
{
    using System.Threading.Tasks;
    using IndependentSocialApp.Services.Data;
    using IndependentSocialApp.Web.Infrastructure.Extensions;
    using IndependentSocialApp.Web.Infrastructure.NloggerExtentions;
    using IndependentSocialApp.Web.ViewModels.Friendships;
    using Microsoft.AspNetCore.Mvc;

    using static IndependentSocialApp.Common.NloggerMessages;

    public class FriendshipsController : ApiController
    {
        private readonly IFriendshipsService friendshipsService;
        private readonly INloggerManager nloggerManager;

        public FriendshipsController(
            IFriendshipsService friendshipsService,
            INloggerManager nloggerManager)
        {
            this.friendshipsService = friendshipsService;
            this.nloggerManager = nloggerManager;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateFriendshipRequestModel friendshipModel)
        {
            var userId = this.User.GetId();

            await this.friendshipsService.SendFriendRequestAsync(userId, friendshipModel.AddresseeId);
            this.nloggerManager.LogInfo(string.Format(SuccesfullyCreated, userId));

            return this.StatusCode(201);
        }
    }
}
