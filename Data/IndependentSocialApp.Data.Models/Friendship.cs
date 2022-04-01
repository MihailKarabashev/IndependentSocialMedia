namespace IndependentSocialApp.Data.Models
{
    public class Friendship
    {
        public int Id { get; set; }

        public ApplicationUser Requester { get; set; }

        public string RequesterId { get; set; }

        public ApplicationUser Addressee { get; set; }

        public string AddresseeId { get; set; }

        public FriendshipStatus Status { get; set; }
    }
}
