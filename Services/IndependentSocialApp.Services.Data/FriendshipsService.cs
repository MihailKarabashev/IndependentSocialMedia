namespace IndependentSocialApp.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using IndependentSocialApp.Common.ExecptionFactory.Others;

    using IndependentSocialApp.Data.Common.Repositories;
    using IndependentSocialApp.Data.Models;
    using Microsoft.EntityFrameworkCore;

    using static IndependentSocialApp.Common.ModelValidations.Friendship;

    public class FriendshipsService : IFriendshipsService
    {
        private readonly IRepository<Friendship> friendshipRepo;

        public FriendshipsService(IRepository<Friendship> friendshipRepo)
        {
            this.friendshipRepo = friendshipRepo;
        }

        public async Task AcceptFriendRequestAsync(string userId, string requesterId)
        {
            var isFreindshipExist = await this.IsFriendshipExistAsync(userId, requesterId);

            if (!isFreindshipExist) throw new CustomBadRequestException(FriendshipDoNotExist);

            var friendship = await this.friendshipRepo
                 .AllAsNoTracking()
                 .Where(x => x.RequesterId == requesterId && x.AddresseeId == userId)
                 .FirstOrDefaultAsync();

            if (friendship.Status == FriendshipStatus.Pending)
            {
                friendship.Status = FriendshipStatus.Accepted;
                this.friendshipRepo.Update(friendship);
            }

            await this.friendshipRepo.SaveChangesAsync();
        }

        public async Task SendFriendRequestAsync(string userId, string addresseeId)
        {
            if (addresseeId == null)
            {
                throw new CustomBadRequestException(AddresseeIdNotFound);
            }

            if (await this.IsFriendshipExistAsync(userId,addresseeId))
            {
                throw new NotFoundException(FriendshipRequestAlreadySent);
            }

            var friendship = new Friendship()
            {
                RequesterId = userId,
                AddresseeId = addresseeId,
                Status = FriendshipStatus.Pending,
            };

            await this.friendshipRepo.AddAsync(friendship);
            await this.friendshipRepo.SaveChangesAsync();
        }

        private async Task<bool> IsFriendshipExistAsync(string currentUserId, string addresseeId)
        {
#pragma warning disable SA1408 // Conditional expressions should declare precedence
            return await this.friendshipRepo
            .AllAsNoTracking()
                .AnyAsync(u => u.RequesterId == currentUserId &&
                            u.AddresseeId == addresseeId ||
                            u.RequesterId == addresseeId &&
                            u.AddresseeId == currentUserId);
#pragma warning restore SA1408 // Conditional expressions should declare precedence
        }
    }
}
