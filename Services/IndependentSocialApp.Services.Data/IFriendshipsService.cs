namespace IndependentSocialApp.Services.Data
{
    using System.Threading.Tasks;

    public interface IFriendshipsService
    {

        Task SendFriendRequestAsync(string userId, string addresseeId);

        Task AcceptFriendRequestAsync(string userId, string requesterId);
    }
}
