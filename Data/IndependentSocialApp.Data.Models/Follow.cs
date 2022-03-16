namespace IndependentSocialApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using IndependentSocialApp.Data.Common.Models;

    public class Follow : BaseModel<int>
    {
        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string FollowerId { get; set; }

        public ApplicationUser FollowerUser { get; set; }
    }
}
