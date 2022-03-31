namespace IndependentSocialApp.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using IndependentSocialApp.Common.ExecptionFactory.Others;
    using IndependentSocialApp.Data.Common.Repositories;
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Web.ViewModels.Follows;
    using Microsoft.EntityFrameworkCore;

    using static IndependentSocialApp.Common.ModelValidations.Follow;

    public class FollowService : IFollowService
    {
        private readonly IRepository<Follow> followRepo;
        private readonly IUsersService userService;

        public FollowService(
            IRepository<Follow> followRepo,
            IUsersService userService)
        {
            this.followRepo = followRepo;
            this.userService = userService;
        }

        public Follow FindFollowById(int id)
        => this.followRepo
              .AllAsNoTracking()
              .FirstOrDefault(x => x.Id == id);

        public async Task FollowAsync(CreateFollowRequestModel model)
        {
            await this.CheckUsers(model.ApplicationUserId, model.FollowerId);

            var userAlreadyFollowed = await this.followRepo
               .AllAsNoTracking()
               .AnyAsync(f => f.ApplicationUserId == model.ApplicationUserId && f.FollowerId == model.FollowerId);

            if (userAlreadyFollowed)
            {
                throw new CustomBadRequestException(CantFollowMoreThenOneTimeOneUser);
            }

            var follow = new Follow
            {
                ApplicationUserId = model.ApplicationUserId,
                FollowerId = model.FollowerId,
            };

            await this.followRepo.AddAsync(follow);
            await this.followRepo.SaveChangesAsync();
        }

        public async Task UnFollowAsync(int id)
        {
            var follow = this.FindFollowById(id);

            if (follow == null)
            {
                throw new NotFoundException(YouDontFollowThisUser);
            }

            this.followRepo.Delete(follow);
            await this.followRepo.SaveChangesAsync();
        }

        private async Task CheckUsers(string userId, string followerId)
        {
            var userExist = await this.userService.FindUserAsync(userId);

            if (userExist == null || userId == null)
            {
                throw new NotFoundException(CantFindThisUser);
            }

            var followerExist = await this.userService.FindUserAsync(followerId);

            if (followerExist == null || followerId == null)
            {
                throw new NotFoundException(FollowerMustLogIn);
            }
        }
    }
}
